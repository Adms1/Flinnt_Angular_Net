using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
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
    [Route("api/{v:apiVersion}/state")]
    public class StateController : BaseApiController
    {
        private readonly IStateService _stateService;
        private readonly IHtmlLocalizer<StateController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StateController(IStateService stateService, IHtmlLocalizer<StateController> htmlLocalizer)
        {
            _stateService = stateService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("GetAll");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _stateService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }


        [HttpGet]
        [Route("get/{Id}")]
        public async Task<object> Get(int Id)
        {
            Logger.Info("Get");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _stateService.GetAsync(Id));
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("get-by-countryId/{CountryId}")]
        public async Task<object> GetByCountryId(int CountryId)
        {
            Logger.Info("Get");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _stateService.GetByCountryIdAsync(CountryId));
                return Response(result, string.Empty);
            });
        }

    }
}