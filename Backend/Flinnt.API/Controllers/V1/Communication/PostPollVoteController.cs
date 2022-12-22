using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/poll/vote")]
    public class PostPollVoteController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostPollVoteController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollVoteController( 
            IHtmlLocalizer<PostPollVoteController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}