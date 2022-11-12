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
using System.Net.Http.Headers;
using System.Linq.Dynamic;
using Flinnt.Business.Enums.General;
using Flinnt.Services;

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
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ParentController(IParentService parentService,
            IUserService userService, 
            IUserInstituteService userInstituteService,
            ICityService cityService, 
            IUserProfileService userProfileService,
            IHtmlLocalizer<ParentController> htmlLocalizer)
        {
            _parentService = parentService;
            _userService = userService;
            _userInstituteService = userInstituteService;
            _cityService = cityService;
            _userProfileService = userProfileService;
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

        [HttpGet]
        [Route("get-by-primary-emailId/{emailId}")]
        public async Task<object> GetByPrimaryEmailId(string emailId)
        {
            Logger.Info("Get");
            return await GetDataWithMessage(async () =>
            {
                var result = (await _userService.GetUserByLoginId(emailId));

                if(result != null)
                {
                    if(result.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                    {
                        return Response(result, string.Empty);
                    }
                }
                return Response(new User(), string.Empty);
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
            var user = await _userService.GetUserByLoginId(model.PrimaryEmailId);

            if (user != null)
            {
                // usertype parent or student check
                if (user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                {
                    // consider dublicate row
                    return Response(new ParentViewModel(), "User account already exist!", HttpStatusCode.Forbidden);
                }

                await _userInstituteService.AddAsync(new UserInstitute
                {
                    InstituteId = user.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.InstituteStaff).FirstOrDefault().InstituteId,
                    RoleId = (int)RolesEnum.PrimaryAccount,
                    UserId = user.UserId,
                    UserTypeId = (int)UserTypes.Parent,
                    IsActive = true,
                    CreateDateTime = DateTime.Now
                });
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
                    var existingCity = await _cityService.GetByCityNameAsync(model.City);

                    if (existingCity == null)
                    {
                        CityViewModel cityViewModel = new CityViewModel
                        {
                            CityName = model.City,
                            CreateDateTime = DateTime.Now,
                            StateId = model.StateId,
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
                return Response(parent, _localizer["RecordAddSuccess"].Value.ToString());
            }
            return Response(parent, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [Route("import-roster")]
        [HttpPost, DisableRequestSizeLimit]
        public object ImportParentRoster()
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
            return _parentService.GetAllAsync().Result.GetFilteredData(model);
        }
    }
}
