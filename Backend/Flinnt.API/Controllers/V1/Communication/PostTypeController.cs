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
    [Route("api/{v:apiVersion}/post/type")]
    public class PostTypeController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostTypeController> _localizer;
        private readonly IPostTypeService _postTypeService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostTypeController(
            IPostTypeService postTypeService,
            IHtmlLocalizer<PostTypeController> htmlLocalizer)
        {
            _postTypeService = postTypeService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("post type list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postTypeService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }
    }
}