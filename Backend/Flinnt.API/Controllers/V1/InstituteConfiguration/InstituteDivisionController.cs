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
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteDivisionController : BaseApiController
    {
        private readonly IInstituteDivisionService _instituteDivisionService;
        private readonly IHtmlLocalizer<InstituteDivisionController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteDivisionController(
            IInstituteDivisionService instituteDivisionService,
            IHtmlLocalizer<InstituteDivisionController> htmlLocalizer)
        {
            _instituteDivisionService = instituteDivisionService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("division/list")]
        public async Task<object> GetAllInstituteDivision()
        {
            Logger.Info("GetAllInstituteDivision list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteDivisionService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("division/{instituteId}")]
        public async Task<object> GetInstituteDivisionByInstituteId(int instituteId)
        {
            Logger.Info("GetInstituteDivisionByInstituteId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteDivisionService.GetDivisionByInstituteIdAsync(instituteId));
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("division/{instituteId}/{instituteGroupId}")]
        public async Task<object> GetInstituteDivisionByInstituteGroupId(int instituteId, int instituteGroupId)
        {
            Logger.Info("GetInstituteDivisionByInstituteGroupId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteDivisionService.GetDivisionByInstituteIdAsync(instituteId));

                if(result != null)
                {
                    if(instituteGroupId > 0)
                    {
                        result = result.Where(x => x.InstituteGroupId == instituteGroupId).ToList();
                    }
                }
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("division/create")]
        public async Task<object> CreateInstituteDivision([FromBody]InstituteDivisionViewModel model)
        {
            Logger.Info("Institute Group");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddInstituteDivisionAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddInstituteDivisionAsync(InstituteDivisionViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _instituteDivisionService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }
            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdateInstituteDivisionAsync(InstituteDivisionViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _instituteDivisionService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("division/delete/{instituteDivisionId}")]
        public async Task<object> Delete(int instituteDivisionId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _instituteDivisionService.DeleteAsync(instituteDivisionId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}