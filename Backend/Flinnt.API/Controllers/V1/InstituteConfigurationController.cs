using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteConfigurationController : BaseApiController
    {
        private readonly IGroupStructureService _groupStructureService;
        private readonly IBoardService _boardService;
        private readonly IHtmlLocalizer<CityController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteConfigurationController(IGroupStructureService groupStructureService,
            IBoardService boardService,
            IHtmlLocalizer<CityController> htmlLocalizer)
        {
            _groupStructureService = groupStructureService;
            _boardService = boardService;
            _localizer = htmlLocalizer;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("board/list")]
        public async Task<object> GetAllBoard()
        {
            Logger.Info("Board list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _boardService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}