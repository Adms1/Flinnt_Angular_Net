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
    public class MediaEmbedServiceController : BaseApiController
    {
        private readonly IHtmlLocalizer<MediaEmbedServiceController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MediaEmbedServiceController( 
            IHtmlLocalizer<MediaEmbedServiceController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}