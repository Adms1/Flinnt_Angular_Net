using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
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
    [Route("api/{v:apiVersion}/post/poll")]
    public class PostPollController : BaseApiController
    {
        private readonly IPostPollService _postPollService;
        private readonly IHtmlLocalizer<PostPollController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollController(
            IPostPollService postPollService,
            IHtmlLocalizer<PostPollController> htmlLocalizer)
        {
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("{postId}/list")]
        public async Task<object> GetPollByPostId(int postId)
        {
            Logger.Info("poll list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postPollService.GetAllAsync(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("get/{postPollId}")]
        public async Task<object> GetById(int postPollId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postPollService.GetAsync(postPollId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostPoll([FromBody] PostPollViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostPollAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostPollAsync(PostPollViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postPollService.AddAsync(model);
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
        public async Task<object> UpdatePostPoll([FromBody] PostPollViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    await UpdatePostPollAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostPollAsync(PostPollViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postPollService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postPollId}")]
        public async Task<object> Delete(int postPollId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postPollService.DeleteAsync(postPollId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}