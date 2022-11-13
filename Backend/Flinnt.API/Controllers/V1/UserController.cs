﻿using Castle.Core.Internal;
using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.General;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IHtmlLocalizer<UserController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserController(IUserService userService, 
            IHtmlLocalizer<UserController> htmlLocalizer)
        {
            _userService = userService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("get/emailid")]
        public async Task<object> GetByPrimaryEmailId(string emailId)
        {
            Logger.Info("Get");

            if (string.IsNullOrEmpty(emailId))
                return Response(new User(), "Something went wrong!!", System.Net.HttpStatusCode.Forbidden);

            return await GetDataWithMessage(async () =>
            {
                var result = (await _userService.GetUserByLoginId(emailId));

                if (result != null)
                {
                    if (result.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                    {
                        return Response(result, string.Empty);
                    }
                }
                return Response(new User(), string.Empty);
            });
        }

        [HttpGet]
        [Route("get/userid")]
        public async Task<object> GetByUserId(int userId)
        {
            Logger.Info("Get");

            if (userId > 0)
                return Response(new User(), "Something went wrong!!", System.Net.HttpStatusCode.Forbidden);

            return await GetDataWithMessage(async () =>
            {
                var result = (await _userService.GetAsync(userId));

                if (result != null)
                {
                    if (result.UserInstitutes.Where(x => x.UserTypeId == (int)UserTypes.Parent || x.UserTypeId == (int)UserTypes.Student).Any())
                    {
                        return Response(result, string.Empty);
                    }
                }
                return Response(new User(), string.Empty);
            });
        }
    }
}