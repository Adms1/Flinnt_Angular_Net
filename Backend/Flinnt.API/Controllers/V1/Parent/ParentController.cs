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
using System.Collections.Generic;
using Flinnt.Interfaces.Background;
using System.Transactions;

namespace Flinnt.API.Controllers
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
        private readonly IBackgroundService _backgroundService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ParentController(IParentService parentService,
            IUserService userService,
            IUserInstituteService userInstituteService,
            ICityService cityService,
            IUserProfileService userProfileService,
            IUserAccountHistoryService userAccountHistoryService,
            IBackgroundService backgroundService,
            IHtmlLocalizer<ParentController> htmlLocalizer)
        {
            _parentService = parentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userProfileService = userProfileService;
            _localizer = htmlLocalizer;
            _userAccountHistoryService = userAccountHistoryService;
            _backgroundService = backgroundService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<object> GetAll()
        {
            Logger.Info("Parent list");
            return await GetDataWithMessage(async () =>
            {
                var result = await _parentService.GetAllAsync();
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
                var result = await _parentService.GetAsync(Id);
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

            var extParent = await _parentService.ValidateParent(model);

            if (extParent.Any())
            {
                return Response(new ParentViewModel(), "Parent account already exist!", HttpStatusCode.Forbidden);
            }

            var parent = new ParentViewModel();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
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
                            Address = model.AddressLine1 + ", " + model.AddressLine2,
                            CountryId = (byte)model.CountryId,
                            StateId = (byte)model.StateId,
                            Pincode = model.Pincode
                        });
                    }
                }

                parent = await _parentService.AddAsync(model);
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
                scope.Complete();
            }
            return Response(parent, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [Route("validate-parent-import")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<object> ValidateParentImport(IFormFile file)
        {
            return await GetDataWithMessage(() =>
            {
                var httpRequest = file;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                List<ParentViewModel> parents = new List<ParentViewModel>();

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
                                    return Task.FromResult(Response(parents, "The file format is not supported.", HttpStatusCode.InternalServerError));

                                dsexcelRecords = reader.AsDataSet();
                                reader.Close();

                                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                                {
                                    string noData = "<<no-data>>";
                                    var dataSet = dsexcelRecords.Tables[0];
                                    foreach (DataRow row in dataSet.Rows)
                                    {
                                        var _parent1FirstName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - First Name (Mandatory)")).ToString();
                                        var _parent1LastName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Last Name (Mandatory)")).ToString();
                                        var _parent1EmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Email Id")).ToString();
                                        var _parent1MobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Mobile No")).ToString();
                                        var _parent1Relationship = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 1 - Father (F) OR Mother (M) (Mandatory)")).ToString();
                                        var _parent2FirstName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - First Name")).ToString();
                                        var _parent2LastName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Last Name")).ToString();
                                        var _parent2EmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Email Id")).ToString();
                                        var _parent2MobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Parent 2 - Mobile No")).ToString();
                                        var _parent2Relationship = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Single Parent (Y/N)")).ToString();
                                        var _primaryEmailId = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Primary Email Address (Mandatory)")).ToString();
                                        var _primaryMobileNo = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Mobile No")).ToString();
                                        var _addressLine1 = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Address Line 1")).ToString();
                                        var _addressLine2 = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Address Line 2")).ToString();
                                        var _cityName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "City")).ToString();
                                        var _stateName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "State")).ToString();
                                        var _countryName = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Country")).ToString();
                                        var _pincode = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Pincode")).ToString();
                                        var _singleParent = row.ItemArray.GetValue(Array.IndexOf(dataSet.Rows[0].ItemArray, "Single Parent (Y/N)")).ToString();
                                        var parent = new ParentViewModel
                                        {
                                            Parent1FirstName = !string.IsNullOrEmpty(_parent1FirstName) ? _parent1FirstName : noData,
                                            Parent1LastName = !string.IsNullOrEmpty(_parent1LastName) ? _parent1LastName : noData,
                                            Parent1EmailId = !string.IsNullOrEmpty(_parent1EmailId) ? _parent1EmailId : noData,
                                            Parent1MobileNo = !string.IsNullOrEmpty(_parent1MobileNo) ? _parent1MobileNo : noData,
                                            Parent1Relationship = !string.IsNullOrEmpty(_parent1Relationship) ? _parent1Relationship.ToUpper() == "M" || _parent1Relationship.ToUpper() == "F" ? _parent1Relationship : noData : noData,
                                            Parent2FirstName = !string.IsNullOrEmpty(_parent2FirstName) ? _parent2FirstName : noData,
                                            Parent2LastName = !string.IsNullOrEmpty(_parent2LastName) ? _parent2LastName : noData,
                                            Parent2EmailId = !string.IsNullOrEmpty(_parent2EmailId) ? _parent2EmailId : noData,
                                            Parent2MobileNo = !string.IsNullOrEmpty(_parent2MobileNo) ? _parent2MobileNo : noData,
                                            Parent2Relationship = !string.IsNullOrEmpty(_parent2Relationship) ? _parent2Relationship.ToUpper() == "M" || _parent2Relationship.ToUpper() == "F" ? _parent2Relationship : noData : noData,
                                            PrimaryEmailId = !string.IsNullOrEmpty(_primaryEmailId) ? _primaryEmailId : noData,
                                            PrimaryMobileNo = !string.IsNullOrEmpty(_primaryMobileNo) ? _primaryMobileNo : noData,
                                            AddressLine1 = !string.IsNullOrEmpty(_addressLine1) ? _addressLine1 : noData,
                                            AddressLine2 = !string.IsNullOrEmpty(_addressLine2) ? _addressLine2 : noData,
                                            CityName = !string.IsNullOrEmpty(_cityName) ? _cityName : noData,
                                            StateName = !string.IsNullOrEmpty(_stateName) ? _stateName : noData,
                                            CountryName = !string.IsNullOrEmpty(_countryName) ? _countryName : noData,
                                            Pincode = !string.IsNullOrEmpty(_pincode) ? _pincode : noData,
                                            ImportStatus = "Valid",
                                            SingleParent = !string.IsNullOrEmpty(_singleParent) ? _singleParent == "Y" ? (byte)1 : (byte)0 : (byte)1,
                                        };

                                        var summary = GenerateImportSummaryAsync(parent);
                                        if (summary.Result.Count > 0)
                                        {
                                            parent.ImportStatus = "Invalid";
                                            parent.ImportSummary.AddRange(summary.Result);
                                        }
                                        parents.Add(parent);
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

        private async Task<List<ParentImportSummary>> GenerateImportSummaryAsync(ParentViewModel parentViewModel)
        {
            List<ParentImportSummary> importSummaries = new List<ParentImportSummary>();

            if (string.IsNullOrEmpty(parentViewModel.Parent1FirstName)
               || parentViewModel.Parent1FirstName.Equals("<<no-data>>"))
            {
                importSummaries.Add(new ParentImportSummary
                {
                    FieldName = "Parent 1 - First Name",
                    Message = "Parent 1 - First Name could not be empty"
                });
            }

            if (string.IsNullOrEmpty(parentViewModel.Parent1LastName)
                || parentViewModel.Parent1LastName.Equals("<<no-data>>"))
            {
                importSummaries.Add(new ParentImportSummary
                {
                    FieldName = "Parent 1 - Last Name",
                    Message = "Parent 1 - Last Name could not be empty"
                });
            }


            if (string.IsNullOrEmpty(parentViewModel.Parent1Relationship)
            || parentViewModel.Parent1Relationship.Equals("<<no-data>>"))
            {
                importSummaries.Add(new ParentImportSummary
                {
                    FieldName = "Parent 1 - Father/Mother",
                    Message = "Parent 1 - Father/Mother could not be empty and it must be M (for Male) and F (for Female)"
                });
            }

            if (parentViewModel.SingleParent == 0)
            {
                if (string.IsNullOrEmpty(parentViewModel.Parent2Relationship)
                    || parentViewModel.Parent2Relationship.Equals("<<no-data>>"))
                {
                    importSummaries.Add(new ParentImportSummary
                    {
                        FieldName = "Parent 2 - Father/Mother",
                        Message = "Parent 2 - Father/Mother could not be empty and it must be M (for Male) and F (for Female)"
                    });
                }
            }

            if (string.IsNullOrEmpty(parentViewModel.PrimaryEmailId)
                || parentViewModel.PrimaryEmailId.Equals("<<no-data>>"))
            {
                importSummaries.Add(new ParentImportSummary
                {
                    FieldName = "Primary email address",
                    Message = "Primary email address could not be blank"
                });
            }
            else
            {
                // call APIs to check if user exists in db
                var user = await _userService.GetUserByLoginId(parentViewModel.PrimaryEmailId);
                if (user != null)
                {
                    // usertype parent or student check
                    if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                    {
                        // consider dublicate row
                        importSummaries.Add(new ParentImportSummary
                        {
                            FieldName = "Primary email address",
                            Message = "A parent account already exists with the provided Primary email address"
                        });
                    }
                }
            }

            if (!string.IsNullOrEmpty(parentViewModel.PrimaryMobileNo)
                || parentViewModel.PrimaryMobileNo.Equals("<<no-data>>"))
            {
                if (parentViewModel.PrimaryMobileNo.Trim().Length > 10)
                {
                    importSummaries.Add(new ParentImportSummary
                    {
                        FieldName = "Primary mobile no",
                        Message = "The mobile no must contain 10 digits"
                    });
                }
            }

            return importSummaries;
        }

        [Route("import-parent-roaster")]
        [HttpPost]
        public async Task<object> ImportParentRoster([FromBody] ParentViewModel[] parentViewModel)
        {
            return await GetDataWithMessage(async () =>
            {
                if (parentViewModel.ToList().Count > 0)
                {
                    _backgroundService.EnqueueJob<IBackgroundParentJobs>(m => m.ImportParentsAsync(parentViewModel.ToList()));
                }
                return Response(true, string.Empty);
            });
        }

        [HttpPost]
        [Route("DataFilter")]
        public object DataFilter([FromBody] DataTableAjaxPostModel model)
        {
            return _parentService.GetAllAsync().Result.GetFilteredData(model);
        }
    }
}
