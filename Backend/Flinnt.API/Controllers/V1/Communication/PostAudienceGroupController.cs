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
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/audience-group")]
    public class PostAudienceGroupController : BaseApiController
    {
        private readonly IHtmlLocalizer<PostAudienceGroupController> _localizer;
        private readonly IPostAudienceGroupService _postAudienceGroupService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostAudienceGroupController(
            IPostAudienceGroupService postAudienceGroupService,
            IHtmlLocalizer<PostAudienceGroupController> htmlLocalizer)
        {
            _postAudienceGroupService = postAudienceGroupService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAllPostAudienceGroup()
        {
            Logger.Info("GetAllPostAudienceGroup list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _postAudienceGroupService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("{instituteId}/{userId}")]
        public async Task<object> GetPostAudienceGroupByInstituteIdAndUserId(int instituteId, int userId)
        {
            Logger.Info("GetPostAudienceGroupByInstituteIdAndUserId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _postAudienceGroupService.GetPostAudienceGroupByInstituteIdAndUserId(instituteId, userId));
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> CreatePostAudienceGroupAsync([FromBody] PostAudienceGroupViewModel model)
        {
            Logger.Info("AddPostAudienceGroupAsync");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddPostAudienceGroupAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostAudienceGroupAsync(PostAudienceGroupViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postAudienceGroupService.AddAsync(model);
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
        public async Task<object> UpdatePostAudienceGroupAsync([FromBody] PostAudienceGroupViewModel model)
        {
            Logger.Info("AddPostAudienceGroupAsync");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await UpdateAudienceGroupAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }
        private async Task<Tuple<bool, string, HttpStatusCode>> UpdateAudienceGroupAsync(PostAudienceGroupViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postAudienceGroupService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{audienceGroupId}")]
        public async Task<object> Delete(int audienceGroupId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postAudienceGroupService.DeleteAsync(audienceGroupId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}