using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using Flinnt.Business.ViewModels;
using Flinnt.Business.Helpers;
using System.Data;
using Flinnt.Business.Enums.General;
using ExcelDataReader;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Collections.Generic;
using Flinnt.Business.ViewModels.General;

namespace Flinnt.API.Controllers.V1
{
    [Route("api/{v:apiVersion}/parent")]
    [ApiVersion("1.0")]
    public class ParentController : BaseApiController
    {
        private readonly IParentService _parentService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserInstituteService _userInstituteService;
        private readonly ICityService _cityService;
        private readonly IHtmlLocalizer<ParentController> _localizer;
        private readonly IUserAccountHistoryService _userAccountHistoryService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ParentController(IParentService parentService,
            IUserService userService, 
            IUserInstituteService userInstituteService,
            ICityService cityService, 
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService,
            IHtmlLocalizer<ParentController> htmlLocalizer)
        {
            _parentService = parentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userProfileService = userProfileService;
            _localizer = htmlLocalizer;
            _userAccountHistoryService = userAccountHistoryService;
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
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var currentInstituteID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "InstituteId")?.Value;
            var currentUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var extStudent = await _parentService.ValidateParent(model);

            if (extStudent.Any())
            {
                return Response(new ParentViewModel(), "Parent account already exist!", HttpStatusCode.Forbidden);
            }

            var user = await _userService.GetUserByLoginId(model.PrimaryEmailId);
            if (user != null)
            {
                // usertype parent or student check
                if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                {
                    // consider dublicate row
                    return Response(new ParentViewModel(), "User account already exist!", HttpStatusCode.Forbidden);
                }
                model.UserId = user.UserId;

                var existingCity = await _cityService.GetByCityNameAsync(model.CityName);

                if (existingCity == null)
                {
                    CityViewModel cityViewModel = new CityViewModel
                    {
                        CityName = model.CityName,
                        CreateDateTime = DateTime.Now,
                        StateId = model.StateId.Value,
                        IsActive = true
                    };
                    var city = await _cityService.AddAsync(cityViewModel);

                    model.CityId = city.CityId;
                }
                else
                {
                    model.CityId = existingCity.CityId;
                }
            }
            else
            {
                // user not found it will need to create and mapped
                var userRes = await _userService.AddAsync(new User
                {
                    LoginId = model.PrimaryEmailId,
                    AuthenticationTypeId = (int)AutheticationTypeEnum.Edplex,
                    IsActive = true,
                    IsDeleted = false,
                    Password = "flinnt@123",
                    OneTimePassword = "flinnt@123",
                    UserTypeId = (int)UserTypes.Parent,
                    RegistrationDateTime = DateTime.Now,
                    LastLoginDateTime = DateTime.Now
                });

                if (userRes != null)
                {
                    //save city
                    var existingCity = await _cityService.GetByCityNameAsync(model.CityName);

                    if (existingCity == null)
                    {
                        CityViewModel cityViewModel = new CityViewModel
                        {
                            CityName = model.CityName,
                            CreateDateTime = DateTime.Now,
                            StateId = model.StateId.Value,
                            IsActive = true
                        };
                        var city = await _cityService.AddAsync(cityViewModel);

                        model.CityId = city.CityId;
                    }
                    else
                    {
                        model.CityId = existingCity.CityId;
                    }

                    model.UserId = userRes.UserId;
                    await _userProfileService.AddAsync(new UserProfile
                    {
                        FirstName = model.Parent1FirstName,
                        LastName = model.Parent1LastName,
                        EmailId = model.PrimaryEmailId,
                        UserId = model.UserId,
                        MobileNo = model.PrimaryMobileNo,
                        GenderId = model.Parent1Relationship == "Male" ? (byte)GenderEnum.Male : (byte)GenderEnum.Female,
                        CreateDateTime = DateTime.Now,
                        CityId = model.CityId,
                        Address = model.AddressLine1 +", "+ model.AddressLine2,
                        CountryId = (byte)model.CountryId,
                        StateId = (byte)model.StateId,
                        Pincode = model.Pincode
                    });
                }
            }

            var parent = await _parentService.AddAsync(model);
            if (parent != null)
            {
                await _userInstituteService.AddAsync(new UserInstitute
                {
                    InstituteId = Convert.ToInt32(currentInstituteID),
                    UserId = model.UserId,
                    UserTypeId = (int)UserTypes.Parent,
                    IsActive = true,
                    CreateDateTime = DateTime.Now
                });

                //save userAccountHistory

                UserAccountHistory userAccountHistory = new UserAccountHistory
                {
                    UserId = Convert.ToInt32(model.UserId),
                    ActionUserId = Convert.ToInt32(currentUserID),
                    HistoryAction = "UserCreatedForParent",
                    ClientIp = ipAddress.ToString()
                };
                await _userAccountHistoryService.AddAsync(userAccountHistory);

                return Response(parent, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(parent, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [Route("import-roster")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<object> ImportParentRoster()
        {
            return await GetDataWithMessage(() =>
            {
                var httpRequest = Request.Form.Files;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                List<ParentViewModel> parents = new List<ParentViewModel>();

                try
                {
                    if (httpRequest.Count > 0)
                    {
                        var Inputfile = httpRequest[0];
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
                                    return Task.FromResult(Response(parents, "The file format is not supported.", HttpStatusCode.InternalServerError));

                                dsexcelRecords = reader.AsDataSet();
                                reader.Close();

                                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                                {
                                    var dataSet = dsexcelRecords.Tables[0];
                                    foreach (DataRow row in dataSet.Rows)
                                    {
                                        parents.Add(new ParentViewModel
                                        {
                                            Parent1FirstName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - First Name (Mandatory)")).ToString(),
                                            Parent1LastName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Last Name (Mandatory)")).ToString(),
                                            Parent1EmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Email Id")).ToString(),
                                            Parent1MobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Mobile No")).ToString(),
                                            Parent1Relationship = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Father (F) OR Mother (M) (Mandatory)")).ToString(),
                                            Parent2FirstName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - First Name")).ToString(),
                                            Parent2LastName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Last Name")).ToString(),
                                            Parent2EmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Email Id")).ToString(),
                                            Parent2MobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Mobile No")).ToString(),
                                            Parent2Relationship = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Single Parent (Y/N)")).ToString(),
                                            PrimaryEmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Primary Email Address (Mandatory)")).ToString(),
                                            PrimaryMobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Mobile No")).ToString(),
                                            AddressLine1 = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Address Line 1")).ToString(),
                                            AddressLine2 = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Address Line 2")).ToString(),
                                            CityName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "City")).ToString(),
                                            StateName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "State")).ToString(),
                                            CountryName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Country")).ToString(),
                                            Pincode = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Pincode")).ToString(),
                                        });
                                    }
                                    parents.Remove(parents.FirstOrDefault());
                                    return Task.FromResult(Response(parents, "", HttpStatusCode.OK));
                                }
                                else
                                {
                                    return Task.FromResult(Response(parents, "Selected file is empty.", HttpStatusCode.InternalServerError));
                                }
                            }
                            else
                            {
                                return Task.FromResult(Response(parents, "Invalid File.", HttpStatusCode.InternalServerError));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Task.FromResult(Response(parents, $"Internal server error: {ex}", HttpStatusCode.InternalServerError));
                }
                return Task.FromResult(Response(parents, "Something went wrong!!", HttpStatusCode.InternalServerError));
            });
        }

        [HttpPost]
        [Route("DataFilter")]
        public object DataFilter([FromBody]DataTableAjaxPostModel model)
        {
            return _parentService.GetAllAsync().Result.GetFilteredData(model);
        }
    }
}
