using Flinnt.Domain;
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
    [Route("api/{v:apiVersion}/post/media-type")]
    public class MediaTypeController : BaseApiController
    {
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IHtmlLocalizer<MediaTypeController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MediaTypeController( 
            IHtmlLocalizer<MediaTypeController> htmlLocalizer, IMediaTypeService mediaTypeService)
        {
            _localizer = htmlLocalizer;
            _mediaTypeService = mediaTypeService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _mediaTypeService.GetAllAsync();
                return Response(result, string.Empty);
            });
        }
    }
}