using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/comment")]
    public class PostCommentController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostCommentController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostCommentController( 
            IHtmlLocalizer<PostCommentController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}