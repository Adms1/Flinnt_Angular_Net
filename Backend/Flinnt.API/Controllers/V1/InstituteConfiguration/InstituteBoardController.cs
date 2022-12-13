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
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteBoardController : BaseApiController
    {
        private readonly IBoardService _boardService;
        private readonly IHtmlLocalizer<InstituteBoardController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteBoardController(
            IBoardService boardService,
            IHtmlLocalizer<InstituteBoardController> htmlLocalizer)
        {
            _boardService = boardService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("board/list")]
        public async Task<object> GetAllBoard()
        {
            Logger.Info("Board list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _boardService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }
    }
}