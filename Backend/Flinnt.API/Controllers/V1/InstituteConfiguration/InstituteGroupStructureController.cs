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
    public class InstituteGroupStructureController : BaseApiController
    {
        private readonly IGroupStructureService _groupStructureService;
        private readonly IHtmlLocalizer<InstituteGroupStructureController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteGroupStructureController(IGroupStructureService groupStructureService,
            IHtmlLocalizer<InstituteGroupStructureController> htmlLocalizer)
        {
            _groupStructureService = groupStructureService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("group-structure/list")]
        public async Task<object> GetAllGroupStructure()
        {
            Logger.Info("GroupStructure list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _groupStructureService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}