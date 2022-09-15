using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Flinnt.Business.ViewModels;
using AAT.API.Helpers;
using Microsoft.AspNetCore.Mvc.Localization;
using Flinnt.Business.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Net;
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
        private readonly IUserService _userService;

        private readonly IBackgroundService _backgroundService;

        public AuthenticationController(IBackgroundService backgroundService, 
            IHtmlLocalizer<AuthenticationController> localizer, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IUserAccountVerificationService userAccountVerificationService,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _backgroundService = backgroundService;
            _userAccountVerificationService = userAccountVerificationService;
            _userService = userService;
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
                    var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        var user = new LoginResponseModel();
                        user.ApplicationUser = await _userManager.FindByNameAsync(loginViewModel.Email);
                        var token = ApiTokenHelper.GenerateJSONWebToken(user.ApplicationUser);
                        user.Token = token;

                        // otp
                        var otpNumber = GenerateRandomNo();
                        var userId = user.ApplicationUser.UserId;
                        var userOtpRes = await _userAccountVerificationService.GetByUserIdAsync(userId);
                        //var user = await _userService.GetAsync(userId);
                        if (userOtpRes != null)
                        {
                            if(!userOtpRes.IsVerified.Value)
                            {
                                userOtpRes.ExpireDateTime = DateTime.Now;
                                userOtpRes.VerificationCode = otpNumber;
                            }
                            //await _userAccountVerificationService.UpdateAsync(userOtpRes);
                        }
                        else
                        {
                            UserAccountVerification userAccountVerification = new UserAccountVerification
                            {
                                UserId = userId,
                                VerificationCode = otpNumber,
                                ExpireDateTime = DateTime.Now.AddMinutes(30),
                                CreateDateTime = DateTime.Now
                            };
                            //await _userAccountVerificationService.AddAsync(userAccountVerification);
                        }
                        return Response(user, string.Empty);
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