using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Transactions;

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

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostLog([FromBody] PostLogViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostLogAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostLogAsync(PostLogViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postLogService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }

            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
    }
}