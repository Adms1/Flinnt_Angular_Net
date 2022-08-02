using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Flinnt.Business.ViewModels;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Flinnt.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IHtmlLocalizer<AccountController> _localizer;
        private readonly IAccountService _accountService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        private readonly IBackgroundService _backgroundService;

        public AccountController(IAccountService accountService, IBackgroundService backgroundService, IHtmlLocalizer<AccountController> localizer)
        {
            //_userManager = userManager;
            //_signInManager = signInManager;
            _accountService = accountService;
            _backgroundService = backgroundService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("GetLocalizationDemoString")]
        public async Task<object> GetLocalizationDemoString()
        {
            return await GetDataWithMessage(async () =>
            {
                var text = _localizer["ControllerString"].Value.ToString();
                return Response(text, string.Empty);
            });

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<object> GetAll()
        {
            // Logger.Info("LogPrint");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _accountService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> Login([FromBody] AccountModel accountModel)
        {
            return await GetDataWithMessage(async () =>
            {
                if (accountModel != null)
                {
                    //var result = await _signInManager.PasswordSignInAsync(personModel.Name, personModel.Password, false, false);                    
                    //if (result.Succeeded)
                    //{                        
                    //    var user = new LoginResponseModel();
                    //    user.ApplicationUser = await _userManager.FindByNameAsync(personModel.Name);
                    //    var token = ApiTokenHelper.GenerateJSONWebToken(user.ApplicationUser);
                    //    user.Token = "TOKEN";
                    //    return Response(user, string.Empty);
                    //}
                }
                return Response(new LoginResponseModel(), _localizer["UserNotFound"].Value.ToString(), DropMessageType.Error);
            });
        }

        [HttpGet]
        public async Task<object> Get(int id)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _accountService.GetAsync(id);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        public async Task<object> Post([FromBody] Account model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return model.Id <= 0 ? await AddAsync(model) : await UpdateAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), DropMessageType.Error);
            });
        }

        private async Task<Tuple<Account, string, DropMessageType>> AddAsync(Account model)
        {
            var flag = await _accountService.AddAsync(model);
            if (flag)
            {
                //_backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendWelcomeEmail());
                return Response(model, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(model, _localizer["RecordNotAdded"].Value.ToString(), DropMessageType.Error);
        }

        private async Task<Tuple<Account, string, DropMessageType>> UpdateAsync(Account model)
        {
            var flag = await _accountService.UpdateAsync(model);
            if (flag)
                return Response(model, _localizer["RecordUpdeteSuccess"].Value.ToString());
            return Response(model, _localizer["RecordNotUpdate"].Value.ToString(), DropMessageType.Error);
        }

        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            return await GetDataWithMessage(async () =>
            {
                var flag = await _accountService.DeleteAsync(id);
                if (flag)
                    return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                return Response(new BooleanResponseModel { Value = flag }, _localizer["ReordNotDeleteSucess"].Value.ToString(), DropMessageType.Error);
            });
        }
    }
}