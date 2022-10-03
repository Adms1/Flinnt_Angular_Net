﻿using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/institute/configure")]
    public class InstituteConfigurationController : BaseApiController
    {
        private readonly IGroupStructureService _groupStructureService;
        private readonly IBoardService _boardService;
        private readonly IMediumService _mediumService;
        private readonly IStandardService _standardService;
        private readonly IInstituteGroupService _instituteGroupService;
        private readonly IInstituteDivisionService _instituteDivisionService;
        private readonly IHtmlLocalizer<CityController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public InstituteConfigurationController(IGroupStructureService groupStructureService,
            IBoardService boardService,
            IMediumService mediumService,
            IStandardService standardService,
            IInstituteGroupService instituteGroupService,
            IInstituteDivisionService instituteDivisionService,
            IHtmlLocalizer<CityController> htmlLocalizer)
        {
            _groupStructureService = groupStructureService;
            _boardService = boardService;
            _mediumService = mediumService;
            _standardService = standardService;
            _instituteGroupService = instituteGroupService;
            _instituteDivisionService = instituteDivisionService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("group-structure/list")]
        public async Task<object> GetAllGroupStructure()
        {
            Logger.Info("GroupStructure list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _groupStructureService.GetAllAsync());
                return Response(result, string.Empty);
            });
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

        [HttpGet]
        [Route("medium/list")]
        public async Task<object> GetAllMedium()
        {
            Logger.Info("Medium list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _mediumService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("standard/list")]
        public async Task<object> GetAllStandard()
        {
            Logger.Info("Standard list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _standardService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        #region InstituteGrop

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
        public async Task<object> CreateInstituteGroup(InstituteGroupViewModel model)
        {
            Logger.Info("Institute Group");
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return model.InstituteGroupId <= 0 ? await AddInstituteGroupAsync(model) : await UpdateInstituteGroupAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<InstituteGroupViewModel, string, HttpStatusCode>> AddInstituteGroupAsync(InstituteGroupViewModel model)
        {
            var instituteGroup = await _instituteGroupService.AddAsync(model);
            if (instituteGroup != null)
            {
                return Response(instituteGroup, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(instituteGroup, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        private async Task<Tuple<InstituteGroupViewModel, string, HttpStatusCode>> UpdateInstituteGroupAsync(InstituteGroupViewModel model)
        {
            var flag = await _instituteGroupService.UpdateAsync(model);
            if (flag)
                return Response(model, _localizer["RecordUpdeteSuccess"].Value.ToString());
            return Response(model, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
        #endregion

        #region InstituteDivision

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
        [Route("division/{instituteGroupId}")]
        public async Task<object> GetInstituteDivisionByInstituteGroupId(int instituteGroupId)
        {
            Logger.Info("GetInstituteDivisionByInstituteGroupId");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _instituteDivisionService.GetByInstituteGroupIdAsync(instituteGroupId));
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("division/create")]
        public async Task<object> CreateInstituteDivision(InstituteDivisionViewModel model)
        {
            Logger.Info("Institute Group");
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return model.InstituteGroupId <= 0 ? await AddInstituteDivisionAsync(model) : await UpdateInstituteDivisionAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<InstituteDivisionViewModel, string, HttpStatusCode>> AddInstituteDivisionAsync(InstituteDivisionViewModel model)
        {
            var instituteGroup = await _instituteDivisionService.AddAsync(model);
            if (instituteGroup != null)
            {
                return Response(instituteGroup, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(instituteGroup, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        private async Task<Tuple<InstituteDivisionViewModel, string, HttpStatusCode>> UpdateInstituteDivisionAsync(InstituteDivisionViewModel model)
        {
            var flag = await _instituteDivisionService.UpdateAsync(model);
            if (flag)
                return Response(model, _localizer["RecordUpdeteSuccess"].Value.ToString());
            return Response(model, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("division/delete/{instituteDivisionId}")]
        public async Task<object> Delete(int instituteDivisionId)
        {
            return await GetDataWithMessage(async () =>
            {
                var flag = await _instituteDivisionService.DeleteAsync(instituteDivisionId);
                if (flag)
                    return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                return Response(new BooleanResponseModel { Value = flag }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }

        #endregion
    }
}