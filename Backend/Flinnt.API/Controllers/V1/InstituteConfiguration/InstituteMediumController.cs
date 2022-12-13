using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteMediumController : BaseApiController
    {
        private readonly IMediumService _mediumService;
        private readonly IHtmlLocalizer<InstituteMediumController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteMediumController(
            IMediumService mediumService,
            IHtmlLocalizer<InstituteMediumController> htmlLocalizer)
        {
            _mediumService = mediumService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("medium/list")]
        public async Task<object> GetAllMedium()
        {
            Logger.Info("Medium list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _mediumService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}