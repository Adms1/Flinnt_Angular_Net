using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers.V1
{
    [Route("api/{v:apiVersion}/student")]
    [ApiVersion("1.0")]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;
        private readonly IHtmlLocalizer<StudentController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StudentController(IStudentService studentService,
            IHtmlLocalizer<StudentController> htmlLocalizer)
        {
            _studentService = studentService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("Student list");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _studentService.GetAllAsync());
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
                var result = (await _studentService.GetAsync(Id));
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> Post([FromBody] CityViewModel model)
        {
            return await GetDataWithMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    return await AddStudentAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(model, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });

        }

        private async Task<Tuple<CityViewModel, string, HttpStatusCode>> AddStudentAsync(CityViewModel model)
        {
            var student = await _studentService.AddAsync(model);
            if (student != null)
            {
                return Response(student, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(student, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }
    }
}
