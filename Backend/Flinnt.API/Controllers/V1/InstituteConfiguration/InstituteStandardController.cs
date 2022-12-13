using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
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
    public class InstituteStandardController : BaseApiController
    {
        private readonly IStandardService _standardService;
        private readonly IHtmlLocalizer<InstituteStandardController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteStandardController(
            IStandardService standardService,
            IHtmlLocalizer<InstituteStandardController> htmlLocalizer)
        {
            _standardService = standardService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("standard/list")]
        public async Task<object> GetAllStandard()
        {
            Logger.Info("Standard list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _standardService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}