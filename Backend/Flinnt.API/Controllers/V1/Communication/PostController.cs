using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post")]
    public class PostController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostController( 
            IHtmlLocalizer<PostController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}