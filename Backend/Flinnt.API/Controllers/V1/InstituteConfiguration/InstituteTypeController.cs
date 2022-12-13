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
    public class InstituteTypeController : BaseApiController
    {
        private readonly IInstituteTypeService _instituteTypeService;
        private readonly IHtmlLocalizer<InstituteTypeController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteTypeController(
            IInstituteTypeService instituteTypeService,
            IHtmlLocalizer<InstituteTypeController> htmlLocalizer)
        {
            _instituteTypeService = instituteTypeService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("type/list")]
        public async Task<object> GetAllType()
        {
            Logger.Info("GetAllType list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteTypeService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}