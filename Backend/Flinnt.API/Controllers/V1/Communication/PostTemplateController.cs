using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/template")]
    public class PostTemplateController : BaseApiController
    {
        private readonly IPostTemplateService _postTemplateService;
        private readonly IHtmlLocalizer<PostTemplateController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTemplateController( 
            IPostTemplateService postTemplateService,
            IHtmlLocalizer<PostTemplateController> htmlLocalizer)
        {
            _postTemplateService = postTemplateService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("template list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postTemplateService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("{templateCategoryId}")]
        public async Task<object> GetTemplateByCategoryId(int templateCategoryId)
        {
            Logger.Info("template list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postTemplateService.GetByCategoryIdAsync(templateCategoryId);
                return Response(result, string.Empty);
            });
        }
    }
}