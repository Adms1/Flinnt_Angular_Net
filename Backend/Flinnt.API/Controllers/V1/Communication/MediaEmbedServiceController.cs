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
    [Route("api/{v:apiVersion}/post/media-embed")]
    public class MediaEmbedServiceController : BaseApiController
    {
        private readonly IMediaEmbedService _mediaEmbedService;
        private readonly IHtmlLocalizer<MediaEmbedServiceController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MediaEmbedServiceController( 
            IHtmlLocalizer<MediaEmbedServiceController> htmlLocalizer, IMediaEmbedService mediaEmbedService)
        {
            _localizer = htmlLocalizer;
           _mediaEmbedService = mediaEmbedService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _mediaEmbedService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }
    }
}