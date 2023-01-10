using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post")]
    public class PostController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostController> _localizer;
        private readonly IPostService _postService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostController( 
            IHtmlLocalizer<PostController> htmlLocalizer,
            IPostService postService)
        {
            _postService = postService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("{instituteId}/list")]
        public async Task<object> GetFeedByInstituteId(int instituteId)
        {
            Logger.Info("post list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetAllAsync(instituteId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("{postId}/{userId}/bookmarks")]
        public async Task<object> GetBookmarkedPostByInstituteId(int postId, int userId)
        {
            Logger.Info("post list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetAllBookmarksAsync(postId, userId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("type/{postTypeId}")]
        public async Task<object> GetPostByPostTypeId(int instituteId, int postTypeId)
        {
            Logger.Info("post list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetPostByPostType(instituteId, postTypeId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("media/{mediaTypeId}")]
        public async Task<object> GetPostByMediaTypeId(int instituteId, int mediaTypeId)
        {
            Logger.Info("post list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetPostByMediaType(instituteId, mediaTypeId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("get/{postId}")]
        public async Task<object> GetById(int postId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetAsync(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("{instituteId}/approval-request/list")]
        public async Task<object> GetApprovalRequestByInstituteId(int instituteId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postService.GetApprovalRequestByInstituteId(instituteId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePost([FromBody] PostViewModel model)
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

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostAsync(PostViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postService.AddAsync(model);
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
        public async Task<object> UpdatePost([FromBody] PostViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await UpdatePostAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }
        
        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostAsync(PostViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postId}")]
        public async Task<object> Delete(int postId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postService.DeleteAsync(postId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}