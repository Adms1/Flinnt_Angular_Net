using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/media")]
    public class PostMediaController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostMediaController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostMediaController( 
            IHtmlLocalizer<PostMediaController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}