using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/type")]
    public class PostTypeController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostTypeController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTypeController( 
            IHtmlLocalizer<PostTypeController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}