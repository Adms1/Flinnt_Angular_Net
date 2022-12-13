using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteSessionController : BaseApiController
    {
        private readonly IInstituteConfigureSessionService _instituteConfigureSessionService;
        private readonly IHtmlLocalizer<InstituteSessionController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteSessionController(
            IInstituteConfigureSessionService instituteConfigureSessionService,
            IHtmlLocalizer<InstituteSessionController> htmlLocalizer)
        {
            _instituteConfigureSessionService = instituteConfigureSessionService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("session/{instituteId}")]
        public async Task<object> GetInstituteConfigureSessionByInstituteId(int instituteId)
        {
            Logger.Info("GetInstituteConfigureSessionByInstituteId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteConfigureSessionService.GetAsync(instituteId));
                return Response(result, string.Empty);
            });
        }


        [HttpPost]
        [Route("session/create")]
        public async Task<object> CreateInstituteConfigureSession([FromBody] InstituteConfigureSessionViewModel model)
        {
            Logger.Info("Institute Configure Session");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddInstituteConfigureSessionAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddInstituteConfigureSessionAsync(InstituteConfigureSessionViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _instituteConfigureSessionService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }
            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
    }
}