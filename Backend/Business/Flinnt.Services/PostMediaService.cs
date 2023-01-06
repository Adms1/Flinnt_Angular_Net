using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class PostMediaService : ServiceBase, IPostMediaService
    {
        public PostMediaService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostMediumViewModel>> GetAsync(int postId)
        {
            var result = mapper.Map<List<PostMediumViewModel>>(await unitOfWork.PostMediaRepository.FindByAsync(x => x.PostId == postId));
            return result.ToList();
        }

        public async Task<bool> AddAsync(PostMediumViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostMediaRepository.AddAsync(mapper.Map<PostMediumViewModel, PostMedium>(model)));

            if (data.PostMediaId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostMediumViewModel model)
        {
            var postMedia = await unitOfWork.PostMediaRepository.GetAsync(model.PostMediaId);
            if (postMedia != null)
            {
                postMedia.FilePath = model.FilePath;
                postMedia.SizeBytes = model.SizeBytes;
                postMedia.Properties = model.Properties;

                await unitOfWork.PostMediaRepository.UpdateAsync(postMedia);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postMedia = unitOfWork.PostMediaRepository.GetAsync(id).Result;
            if (postMedia != null)
            {
                await unitOfWork.PostMediaRepository.DeleteAsync(postMedia);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}