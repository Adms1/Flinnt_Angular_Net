using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using NLog;
using Flinnt.Business.ViewModels;
using AAT.API.Helpers;
using Microsoft.AspNetCore.Mvc.Localization;
using Flinnt.Business.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Flinnt.Mail.Models;
using Flinnt.Services;

namespace Flinnt.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/authentication")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IHtmlLocalizer<AuthenticationController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserAccountVerificationService _userAccountVerificationService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly IUserService _userService;
        private readonly IInstituteService _instituteService;
        private readonly IUserProfileService _userProfileService;
        private readonly ILoginHistoryService _loginHistoryService;

        private readonly IBackgroundService _backgroundService;

        public AuthenticationController(IBackgroundService backgroundService, 
            IHtmlLocalizer<AuthenticationController> localizer, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IUserAccountVerificationService userAccountVerificationService,
            IUserService userService,
            IInstituteService instituteService,
            IUserInstituteService userInstituteService,
            IUserProfileService userProfileService,
            ILoginHistoryService loginHistoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _backgroundService = backgroundService;
            _userAccountVerificationService = userAccountVerificationService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _instituteService = instituteService;
            _loginHistoryService = loginHistoryService;
            _userProfileService = userProfileService;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("test-email")]
        [AllowAnonymous]
        public async Task<object> Login(string emailId)
        {
            return await GetDataWithMessage(async () =>
            {
                _backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendOtpEmail("1234", emailId));
                return Response(new LoginResponseModel(), "Ok", HttpStatusCode.Accepted);
            });
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginViewModel loginViewModel)
        {
            return await GetDataWithMessage(async () =>
            {
                if (loginViewModel != null)
                {
                    var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
                    var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        var loginResponse = new LoginResponseModel();
                       
                        loginResponse.ApplicationUser = await _userManager.FindByNameAsync(loginViewModel.Email);
                        var userId = loginResponse.ApplicationUser.UserId;
                        var userOtpRes = await _userAccountVerificationService.GetByUserIdAsync(userId);
                        var user = await _userService.GetAsync(userId);
                        var userProfile = await _userProfileService.GetByUserIdAsync(userId);
                        var userInstitute = await _userInstituteService.GetByUserIdAsync(userId);

                        var token = ApiTokenHelper.GenerateJSONWebToken(loginResponse.ApplicationUser, userInstitute.InstituteId);
                        loginResponse.Token = token;

                        // otp
                        var otpNumber = GenerateRandomNo();
                        
                        // login history
                        LoginHistory loginHistory = new LoginHistory
                        {
                            UserId = userId,
                            User = user,
                            ClientIp = ipAddress.ToString(),
                            LoginDateTime = DateTime.Now
                        }; 
                        await _loginHistoryService.AddAsync(loginHistory);

                        if(userInstitute != null)
                        {
                            var instituteId = userInstitute.InstituteId;
                            loginResponse.InstituteId = userInstitute.InstituteId;

                            var institute = await _instituteService.GetAsync(instituteId);

                            if (institute != null)
                                loginResponse.InstituteModel = institute;
                        }

                        if(userProfile != null)
                        {
                            loginResponse.UserProfile = userProfile;
                        }

                        if (userOtpRes != null)
                        {
                            if(!userOtpRes.IsVerified.Value)
                            {
                                userOtpRes.ExpireDateTime = DateTime.Now;
                                userOtpRes.VerificationCode = otpNumber;
                                await _userAccountVerificationService.UpdateAsync(userOtpRes);

                                _backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendOtpEmail(otpNumber, user.LoginId));
                            }
                            else
                            {
                                // user verified
                                loginResponse.ApplicationUser.IsVerified = true;
                            }
                        }
                        else
                        {
                            UserAccountVerification userAccountVerification = new UserAccountVerification
                            {
                                UserId = userId,
                                User = user,
                                VerificationCode = otpNumber,
                                ExpireDateTime = DateTime.Now.AddMinutes(30),
                                CreateDateTime = DateTime.Now
                            };
                            await _userAccountVerificationService.AddAsync(userAccountVerification);

                            _backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendOtpEmail(otpNumber, user.LoginId));
                        }
                        return Response(loginResponse, string.Empty);
                    }
                }
                return Response(new LoginResponseModel(), _localizer["UserNotFound"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }

        //Generate RandomNo
        private string GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }
    }
}