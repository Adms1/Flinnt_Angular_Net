using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/log")]
    public class PostLogController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostLogController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostLogController( 
            IHtmlLocalizer<PostLogController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}