using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
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
    [Route("api/{v:apiVersion}/post/user")]
    public class PostUserController : BaseApiController
    {
        private readonly IPostUserService _postUserService;
        private readonly IHtmlLocalizer<PostUserController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostUserController( 
            IHtmlLocalizer<PostUserController> htmlLocalizer, IPostUserService postUserService)
        {
            _localizer = htmlLocalizer;
            _postUserService = postUserService;
        }

        [HttpGet]
        [Route("{postId}/list")]
        public async Task<object> GetPostUserByPostId(int postId)
        {
            Logger.Info("post user list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postUserService.GetAllAsync(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("get/{postUserId}")]
        public async Task<object> GetById(long postUserId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postUserService.GetAsync(postUserId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostUser([FromBody] PostUserViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostUserAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostUserAsync(PostUserViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postUserService.AddAsync(model);
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
        public async Task<object> UpdatePostUser([FromBody] PostUserViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await UpdatePostUserAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostUserAsync(PostUserViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postUserService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postUserId}")]
        public async Task<object> Delete(long postUserId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postUserService.DeleteAsync(postUserId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}