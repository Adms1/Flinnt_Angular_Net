using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/user")]
    public class PostUserController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostUserController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostUserController( 
            IHtmlLocalizer<PostUserController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}