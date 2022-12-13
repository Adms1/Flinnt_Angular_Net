using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/template-category")]
    public class PostTemplateCategoryController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostTemplateCategoryController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTemplateCategoryController( 
            IHtmlLocalizer<PostTemplateCategoryController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }
    }
}