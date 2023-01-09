using Flinnt.Business.ViewModels;
using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System;
using System.IO;
using Flinnt.API.Helpers;
using Microsoft.AspNetCore.StaticFiles;
using Flinnt.Business.ViewModels.General;

namespace Flinnt.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/post/media")]
    public class PostMediaController : BaseApiController
    {
        private readonly IPostMediaService _postMediaService;
        private readonly IHtmlLocalizer<PostMediaController> _localizer;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostMediaController( IPostMediaService postMediaService,
            IHtmlLocalizer<PostMediaController> htmlLocalizer)
        {
            _postMediaService = postMediaService;
            _localizer = htmlLocalizer;
        }

        [HttpGet]
        [Route("get/{postId}")]
        public async Task<object> GetById(int postId)
        {
            Logger.Info("post");
            return await GetDataWithMessage(async () =>
            {
                var result = await _postMediaService.GetAsync(postId);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("create"), DisableRequestSizeLimit]
        public async Task<object> CreatePostMedia([FromForm] PostMediumViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (model.Files.Count > 0)
                {
                    foreach (var item in model.Files)
                    {
                        var uniqueFileName = FileHelper.GetUniqueFileName(item.FileName);
                        var fullPath = Path.Combine(pathToSave, uniqueFileName);
                        var dbPath = Path.Combine(folderName, uniqueFileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }

                        //Determine the Content Type of the File.
                        string contentType = "";
                        new FileExtensionContentTypeProvider().TryGetContentType(uniqueFileName, out contentType);

                        model.MimeType = contentType;
                        model.SizeBytes = Convert.ToInt32(item.Length);
                        model.FilePath = dbPath;

                        // TODO: get all type of file info seperate
                        FileInfo oFileInfo = new FileInfo(fullPath);
                        model.Properties = "";
                    }
                    return await AddPostMediaAsync(model);
                }
                else
                {
                    return Response(false, "Internal server error", HttpStatusCode.InternalServerError);
                }
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> AddPostMediaAsync(PostMediumViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postMediaService.AddAsync(model);
                scope.Complete();

                if (flag)
                {
                    return Response(flag, _localizer["RecordAddSuccess"].Value.ToString());
                }
            }

            return Response(false, _localizer["RecordNotAdded"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("update")]
        public async Task<object> UpdatePostMedia([FromForm] PostMediumViewModel model)
        {
            Logger.Info("Post");
            return await GetMessage(async () =>
            {
                if (ModelState.IsValid && model != null)
                {
                    var folderName = Path.Combine("Resources", "Files");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (model.Files.Count > 0)
                    {
                        foreach (var item in model.Files)
                        {
                            var uniqueFileName = FileHelper.GetUniqueFileName(item.FileName);
                            var fullPath = Path.Combine(pathToSave, uniqueFileName);
                            var dbPath = Path.Combine(folderName, uniqueFileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }

                            //Determine the Content Type of the File.
                            string contentType = "";
                            new FileExtensionContentTypeProvider().TryGetContentType(uniqueFileName, out contentType);

                            model.MimeType = contentType;
                            model.SizeBytes = Convert.ToInt32(item.Length);
                            model.FilePath = dbPath;

                            // TODO: get all type of file info seperate
                            FileInfo oFileInfo = new FileInfo(fullPath);
                            model.Properties = "";
                        }
                    }

                    return await UpdatePostMediaAsync(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return Response(false, string.Join(",", errors), HttpStatusCode.InternalServerError);
            });
        }

        private async Task<Tuple<bool, string, HttpStatusCode>> UpdatePostMediaAsync(PostMediumViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var flag = await _postMediaService.UpdateAsync(model);
                scope.Complete();

                if (flag)
                    return Response(flag, _localizer["RecordUpdeteSuccess"].Value.ToString());
            }
            return Response(false, _localizer["RecordNotUpdate"].Value.ToString(), HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("delete/{postMediaId}")]
        public async Task<object> Delete(int postMediaId)
        {
            return await GetDataWithMessage(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var flag = await _postMediaService.DeleteAsync(postMediaId);
                    scope.Complete();

                    if (flag)
                        return Response(new BooleanResponseModel { Value = flag }, _localizer["RecordDeleteSuccess"].Value.ToString());
                }
                return Response(new BooleanResponseModel { Value = false }, _localizer["ReordNotDeleteSucess"].Value.ToString(), HttpStatusCode.InternalServerError);
            });
        }
    }
}