using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using Flinnt.Business.ViewModels;

namespace Flinnt.API.Controllers.V1
{
    [Route("api/{v:apiVersion}/parent")]
    [ApiVersion("1.0")]
    public class ParentController : BaseApiController
    {
        private readonly IParentService _parentService;
        private readonly IHtmlLocalizer<ParentController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ParentController(IParentService parentService,
            IHtmlLocalizer<ParentController> htmlLocalizer)
        {
            _parentService = parentService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("Parent list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _parentService.GetAllAsync());
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("get/{Id}")]
        public async Task<object> Get(int Id)
        {
            Logger.Info("Get");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _parentService.GetAsync(Id));
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> Post([FromBody] ParentViewModel model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddParentAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<ParentViewModel, string, HttpStatusCode>> AddParentAsync(ParentViewModel model)
        {
            var student = await _parentService.AddAsync(model);
            if (student != null)
            {
                return Response(student, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(student, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
    }
}
