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
    public class PostPollVoteService : ServiceBase, IPostPollVoteService
    {
        public PostPollVoteService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }
        public async Task<PostPollVoteViewModel> GetAsync(int id)
        {
            return mapper.Map<PostPollVoteViewModel>(await unitOfWork.PostPollVoteRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostPollVoteViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostPollVoteRepository.AddAsync(mapper.Map<PostPollVoteViewModel, PostPollVote>(model)));

            if (data.PostPollId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostPollVoteViewModel model)
        {
            var data = await unitOfWork.PostPollVoteRepository.GetAsync(model.PostPollId);
            if (data != null)
            {
                await unitOfWork.PostPollVoteRepository.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postPoll = unitOfWork.PostPollVoteRepository.GetAsync(id).Result;
            if (postPoll != null)
            {
                await unitOfWork.PostPollVoteRepository.DeleteAsync(postPoll);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}