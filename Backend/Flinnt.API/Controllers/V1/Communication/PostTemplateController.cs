using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/template")]
    public class PostTemplateController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostTemplateController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTemplateController( 
            IHtmlLocalizer<PostTemplateController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}