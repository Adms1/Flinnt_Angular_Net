using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
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
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteController(IInstituteService instituteService)
        {
            _instituteService = instituteService;
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
    }
}
