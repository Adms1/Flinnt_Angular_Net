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
using System.Transactions;

namespace Flinnt.Background
{
    // TODO: Refactor Code to re-used everyware
    public class BackgroundStudentJobs : IBackgroundStudentJobs
    {
        public static IStudentService _studentService;
        public static IUserService _userService;
        public static IUserProfileService _userProfileService;
        public static IUserInstituteService _userInstituteService;
        public static ICityService _cityService;
        public static IUserAccountHistoryService _userAccountHistoryService;
        public static IUserParentChildRelationshipService _userParentChildRelationshipService;
        public static IUserInstituteGroupService _userInstituteGroupService;
        public static IBackgroundMailerJobs _backgroundMailerJobs;
        public static IInstituteService _instituteService { get; set; }

        #region Constructor

        public BackgroundStudentJobs(
           IStudentService studentService,
           IUserService userService,
           IUserProfileService userProfileService,
           IUserInstituteService userInstituteService,
           ICityService cityService,
           IUserAccountHistoryService userAccountHistoryService,
           IUserParentChildRelationshipService userParentChildRelationshipService,
           IUserInstituteGroupService userInstituteGroupService,
           IBackgroundMailerJobs backgroundMailerJobs,
           IInstituteService instituteService
            )
        {
            _studentService = studentService;
            _userService = userService;
            _userProfileService = userProfileService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userAccountHistoryService = userAccountHistoryService;
            _userInstituteGroupService = userInstituteGroupService;
            _userParentChildRelationshipService = userParentChildRelationshipService;
            _backgroundMailerJobs = backgroundMailerJobs;
            _instituteService = instituteService;
        }
        #endregion Constructor

        public async Task ImportStudentsAsync(List<StudentViewModel> studentViewModels)
        {
            try
            {
                int instituteId = 0;
                int userId = 0;
                string instituteEmailId = "";

                foreach (StudentViewModel _student in studentViewModels)
                {
                    if(instituteId == 0 && userId == 0)
                    {
                        instituteId = _student.InstituteId;
                        userId = _student.LoggedUserId;
                    }

                    var extStudent = await _studentService.ValidateStudent(_student);

                    if (extStudent.Any())
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(instituteEmailId))
                    {
                        var institute = await _instituteService.GetAsync(instituteId);
                        if(institute != null)
                        {
                            instituteEmailId = institute.EmailId;
                        }
                    }
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
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
                                            InstituteId = instituteId,
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
                                        InstituteId = instituteId,
                                        UserId = userRes.UserId,
                                        UserTypeId = (int)UserTypes.Student,
                                        IsActive = true,
                                        CreateDateTime = DateTime.Now
                                    });

                                    await _userInstituteGroupService.AddAsync(new UserInstituteGroupModel
                                    {
                                        UserId = userRes.UserId,
                                        InstituteId = instituteId,
                                        InstituteGroupId = _student.instituteGroupId,
                                        InstituteDivisionId = _student.instituteDivisionId
                                    });

                                    //save userAccountHistory
                                    UserAccountHistory userAccountHistory = new UserAccountHistory
                                    {
                                        UserId = _student.UserId,
                                        ActionUserId = userId,
                                        HistoryAction = "UserCreatedForStudent",
                                        ClientIp = "BackgroundJob"
                                    };
                                    await _userAccountHistoryService.AddAsync(userAccountHistory);
                                }
                            }

                            var student = await _studentService.AddAsync(_student);

                            scope.Complete();
                        }
                        catch (TransactionException txp)
                        {
                            scope.Dispose();
                            throw;
                        }
                    }
                }

                //send summary email
                _backgroundMailerJobs.SendImportParentSummaryEmail("Student Success", instituteEmailId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}