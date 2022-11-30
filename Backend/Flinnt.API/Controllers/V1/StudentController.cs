using ExcelDataReader;
using Flinnt.Business.Enums.General;
using Flinnt.Business.Helpers;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Background;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        private readonly IUserParentChildRelationshipService _userParentChildRelationshipService;
        private readonly IUserInstituteGroupService _userInstituteGroupService;
        private readonly IHtmlLocalizer<StudentController> _localizer;
        private readonly IBackgroundService _backgroundService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StudentController(IStudentService studentService,
            IUserService userService,
            IUserInstituteService userInstituteService,
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService,
            IUserInstituteGroupService userInstituteGroupService,
            IUserParentChildRelationshipService userParentChildRelationshipService,
            IBackgroundService backgroundService,
            IHtmlLocalizer<StudentController> htmlLocalizer)
        {
            _studentService = studentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _userProfileService = userProfileService;
            _userAccountHistoryService = userAccountHistoryService;
            _userParentChildRelationshipService = userParentChildRelationshipService;
            _userInstituteGroupService = userInstituteGroupService;
            _backgroundService = backgroundService;
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
            var currentInstituteID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "InstituteId")?.Value;
            var currentUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;

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

                        await _userParentChildRelationshipService.AddAsync(new UserParentChildRelationshipModel
                        {
                            ChildUserId = userRes.UserId,
                            ChildUserTypeId = (int)UserTypes.Student,
                            ParentUserId = model.parentUserId.Value,
                            InstituteId = Convert.ToInt32(currentInstituteID),
                            ParentUserTypeId = (int)UserTypes.Parent,
                        });
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
                        InstituteId = Convert.ToInt32(currentInstituteID),
                        UserId = userRes.UserId,
                        UserTypeId = (int)UserTypes.Student,
                        IsActive = true,
                        CreateDateTime = DateTime.Now
                    });

                    await _userInstituteGroupService.AddAsync(new UserInstituteGroupModel
                    {
                        UserId = userRes.UserId,
                        InstituteId = Convert.ToInt32(currentInstituteID),
                        InstituteGroupId = model.instituteGroupId,
                        InstituteDivisionId = model.instituteDivisionId
                    });

                    //save userAccountHistory
                    UserAccountHistory userAccountHistory = new UserAccountHistory
                    {
                        UserId = model.UserId,
                        ActionUserId = Convert.ToInt32(currentUserID),
                        HistoryAction = "UserCreatedForStudent",
                        ClientIp = ipAddress.ToString()
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

        [Route("validate-student-import")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<object> ValidateStudentImport(IFormFile file)
        {
            return await GetDataWithMessage(() =>
            {
                var httpRequest = file;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                List<StudentViewModel> students = new List<StudentViewModel>();

                try
                {
                    if (httpRequest.Length > 0)
                    {
                        var Inputfile = httpRequest;
                        using (var stream = Inputfile.OpenReadStream())
                        {
                            FileStream = stream;

                            if (Inputfile != null && FileStream != null)
                            {
                                if (Inputfile.FileName.EndsWith(".xls"))
                                    reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                                else if (Inputfile.FileName.EndsWith(".xlsx"))
                                    reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                                else
                                    return Task.FromResult(Response(students, "The file format is not supported.", HttpStatusCode.InternalServerError));

                                dsexcelRecords = reader.AsDataSet();
                                reader.Close();

                                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                                {
                                    string noData = "<<no-data>>";
                                    var dataSet = dsexcelRecords.Tables[0];
                                    foreach (DataRow row in dataSet.Rows)
                                    {
                                        var _firstName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "First Name (Mandatory)")).ToString();
                                        var _lastName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Last Name (Mandatory)")).ToString();
                                        var _emailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Email Address (Mandatory)")).ToString();
                                        var _mobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Mobile Number")).ToString();
                                        var _dob = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "DOB")).ToString();
                                        var _gendar = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Gendar(Mandatory)")).ToString();
                                        var _rollNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Roll No.")).ToString();
                                        var _grNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "G.R.No.")).ToString();
                                        var _parentEmailAddress = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent Email Address")).ToString();

                                        var student = new StudentViewModel
                                        {
                                            FirstName = !string.IsNullOrEmpty(_firstName) ? _firstName : noData,
                                            LastName = !string.IsNullOrEmpty(_lastName) ? _lastName : noData,
                                            EmailId = !string.IsNullOrEmpty(_emailId) ? _emailId : noData,
                                            MobileNo = !string.IsNullOrEmpty(_mobileNo) ? _mobileNo : noData,
                                            Dob = !string.IsNullOrEmpty(_dob) ? _dob : noData,
                                            GenderId = !string.IsNullOrEmpty(_gendar) ? (_gendar == "M" ? (byte)1 : (byte)2) : (byte)0,
                                            RollNo = !string.IsNullOrEmpty(_rollNo) ? _rollNo : noData,
                                            Grno = !string.IsNullOrEmpty(_grNo) ? _grNo : noData,
                                            ImportStatus = "Valid"
                                        };

                                        var summary = GenerateImportSummaryAsync(student);
                                        if (summary.Result.Count > 0)
                                        {
                                            student.ImportStatus = "Invalid";
                                            student.ImportSummary.AddRange(summary.Result);
                                        }
                                        students.Add(student);
                                    }
                                    students.Remove(students.FirstOrDefault());
                                    
                                    return Task.FromResult(Response(students, "", HttpStatusCode.OK));
                                }
                                else
                                {
                                    return Task.FromResult(Response(students, "Selected file is empty.", HttpStatusCode.InternalServerError));
                                }
                            }
                            else
                            {
                                return Task.FromResult(Response(students, "Invalid File.", HttpStatusCode.InternalServerError));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Task.FromResult(Response(students, $"Internal server error: {ex}", HttpStatusCode.InternalServerError));
                }
                return Task.FromResult(Response(students, "Something went wrong!!", HttpStatusCode.InternalServerError));
            });
        }

        private async Task<List<StudentImportSummary>> GenerateImportSummaryAsync(StudentViewModel studentViewModel)
        {
            List<StudentImportSummary> importSummaries = new List<StudentImportSummary>();

            if (string.IsNullOrEmpty(studentViewModel.FirstName)
               || studentViewModel.FirstName.Equals("<<no-data>>"))
            {
                importSummaries.Add(new StudentImportSummary
                {
                    FieldName = "First Name",
                    Message = "First Name could not be empty"
                });
            }

            if (string.IsNullOrEmpty(studentViewModel.LastName)
                || studentViewModel.LastName.Equals("<<no-data>>"))
            {
                importSummaries.Add(new StudentImportSummary
                {
                    FieldName = "Last Name",
                    Message = "Last Name could not be empty"
                });
            }

            if (studentViewModel.GenderId == 0)
            {
                importSummaries.Add(new StudentImportSummary
                {
                    FieldName = "Gendar",
                    Message = "The Gendar could not be empty and it must be M (for Male) and F (for Female)"
                });
            }

            if (string.IsNullOrEmpty(studentViewModel.ParentPrimaryEmailId)
                || studentViewModel.ParentPrimaryEmailId.Equals("<<no-data>>"))
            {
                importSummaries.Add(new StudentImportSummary
                {
                    FieldName = "Parent Primary email address",
                    Message = "Parent Primary email address could not be blank"
                });
            }
            else
            {
                // call APIs to check if user exists in db
                var user = await _userService.GetUserByLoginId(studentViewModel.ParentPrimaryEmailId);
                if (user != null)
                {
                    // usertype parent or student check
                    if (!user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent).Any())
                    {
                        // consider dublicate row
                        importSummaries.Add(new StudentImportSummary
                        {
                            FieldName = "Primary email address",
                            Message = "There is no parent account with the provided Parent Email Address. You have to first create a parent account."
                        });
                    }
                }
            }

            if (!string.IsNullOrEmpty(studentViewModel.MobileNo)
                || studentViewModel.MobileNo.Equals("<<no-data>>"))
            {
                if (studentViewModel.MobileNo.Trim().Length > 10)
                {
                    importSummaries.Add(new StudentImportSummary
                    {
                        FieldName = "Mobile no",
                        Message = "The mobile no must contain 10 digits"
                    });
                }
            }

            if (string.IsNullOrEmpty(studentViewModel.EmailId)
                || studentViewModel.EmailId.Equals("<<no-data>>"))
            {
                importSummaries.Add(new StudentImportSummary
                {
                    FieldName = "Email Address",
                    Message = "The Email address could not be blank"
                });
            }
            else
            {
                if(studentViewModel.EmailId == studentViewModel.ParentPrimaryEmailId)
                {
                    importSummaries.Add(new StudentImportSummary
                    {
                        FieldName = "Email Address",
                        Message = "The Student Email address and Parent Email Address could not be same"
                    });
                }
            }

            return importSummaries;
        }

        [Route("import-roster")]
        [HttpPost]
        public async Task<object> ImportStudentRoster(List<StudentViewModel> studentViewModels)
        {
            return await GetDataWithMessage(async () =>
            {
                _backgroundService.EnqueueJob<IBackgroundStudentJobs>(m => m.ImportStudents(studentViewModels));
                return Response(true, string.Empty);
            });
        }

        [HttpPost]
        [Route("DataFilter")]
        public object DataFilter(DataTableAjaxPostModel model)
        {
            return _studentService.GetAllAsync().Result.GetFilteredData(model);
        }
    }
}
