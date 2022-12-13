using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/poll/option")]
    public class PostPollOptionController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostPollOptionController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollOptionController( 
            IHtmlLocalizer<PostPollOptionController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}