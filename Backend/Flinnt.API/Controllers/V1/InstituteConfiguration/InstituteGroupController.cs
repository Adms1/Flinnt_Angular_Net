using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Services;
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
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteGroupController : BaseApiController
    {
        private readonly IInstituteGroupService _instituteGroupService;
        private readonly IHtmlLocalizer<InstituteGroupController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteGroupController(
            IInstituteGroupService instituteGroupService,
            IHtmlLocalizer<InstituteGroupController> htmlLocalizer)
        {
            _instituteGroupService = instituteGroupService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("group/list")]
        public async Task<object> GetAllInstituteGroup()
        {
            Logger.Info("GetAllInstituteGroup list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteGroupService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("group/get")]
        public async Task<object> GetInstituteGroup(int instituteId, int boardId, int mediumId, int standardId)
        {
            Logger.Info("GetInstituteGroupByInstituteId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteGroupService.GetByInstituteIdAsync(instituteId));

                if(result != null)
                {
                    if(boardId > 0)
                    {
                        result = result.Where(x => x.BoardId == boardId).ToList();
                    }
                    if(mediumId > 0)
                    {
                        result = result.Where(x => x.MediumId == mediumId).ToList();
                    }
                    if(standardId > 0)
                    {
                        result = result.Where(x => x.StandardId == standardId).ToList();
                    }
                }
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("group/{instituteId}")]
        public async Task<object> GetInstituteGroupByInstituteId(int instituteId)
        {
            Logger.Info("GetInstituteGroupByInstituteId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteGroupService.GetByInstituteIdAsync(instituteId));
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("group/create")]
        public async Task<object> CreateInstituteGroup([FromBody]InstituteGroupViewModel model)
        {
            Logger.Info("Institute Group");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return model.InstituteGroupId <= 0 ? await AddInstituteGroupAsync(model) : await UpdateInstituteGroupAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddInstituteGroupAsync(InstituteGroupViewModel model)
        {
            using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _instituteGroupService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }
            
            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdateInstituteGroupAsync(InstituteGroupViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _instituteGroupService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
    }
}