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
        private readonly IUserService _userService;
        private readonly ILoginHistoryService _loginHistoryService;

        private readonly IBackgroundService _backgroundService;

        public AuthenticationController(IBackgroundService backgroundService, 
            IHtmlLocalizer<AuthenticationController> localizer, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IUserAccountVerificationService userAccountVerificationService,
            IUserService userService,
            ILoginHistoryService loginHistoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _backgroundService = backgroundService;
            _userAccountVerificationService = userAccountVerificationService;
            _userService = userService;
            _loginHistoryService = loginHistoryService;
            _localizer = localizer;
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
                        var token = ApiTokenHelper.GenerateJSONWebToken(loginResponse.ApplicationUser);
                        loginResponse.Token = token;

                        // otp
                        var otpNumber = GenerateRandomNo();
                        var userId = loginResponse.ApplicationUser.UserId;
                        var userOtpRes = await _userAccountVerificationService.GetByUserIdAsync(userId);
                        var user = await _userService.GetAsync(Convert.ToInt64(userId));

                        // login history
                        LoginHistory loginHistory = new LoginHistory
                        {
                            UserId = userId,
                            User = user,
                            ClientIp = ipAddress.ToString(),
                            LoginDateTime = DateTime.Now,
                        }; 
                        await _loginHistoryService.AddAsync(loginHistory);

                        if (userOtpRes != null)
                        {
                            if(!userOtpRes.IsVerified.Value)
                            {
                                userOtpRes.ExpireDateTime = DateTime.Now;
                                userOtpRes.VerificationCode = otpNumber;
                            }
                            await _userAccountVerificationService.UpdateAsync(userOtpRes);

                            //object otpModel = new
                            //{
                            //    Otp = otpNumber,
                            //    RecipientMail = "vaishnanik@gmail.com"
                            //};
                            
                            //_backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendOtpEmail(objOtp));
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
                            //object otpModel = new
                            //{
                            //    Otp = otpNumber,
                            //    RecipientMail = "vaishnanik@gmail.com",//user.EmailId
                            //};
                            //_backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendOtpEmail(otpModel));
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