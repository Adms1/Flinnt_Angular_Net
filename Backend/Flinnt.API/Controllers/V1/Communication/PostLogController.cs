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
    [Route("api/{v:apiVersion}/post/log")]
    public class PostLogController : BaseApiController
    {
        private readonly IPostLogService _postLogService;
        private readonly IHtmlLocalizer<PostLogController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostLogController( 
            IHtmlLocalizer<PostLogController> htmlLocalizer, IPostLogService postLogService)
        {
            _localizer = htmlLocalizer;
            _postLogService = postLogService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAllAsync()
        {
            Logger.Info("postLog list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postLogService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }
    }
}