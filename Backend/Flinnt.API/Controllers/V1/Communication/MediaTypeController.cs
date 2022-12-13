using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/user")]
    public class MediaTypeController : BaseApiController
    {
        private readonly IHtmlLocalizer<MediaTypeController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MediaTypeController( 
            IHtmlLocalizer<MediaTypeController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}