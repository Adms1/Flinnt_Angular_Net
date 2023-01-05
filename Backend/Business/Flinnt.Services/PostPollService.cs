using AutoMapper;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class PostPollService : ServiceBase, IPostPollService
    {
        public PostPollService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostPollViewModel>> GetAllAsync(int postId)
        {
            var result = mapper.Map<List<PostPollViewModel>>(await unitOfWork.PostPollRepository.GetAllAsync());
            return result.Where(x => x.PostId == postId).ToList();
        }

        public async Task<PostPollViewModel> GetAsync(int id)
        {
            return mapper.Map<PostPollViewModel>(await unitOfWork.PostPollRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostPollViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostPollRepository.AddAsync(mapper.Map<PostPollViewModel, PostPoll>(model)));

            if (data.PostPollId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostPollViewModel model)
        {
            var postPoll = await unitOfWork.PostPollRepository.GetAsync(model.PostPollId);
            if (postPoll != null)
            {
                postPoll.EndDateTime = model.EndDateTime;
                
                if(model.TotalVotesReceived > 0)
                {
                    postPoll.TotalVotesReceived = model.TotalVotesReceived;
                }
                await unitOfWork.PostPollRepository.UpdateAsync(postPoll);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postPoll = unitOfWork.PostPollRepository.GetAsync(id).Result;
            if (postPoll != null)
            {
                await unitOfWork.PostPollRepository.DeleteAsync(postPoll);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}