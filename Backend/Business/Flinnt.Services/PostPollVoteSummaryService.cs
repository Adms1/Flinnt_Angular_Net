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
    public class PostPollVoteSummaryService : ServiceBase, IPostPollVoteSummaryService
    {
        public PostPollVoteSummaryService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostPollVoteSummaryViewModel>> GetAsync(int id)
        {
            return mapper.Map<List<PostPollVoteSummaryViewModel>>(await unitOfWork.PostPollVoteSummaryRepository.FindByAsync(x=>x.PostPollId == id));
        }

        public async Task<bool> AddAsync(PostPollVoteSummaryViewModel model)
        {
            var existing = await Task.FromResult(await unitOfWork.PostPollVoteSummaryRepository.FindByAsync(x=>x.PostPollId == model.PostPollId && x.PostPollOptionId == model.PostPollOptionId));

            if (existing.Any())
            {
                return false;
            }
            var data = await Task.FromResult(await unitOfWork.PostPollVoteSummaryRepository.AddAsync(mapper.Map<PostPollVoteSummaryViewModel, PostPollVoteSummary>(model)));

            if (data.PostPollId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostPollVoteSummaryViewModel model)
        {
            var data = await unitOfWork.PostPollVoteSummaryRepository.GetAsync(model.PostPollVoteSummaryId);
            if (data != null)
            {
                data.VotesReceive = data.VotesReceive + 1;
                //TODO: C = V/T × 100

                data.VotePercentage = data.VotePercentage; 
                await unitOfWork.PostPollVoteSummaryRepository.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postPoll = unitOfWork.PostPollVoteSummaryRepository.GetAsync(id).Result;
            if (postPoll != null)
            {
                await unitOfWork.PostPollVoteSummaryRepository.DeleteAsync(postPoll);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}