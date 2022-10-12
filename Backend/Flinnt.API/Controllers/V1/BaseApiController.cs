using Flinnt.Business.Enums.General;
using Flinnt.Business.ViewModels.General;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Flinnt.API.Controllers
{
    //[JwtAuthenticationFilter]
    public class BaseApiController : Controller
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public BaseApiController()
        {
        }

        protected async Task<ResponseDetail<T>> GetDataWithMessage<T>(Func<Task<Tuple<T, string, HttpStatusCode>>> getDataFunc)
        {
            var output = new ResponseDetail<T>();
            try
            {                
                var result = await getDataFunc();
                output.Data = result.Item1;
                output.Message = result.Item2;
                output.StatusCode = result.Item3;               
                return await Task.FromResult(output);
            }
            catch (Exception ex)
            {
                output.StatusCode = HttpStatusCode.InternalServerError;
                output.Error = new Error
                {
                    Code = ErrorCode.SERVICE_EXECUTION_FAILED,
                    Message = ex.Message
                };
                output.Message = "Something went wrong!! Please Try again later";
                Logger.Error($"An error has occuerd on {Convert.ToString(ControllerContext.RouteData.Values["controller"])+ " controller &" + Convert.ToString(ControllerContext.RouteData.Values["action"])+" Method"}Message:{ex.Message}");
                return await Task.FromResult(output);
            }
        }

        protected async Task<ResponseDetail> GetMessage(Func<Task<Tuple<bool,string, HttpStatusCode>>> getMessageFunc)
        {
            var output = new ResponseDetail();
            try
            {
                var result = await getMessageFunc();
                output.Data = result.Item1;
                output.Message = result.Item2;
                output.StatusCode = result.Item3;
                return await Task.FromResult(output);
            }
            catch (Exception ex)
            {
                output.Data = false;
                output.StatusCode = HttpStatusCode.InternalServerError;
                output.Error = new Error
                {
                    Code = ErrorCode.SERVICE_EXECUTION_FAILED,
                    Message = ex.Message
                };
                output.Message = "Something went wrong!! Please Try again later";
                Logger.Error($"An error has occuerd on {Convert.ToString(ControllerContext.RouteData.Values["controller"]) + " controller &" + Convert.ToString(ControllerContext.RouteData.Values["action"]) + " Method"}Message:{ex.Message}");
                return await Task.FromResult(output);
            }
        }

        protected new Tuple<T, string, HttpStatusCode> Response<T>(T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Tuple<T, string, HttpStatusCode>(data, message, statusCode);
        }
    }
}