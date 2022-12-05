using AutoMapper;
using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Flinnt.UoW;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Flinnt.Background
{
    // TODO: Refactor Code to re-used everyware
    public class BackgroundParentJobs : IBackgroundParentJobs
    {
        public static IUnitOfWork unitOfWork { get; set; }
        public static IMapper mapper { get; set; }
        public static IParentService _parentService;
        public static IUserService _userService;
        public static IUserProfileService _userProfileService;
        public static IUserInstituteService _userInstituteService;
        public static ICityService _cityService;
        public static IUserAccountHistoryService _userAccountHistoryService;

        #region Constructor

        public BackgroundParentJobs()
        {
        }
        #endregion Constructor
        
        public async Task ImportParentsAsync(List<ParentViewModel> parentViewModels)
        {
            bool isDispose = false;
            if (unitOfWork != null)
            {
                isDispose = true;
            }
            unitOfWork = new UnitOfWork(new edplexdbContext());

            _parentService = new ParentService(unitOfWork, mapper);
            _userService = new UserService(unitOfWork, mapper);
            _userProfileService = new UserProfileService(unitOfWork, mapper);
            _userInstituteService = new UserInstituteService(unitOfWork, mapper);
            _cityService = new CityService(unitOfWork, mapper);
            _userAccountHistoryService = new UserAccountHistoryService(unitOfWork, mapper);

            // TODO: To get current claim values
            var currentInstituteID = 14;
            var currentUserID = 12;

            foreach (var _parent in parentViewModels)
            {
                var extParent = await _parentService.ValidateParent(_parent);

                if (extParent.Any())
                {
                    //Parent account already exist!
                    continue;
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
                        InstituteId = Convert.ToInt32(currentInstituteID),
                        UserId = _parent.UserId,
                        UserTypeId = (int)UserTypes.Parent,
                        IsActive = true,
                        CreateDateTime = DateTime.Now
                    });

                    //save userAccountHistory

                    UserAccountHistory userAccountHistory = new UserAccountHistory
                    {
                        UserId = Convert.ToInt32(_parent.UserId),
                        ActionUserId = Convert.ToInt32(currentUserID),
                        HistoryAction = "UserCreatedForParent",
                        ClientIp = "BackgroundJob"
                    };
                    await _userAccountHistoryService.AddAsync(userAccountHistory);

                }

            }
        }
    }
}