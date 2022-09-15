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
    [Route("api/{v:apiVersion}/country")]
    public class CountryController : BaseApiController
    {
        private readonly ICountryService _countryService;
        private readonly IHtmlLocalizer<CountryController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CountryController(ICountryService countryService, 
            IHtmlLocalizer<CountryController> htmlLocalizer)
        {
            _countryService = countryService;
            _localizer = htmlLocalizer;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("GetAll");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _countryService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get/{Id}")]
        public async Task<object> Get(int Id)
        {
            Logger.Info("Get");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _countryService.GetAsync(Id));
                return Response(result, string.Empty);
            });
        }

    }
}