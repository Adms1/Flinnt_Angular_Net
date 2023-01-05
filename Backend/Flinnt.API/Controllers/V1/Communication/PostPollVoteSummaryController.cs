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
    [Route("api/{v:apiVersion}/post/poll/vote-summary")]
    public class PostPollVoteSummaryController : BaseApiController
    {
        private readonly IPostPollVoteSummaryService _postPollVoteSummaryService;
        private readonly IHtmlLocalizer<PostPollVoteSummaryController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostPollVoteSummaryController( 

            IHtmlLocalizer<PostPollVoteSummaryController> htmlLocalizer, IPostPollVoteSummaryService postPollVoteSummaryService)
        {
            _localizer = htmlLocalizer;
            _postPollVoteSummaryService = postPollVoteSummaryService;
        }

        [HttpGet]
        [Route("get/{postPollId}")]
        public async Task<object> GetById(int postPollId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postPollVoteSummaryService.GetAsync(postPollId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostPollVote([FromBody] PostPollVoteSummaryViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostPollVoteSummaryAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostPollVoteSummaryAsync(PostPollVoteSummaryViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postPollVoteSummaryService.AddAsync(model);
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
        public async Task<object> UpdatePostPollVote([FromBody] PostPollVoteSummaryViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await UpdatePostPollVoteSummaryAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostPollVoteSummaryAsync(PostPollVoteSummaryViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postPollVoteSummaryService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postPollVoteId}")]
        public async Task<object> Delete(int postPollVoteSummaryId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postPollVoteSummaryService.DeleteAsync(postPollVoteSummaryId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}