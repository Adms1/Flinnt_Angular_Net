using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/template-category")]
    public class PostTemplateCategoryController : BaseApiController
    {
        private readonly IPostTemplateCategoryService _postTemplateCategoryService;
        private readonly IHtmlLocalizer<PostTemplateCategoryController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTemplateCategoryController( 
            IPostTemplateCategoryService postTemplateCategoryService,
            IHtmlLocalizer<PostTemplateCategoryController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
            _postTemplateCategoryService = postTemplateCategoryService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("template category list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postTemplateCategoryService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }
    }
}