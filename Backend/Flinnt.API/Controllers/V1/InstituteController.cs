using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/institute")]
    public class InstituteController : BaseApiController
    {
        private readonly IInstituteService _instituteService;
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserAccountHistoryService _userAccountHistoryService;
        private readonly IUserAccountVerificationService _userAccountVerificationService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly IUserSettingService _userSettingService;
        private readonly IBackgroundService _backgroundService;
        private readonly IHtmlLocalizer<InstituteController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<ApplicationUser> _userManager;

        public InstituteController(IInstituteService instituteService, 
            IUserService userService, 
            IUserProfileService userProfileService,
            ICityService cityService,
            IUserRoleService userRoleService,
            IUserAccountHistoryService userAccountHistoryService,
            IUserAccountVerificationService userAccountVerificationService,
            IUserInstituteService userInstituteService,
            IUserSettingService userSettingService,
            IBackgroundService backgroundService,
            IHtmlLocalizer<InstituteController> htmlLocalizer,
            UserManager<ApplicationUser> userManager)
        {
            _instituteService = instituteService;
            _userService = userService;
            _userProfileService = userProfileService;
            _cityService = cityService;
            _userRoleService = userRoleService;
            _userAccountHistoryService = userAccountHistoryService;
            _userInstituteService = userInstituteService;
            _userSettingService = userSettingService;
            _userAccountVerificationService = userAccountVerificationService;
            _backgroundService = backgroundService;
            _localizer = htmlLocalizer;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("GetAll");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<object> Post([FromBody] InstituteModel model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("account-verify/{otp}")]
        public async Task<object> Post(string otp)
        {
            return await GetDataWithMessage(async () =>
            {
                // TODO : KISHAN
                var result = (await _instituteService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }


        [HttpPut]
        [Route("update")]
        public async Task<object> Put([FromBody] InstituteModel model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await UpdateAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }
        private async Task<Tuple<InstituteModel, string, HttpStatusCode>> AddAsync(InstituteModel model)
        {
            if(await _userProfileService.GetByEmailAsync(model.EmailId) != null)
            {
                return Response(model, _localizer["fmEmailIdFound"].Value.ToString(), HttpStatusCode.InternalServerError);
            }

            //save city
            CityViewModel cityViewModel = new CityViewModel
            {
                CityName = model.CityName,
                CreateDateTime = DateTime.Now,
                StateId = model.StateId.Value,
                IsActive = true
            };
            var city = await _cityService.AddAsync(cityViewModel);

            model.InstituteTypeId = 1;
            model.CityId = city.CityId;
            var extInstitute = await _instituteService.AddAsync(model);
            if (extInstitute != null)
            {
                // save userObj
                User user = new User
                {
                    LoginId = model.EmailId,
                    AuthenticationTypeId = 1,
                    IsActive = true,
                    IsDeleted = false,
                    Password = model.Password,
                    OneTimePassword = model.Password,
                    UserTypeId = 1,
                    RegistrationDateTime = DateTime.Now,
                    LastLoginDateTime = DateTime.Now
                };

                var userRes = await _userService.AddAsync(user);
                if(userRes != null)
                {
                    UserProfile userProfile = new UserProfile
                    {
                        UserId = userRes.UserId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailId = model.EmailId,
                        MobileNo = model.MobileNo,
                        CreateDateTime = DateTime.Now
                    };

                    await _userProfileService.AddAsync(userProfile);

                    //save userInstitute

                    UserInstitute userInstitute = new UserInstitute
                    {
                        InstituteId = extInstitute.InstituteId,
                        RoleId = 1,
                        UserId = userRes.UserId,
                        UserTypeId = 1,
                        IsActive = true,
                        CreateDateTime = DateTime.Now
                    };

                    await _userInstituteService.AddAsync(userInstitute);
                    //save userRole

                    UserRole userRole = new UserRole
                    {
                        RoleId = 1,
                        UserId = userRes.UserId,
                        CreateDateTime = DateTime.Now
                    };
                    await _userRoleService.AddAsync(userRole);

                    //save identity
                    var identityObj = new ApplicationUser
                    {
                        UserName = model.EmailId,
                        Email = model.EmailId,
                        UserId = userRes.UserId,
                        PhoneNumber = model.MobileNo
                    };
                    await _userManager.CreateAsync(identityObj, model.Password);

                    // save userSetting

                    var userSetting = new UserSetting
                    {
                        UserId = userRes.UserId
                    };

                    await _userSettingService.AddAsync(userSetting);

                    //save userAccountVerification

                    UserAccountVerification userAccountVerification = new UserAccountVerification
                    {
                        UserId = userRes.UserId,
                        VerificationCode = GenerateRandomNo(),
                        ExpireDateTime = DateTime.Now.AddMinutes(30),
                        CreateDateTime = DateTime.Now
                    };
                    await _userAccountVerificationService.AddAsync(userAccountVerification);

                    //save userAccountHistory

                    UserAccountHistory userAccountHistory = new UserAccountHistory
                    {
                        UserId = userRes.UserId,
                        HistoryAction = "User created"
                    };
                    await _userAccountHistoryService.AddAsync(userAccountHistory);
                }

                _backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendWelcomeEmail());
                return Response(extInstitute, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(extInstitute, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        //Generate RandomNo
        private string GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }

        private async Task<Tuple<InstituteModel, string, HttpStatusCode>> UpdateAsync(InstituteModel model)
        {
            var flag = await _instituteService.UpdateAsync(model);
            if (flag)
                return Response(model, _localizer["RecordUpdeteSuccess"].Value.ToString());
            return Response(model, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }


        [HttpDelete]
        [Route("delete")]
        public async Task<object> Delete(int id)
        {
            return await GetDataWithMessage(async () =>
            {
                var flag = await _instituteService.DeleteAsync(id);
                if (flag)
                    return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                return Response(new BooleanResponseModel { Value = flag }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}