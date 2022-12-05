using AutoMapper;
using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Flinnt.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Flinnt.Background
{
    // TODO: Refactor Code to re-used everyware
    public class BackgroundStudentJobs : IBackgroundStudentJobs
    {
        public static IUnitOfWork unitOfWork { get; set; }
        public static IMapper mapper { get; set; }
        public static IStudentService _studentService;
        public static IUserService _userService;
        public static IUserProfileService _userProfileService;
        public static IUserInstituteService _userInstituteService;
        public static ICityService _cityService;
        public static IUserAccountHistoryService _userAccountHistoryService;
        public static IUserParentChildRelationshipService _userParentChildRelationshipService;
        public static IUserInstituteGroupService _userInstituteGroupService;

        #region Constructor

        public BackgroundStudentJobs()
        {
        }
        #endregion Constructor

        public async Task ImportStudentsAsync(List<StudentViewModel> studentViewModels)
        {
            bool isDispose = false;
            if (unitOfWork != null)
            {
                isDispose = true;
            }
            unitOfWork = new UnitOfWork(new edplexdbContext());

            _studentService = new StudentService(unitOfWork, mapper);
            _userService = new UserService(unitOfWork, mapper);
            _userProfileService = new UserProfileService(unitOfWork, mapper);
            _userInstituteService = new UserInstituteService(unitOfWork, mapper);
            _cityService = new CityService(unitOfWork, mapper);
            _userAccountHistoryService = new UserAccountHistoryService(unitOfWork, mapper);
            _userParentChildRelationshipService = new UserParentChildRelationshipService(unitOfWork, mapper);
            _userInstituteGroupService = new UserInstituteGroupService(unitOfWork, mapper);

            // TODO: To get current claim values
            var currentInstituteID = 14;
            var currentUserID = 12;

            foreach (StudentViewModel _student in studentViewModels)
            {
                var extStudent = await _studentService.ValidateStudent(_student);

                if (extStudent.Any())
                {
                    continue;
                }

                var user = await _userService.GetUserByLoginId(_student.EmailId);
                if (user != null)
                {
                    // usertype parent or student check
                    if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                    {
                        // consider dublicate row
                        // Student account already exist!
                        continue;
                    }
                }
                else
                {
                    // user not found it will need to create and mapped
                    var userRes = await _userService.AddAsync(new User
                    {
                        LoginId = _student.EmailId,
                        AuthenticationTypeId = (int)AutheticationTypeEnum.Edplex,
                        IsActive = true,
                        IsDeleted = false,
                        Password = "flinnt@123",
                        OneTimePassword = "flinnt@123",
                        UserTypeId = (int)UserTypes.Student,
                        RegistrationDateTime = DateTime.Now,
                        LastLoginDateTime = DateTime.Now
                    });

                    if (userRes != null)
                    {
                        _student.UserId = userRes.UserId;

                        // check relation with parent by passing parentId (userid)
                        UserProfile prtUsr = new UserProfile();
                        if (_student.parentUserId != null)
                        {
                            var parentUser = await _userService.GetAsync(_student.parentUserId.Value);

                            if (parentUser == null)
                            {
                                //"Parent account not exist!"
                                continue;
                            }

                            prtUsr = parentUser.UserProfiles.FirstOrDefault();

                            await _userParentChildRelationshipService.AddAsync(new UserParentChildRelationshipModel
                            {
                                ChildUserId = userRes.UserId,
                                ChildUserTypeId = (int)UserTypes.Student,
                                ParentUserId = _student.parentUserId.Value,
                                InstituteId = Convert.ToInt32(currentInstituteID),
                                ParentUserTypeId = (int)UserTypes.Parent,
                            });
                        }

                        await _userProfileService.AddAsync(new UserProfile
                        {
                            FirstName = _student.FirstName,
                            LastName = _student.LastName,
                            EmailId = _student.EmailId,
                            UserId = userRes.UserId,
                            MobileNo = _student.MobileNo,
                            GenderId = _student.GenderId,
                            CreateDateTime = DateTime.Now,
                            CityId = prtUsr?.CityId,
                            Address = prtUsr?.Address,
                            CountryId = prtUsr?.CountryId,
                            StateId = prtUsr?.StateId,
                            Pincode = prtUsr?.Pincode
                        });

                        await _userInstituteService.AddAsync(new UserInstitute
                        {
                            InstituteId = Convert.ToInt32(currentInstituteID),
                            UserId = userRes.UserId,
                            UserTypeId = (int)UserTypes.Student,
                            IsActive = true,
                            CreateDateTime = DateTime.Now
                        });

                        await _userInstituteGroupService.AddAsync(new UserInstituteGroupModel
                        {
                            UserId = userRes.UserId,
                            InstituteId = Convert.ToInt32(currentInstituteID),
                            InstituteGroupId = _student.instituteGroupId,
                            InstituteDivisionId = _student.instituteDivisionId
                        });

                        //save userAccountHistory
                        UserAccountHistory userAccountHistory = new UserAccountHistory
                        {
                            UserId = _student.UserId,
                            ActionUserId = Convert.ToInt32(currentUserID),
                            HistoryAction = "UserCreatedForStudent",
                            ClientIp = "BackgroundJob"
                        };
                        await _userAccountHistoryService.AddAsync(userAccountHistory);
                    }
                }

                var student = await _studentService.AddAsync(_student);
            }
        }

        private static long GetInstituteId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;


            var userId = identity.Claims.Where(c => c.Type == "InstituteId")
                               .Select(c => c.Value).FirstOrDefault();
            return Convert.ToInt64(userId);
        }

        private static long GetUserId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;


            var userId = identity.Claims.Where(c => c.Type == "UserId")
                               .Select(c => c.Value).FirstOrDefault();
            return Convert.ToInt64(userId);
        }
    }
}