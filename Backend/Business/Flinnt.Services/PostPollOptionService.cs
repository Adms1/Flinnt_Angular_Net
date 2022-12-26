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
    public class PostPollOptionService : ServiceBase, IPostPollOptionService
    {
        public PostPollOptionService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<PostPollOptionViewModel> GetAsync(int id)
        {
            return mapper.Map<PostPollOptionViewModel>(await unitOfWork.PostPollOptionRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostPollOptionViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostPollOptionRepository.AddAsync(mapper.Map<PostPollOptionViewModel, PostPollOption>(model)));

            if (data.PostPollId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostPollOptionViewModel model)
        {
            var data = await unitOfWork.PostPollOptionRepository.GetAsync(model.PostPollId);
            if (data != null)
            {
                await unitOfWork.PostPollOptionRepository.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postPoll = unitOfWork.PostPollOptionRepository.GetAsync(id).Result;
            if (postPoll != null)
            {
                await unitOfWork.PostPollOptionRepository.DeleteAsync(postPoll);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}