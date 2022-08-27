using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Route("api/[controller]")]
    public class InstituteController : BaseApiController
    {
        private readonly IInstituteService _instituteService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHtmlLocalizer<InstituteController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteController(IInstituteService instituteService, 
            IUserService userService, 
            IUserProfileService userProfileService, 
            IHtmlLocalizer<InstituteController> htmlLocalizer)
        {
            _instituteService = instituteService;
            _userService = userService;
            _userProfileService = userProfileService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<object> GetAll()
        {
            Logger.Info("GetAll");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("AddOrUpdateRecord")]
        public async Task<object> Post([FromBody] InstituteModel model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return model.InstituteId <= 0 ? await AddAsync(model) : await UpdateAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), DropMessageType.Error);
            });
        }

        private async Task<Tuple<InstituteModel, string, DropMessageType>> AddAsync(InstituteModel model)
        {
            if(await _userProfileService.GetByEmailAsync(model.EmailId) != null)
            {
                // check if emailId exist
                return Response(model, _localizer["fmEmailIdFound"].Value.ToString(), DropMessageType.Error);
            }
            model.InstituteTypeId = 1; // static for now
            var extInstitute = await _instituteService.AddAsync(model);
            if (extInstitute != null)
            {
                // save userObj
                User user = new User
                {
                    LoginId = Guid.NewGuid().ToString(), // need to verify
                    AuthenticationTypeId = 1, // static for now
                    IsActive = true,
                    IsDeleted = false,
                    Password = model.User.Password,
                    OneTimePassword = model.User.Password,
                    UserTypeId = 4, // static for now
                    RegistrationDateTime = DateTime.Now
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
                }

                //_backgroundService.EnqueueJob<IBackgroundMailerJobs>(m => m.SendWelcomeEmail());
                return Response(extInstitute, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(extInstitute, _localizer["RecordNotAdded"].Value.ToString(), DropMessageType.Error);
        }

        private async Task<Tuple<InstituteModel, string, DropMessageType>> UpdateAsync(InstituteModel model)
        {
            var flag = await _instituteService.UpdateAsync(model);
            if (flag)
                return Response(model, _localizer["RecordUpdeteSuccess"].Value.ToString());
            return Response(model, _localizer["RecordNotUpdate"].Value.ToString(), DropMessageType.Error);
        }


        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            return await GetDataWithMessage(async () =>
            {
                var flag = await _instituteService.DeleteAsync(id);
                if (flag)
                    return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                return Response(new BooleanResponseModel { Value = flag }, _localizer["ReordNotDeleteSucess"].Value.ToString(), DropMessageType.Error);
            });
        }
    }
}