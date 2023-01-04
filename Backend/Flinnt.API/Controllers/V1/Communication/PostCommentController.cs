using Flinnt.Business.ViewModels.General;
using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/comment")]
    public class PostCommentController : BaseApiController
    {
        private readonly IPostCommentService _postCommentService;
        private readonly IHtmlLocalizer<PostCommentController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostCommentController( 
            IHtmlLocalizer<PostCommentController> htmlLocalizer,
            IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("get/{postId}")]
        public async Task<object> GetByPostId(int postId)
        {
            Logger.Info("post comments");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postCommentService.GetAsync(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("{postId}/approval-request/list")]
        public async Task<object> GetApprovalRequestByPostId(int postId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postCommentService.GetApprovalRequestByPostId(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostComment([FromBody] PostCommentViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostAsync(PostCommentViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postCommentService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }

            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("update")]
        public async Task<object> UpdatePostComment([FromBody] PostCommentViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    await UpdatePostCommentAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostCommentAsync(PostCommentViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postCommentService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postCommentId}")]
        public async Task<object> Delete(int postCommentId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postCommentService.DeleteAsync(postCommentId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}