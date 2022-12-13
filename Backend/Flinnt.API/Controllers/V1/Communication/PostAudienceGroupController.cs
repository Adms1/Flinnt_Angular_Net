using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/audience-group")]
    public class PostAudienceGroupController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostAudienceGroupController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostAudienceGroupController( 
            IHtmlLocalizer<PostAudienceGroupController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}