using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/poll")]
    public class PostPollController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostPollController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollController( 
            IHtmlLocalizer<PostPollController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}