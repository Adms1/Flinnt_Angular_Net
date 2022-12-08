using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using System.Transactions;

namespace Flinnt.Background
{
    // TODO: Refactor Code to re-used everyware
    public class BackgroundParentJobs : IBackgroundParentJobs
    {
        public static IParentService _parentService;
        public static IUserService _userService;
        public static IUserProfileService _userProfileService;
        public static IUserInstituteService _userInstituteService;
        public static ICityService _cityService;
        public static IUserAccountHistoryService _userAccountHistoryService;
        public static IStateService _stateService;
        public static ICountryService _countryService;
        public static IBackgroundMailerJobs _backgroundMailerJobs;
        public static IInstituteService _instituteService { get; set; }

        #region Constructor

        public BackgroundParentJobs(
           IParentService parentService,
           IUserService userService,
           IUserProfileService userProfileService,
           IUserInstituteService userInstituteService,
           ICityService cityService,
           IUserAccountHistoryService userAccountHistoryService,
           IStateService stateService,
           ICountryService countryService,
           IBackgroundMailerJobs backgroundMailerJobs,
           IInstituteService instituteService)
        {
            _parentService = parentService;
            _userService = userService;
            _userProfileService = userProfileService;
            _cityService = cityService;
            _userInstituteService = userInstituteService;
            _userAccountHistoryService = userAccountHistoryService;
            _stateService = stateService;
            _countryService = countryService;
            _backgroundMailerJobs = backgroundMailerJobs;
            _instituteService = instituteService;
        }
        #endregion Constructor

        public async Task ImportParentsAsync(List<ParentViewModel> parentViewModels)
        {
            try
            {
                int instituteId = 0;
                int userId = 0;
                string instituteEmailId = "";

                foreach (var _parent in parentViewModels)
                {
                    if (instituteId == 0 && userId == 0)
                    {
                        instituteId = _parent.InstituteId;
                        userId = _parent.LoggedUserId;
                    }

                    var extParent = await _parentService.ValidateParent(_parent);

                    if (extParent.Any())
                    {
                        //Parent account already exist!
                        continue;
                    }

                    if (string.IsNullOrEmpty(instituteEmailId))
                    {
                        var institute = await _instituteService.GetAsync(instituteId);
                        if (institute != null)
                        {
                            instituteEmailId = institute.EmailId;
                        }
                    }
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            //check state
                            var state = await _stateService.GetByStateNameAsync(_parent.StateName);
                            if (state == null)
                            {
                                // set to default Gujarat
                                var state1 = await _stateService.GetByStateNameAsync("gujarat");
                                if (state1 != null)
                                    _parent.StateId = state1.StateId;
                                continue;
                            }
                            else
                            {
                                _parent.StateId = state.StateId;
                            }

                            //check country
                            var country = await _countryService.GetByCountryNameAsync(_parent.CountryName);
                            if (country == null)
                            {
                                // set to default Gujarat
                                var country1 = await _countryService.GetByCountryNameAsync("india");
                                if (country1 != null)
                                    _parent.CountryId = country1.CountryId;
                                continue;
                            }
                            else
                            {
                                _parent.CountryId = country.CountryId;
                            }

                            var user = await _userService.GetUserByLoginId(_parent.PrimaryEmailId);
                            if (user != null)
                            {
                                // usertype parent or student check
                                if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                                {
                                    // consider dublicate row
                                    // User account already exist!
                                    continue;
                                }
                                _parent.UserId = user.UserId;

                                var existingCity = await _cityService.GetByCityNameAsync(_parent.CityName);

                                if (existingCity == null)
                                {
                                    CityViewModel cityViewModel = new CityViewModel
                                    {
                                        CityName = _parent.CityName,
                                        CreateDateTime = DateTime.Now,
                                        StateId = _parent.StateId.Value,
                                        IsActive = true
                                    };
                                    var city = await _cityService.AddAsync(cityViewModel);

                                    _parent.CityId = city.CityId;
                                }
                                else
                                {
                                    _parent.CityId = existingCity.CityId;
                                }
                            }
                            else
                            {
                                // user not found it will need to create and mapped
                                var userRes = await _userService.AddAsync(new User
                                {
                                    LoginId = _parent.PrimaryEmailId,
                                    AuthenticationTypeId = (int)AutheticationTypeEnum.Edplex,
                                    IsActive = true,
                                    IsDeleted = false,
                                    Password = "flinnt@123",
                                    OneTimePassword = "flinnt@123",
                                    UserTypeId = (int)UserTypes.Parent,
                                    RegistrationDateTime = DateTime.Now,
                                    LastLoginDateTime = DateTime.Now
                                });

                                if (userRes != null)
                                {
                                    //save city
                                    var existingCity = await _cityService.GetByCityNameAsync(_parent.CityName);

                                    if (existingCity == null)
                                    {
                                        CityViewModel cityViewModel = new CityViewModel
                                        {
                                            CityName = _parent.CityName,
                                            CreateDateTime = DateTime.Now,
                                            StateId = _parent.StateId.Value,
                                            IsActive = true
                                        };
                                        var city = await _cityService.AddAsync(cityViewModel);

                                        _parent.CityId = city.CityId;
                                    }
                                    else
                                    {
                                        _parent.CityId = existingCity.CityId;
                                    }

                                    _parent.UserId = userRes.UserId;
                                    await _userProfileService.AddAsync(new UserProfile
                                    {
                                        FirstName = _parent.Parent1FirstName,
                                        LastName = _parent.Parent1LastName,
                                        EmailId = _parent.PrimaryEmailId,
                                        UserId = _parent.UserId,
                                        MobileNo = _parent.PrimaryMobileNo,
                                        GenderId = _parent.Parent1Relationship == "Male" ? (byte)GenderEnum.Male : (byte)GenderEnum.Female,
                                        CreateDateTime = DateTime.Now,
                                        CityId = _parent.CityId,
                                        Address = _parent.AddressLine1 + ", " + _parent.AddressLine2,
                                        CountryId = (byte)_parent.CountryId,
                                        StateId = (byte)_parent.StateId,
                                        Pincode = _parent.Pincode
                                    });
                                }
                            }

                            var parent = await _parentService.AddAsync(_parent);
                            if (parent != null)
                            {
                                await _userInstituteService.AddAsync(new UserInstitute
                                {
                                    InstituteId = instituteId,
                                    UserId = _parent.UserId,
                                    UserTypeId = (int)UserTypes.Parent,
                                    IsActive = true,
                                    CreateDateTime = DateTime.Now
                                });

                                //save userAccountHistory

                                UserAccountHistory userAccountHistory = new UserAccountHistory
                                {
                                    UserId = Convert.ToInt32(_parent.UserId),
                                    ActionUserId = userId,
                                    HistoryAction = "UserCreatedForParent",
                                    ClientIp = "BackgroundJob"
                                };
                                await _userAccountHistoryService.AddAsync(userAccountHistory);
                            }
                            scope.Complete();
                            scope.Dispose();
                        }
                        catch (TransactionException tex)
                        {
                            scope.Dispose();
                            throw;
                        }
                    }
                }

                //send summary email
                _backgroundMailerJobs.SendImportParentSummaryEmail("Parent Success", instituteEmailId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
