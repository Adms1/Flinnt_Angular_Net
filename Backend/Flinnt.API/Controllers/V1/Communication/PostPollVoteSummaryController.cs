using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/poll/vote-summary")]
    public class PostPollVoteSummaryController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostPollVoteSummaryController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollVoteSummaryController( 
            IHtmlLocalizer<PostPollVoteSummaryController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}