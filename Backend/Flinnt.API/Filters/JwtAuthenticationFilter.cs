//using Flinnt.Business.ViewModels;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Linq;
//using System.Security.Claims;

//namespace AAT.API.Filters
//{
//    [AttributeUsage(AttributeTargets.Class)]
//    public class JwtAuthenticationFilter : Attribute, IAuthorizationFilter, IActionFilter
//    {
//        public static AccountModel ApplicationUserApiRequest { get; set; }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            try
//            {
//                var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
//                ApplicationUserApiRequest = null;
//                if (IsAuthenticated)
//                {
//                    var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
//                    ApplicationUserApiRequest = new AccountModel
//                    {
//                        Id = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value),
//                        Name = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Name")?.Value
//                    };
//                }
//            }
//            catch (Exception)
//            {
//            }
//        }

//        public void OnActionExecuting(ActionExecutingContext context)
//        {
//            try
//            {
//                if (ApplicationUserApiRequest != null)
//                {
//                    dynamic valuesCntlr = context.Controller as dynamic;
//                    valuesCntlr.ApplicationUserApiRequest = ApplicationUserApiRequest;
//                }
//            }
//            catch (Exception)
//            {
//            }
//        }

//        public void OnActionExecuted(ActionExecutedContext context)
//        {
//            //throw new NotImplementedException();
//        }
//    }
//}
