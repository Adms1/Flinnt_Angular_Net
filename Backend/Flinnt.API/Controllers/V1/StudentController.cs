using Flinnt.Business.Enums.General;
using Flinnt.Business.Helpers;
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
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers.V1
{
    [Route("api/{v:apiVersion}/student")]
    [ApiVersion("1.0")]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserAccountHistoryService _userAccountHistoryService;
        private readonly IHtmlLocalizer<StudentController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StudentController(IStudentService studentService,
            IUserService userService,
            IUserInstituteService userInstituteService,
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService, 
            IHtmlLocalizer<StudentController> htmlLocalizer)
        {
            _studentService = studentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _userProfileService = userProfileService;
            _userAccountHistoryService = userAccountHistoryService;
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
        public async Task<object> Post([FromBody] StudentViewModel model)
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

        private async Task<Tuple<StudentViewModel, string, HttpStatusCode>> AddStudentAsync(StudentViewModel model)
        {
            var extStudent = await _studentService.ValidateStudent(model);

            if(extStudent.Any())
            {
                return Response(new StudentViewModel(), "Student account already exist!", HttpStatusCode.Forbidden);
            }

            var user = await _userService.GetUserByLoginId(model.EmailId);
            if (user != null)
            {
                // usertype parent or student check
                if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                {
                    // consider dublicate row
                    return Response(new StudentViewModel(), "Student account already exist!", HttpStatusCode.Forbidden);
                }
            }
            else
            {
                // user not found it will need to create and mapped
                var userRes = await _userService.AddAsync(new User
                {
                    LoginId = model.EmailId,
                    AuthenticationTypeId = (int)AutheticationTypeEnum.Edplex,
                    IsActive = true,
                    IsDeleted = false,
                    Password = "flinnt@123",
                    OneTimePassword = "flinnt@123",
                    UserTypeId = (int)UserTypes.Student,
                    RegistrationDateTime = DateTime.Now,
                    LastLoginDateTime = DateTime.Now
                });

                if (userRes != null)
                {
                    model.UserId = userRes.UserId;
                    // check relation with parent by passing parentId (userid)

                    UserProfile prtUsr = new UserProfile();
                    if (model.parentUserId != null)
                    {
                        var parentUser = await _userService.GetAsync(model.parentUserId.Value);

                        if (parentUser == null)
                        {
                            return Response(new StudentViewModel(), "Parent account not exist!", HttpStatusCode.Forbidden);
                        }

                        prtUsr = parentUser.UserProfiles.FirstOrDefault();
                    }

                    await _userProfileService.AddAsync(new UserProfile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailId = model.EmailId,
                        UserId = userRes.UserId,
                        MobileNo = model.MobileNo,
                        GenderId = model.GenderId,
                        CreateDateTime = DateTime.Now,
                        CityId = prtUsr?.CityId,
                        Address = prtUsr?.Address,
                        CountryId = prtUsr?.CountryId,
                        StateId = prtUsr?.StateId,
                        Pincode = prtUsr?.Pincode
                    });

                    await _userInstituteService.AddAsync(new UserInstitute
                    {
                        InstituteId = model.instituteId,
                        UserId = userRes.UserId,
                        UserTypeId = (int)UserTypes.Student,
                        IsActive = true,
                        CreateDateTime = DateTime.Now
                    });

                    //save userAccountHistory
                    UserAccountHistory userAccountHistory = new UserAccountHistory
                    {
                        ActionUserId = Convert.ToInt32(model.UserId),
                        HistoryAction = "UserCreatedForStudent"
                    };
                    await _userAccountHistoryService.AddAsync(userAccountHistory);
                }
            }

            var student = await _studentService.AddAsync(model);
            if (student != null)    
            {
                return Response(student, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(student, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [Route("import-roster")]
        [HttpPost, DisableRequestSizeLimit]
        public object ImportStudentRoster()
        {
            try
            {
                var file = Request.Form.Files[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                
                if (file.Length > 0)
                {
                    return Ok(fileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            return Ok();
        }

        [HttpPost]
        [Route("DataFilter")]
        public object DataFilter(DataTableAjaxPostModel model)
        {
            return _studentService.GetAllAsync().Result.GetFilteredData(model);
        }
    }
}
