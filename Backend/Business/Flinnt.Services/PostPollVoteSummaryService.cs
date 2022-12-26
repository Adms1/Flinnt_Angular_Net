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

        public async Task<PostPollVoteSummaryViewModel> GetAsync(int id)
        {
            return mapper.Map<PostPollVoteSummaryViewModel>(await unitOfWork.PostPollVoteSummaryRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostPollVoteSummaryViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostPollVoteSummaryRepository.AddAsync(mapper.Map<PostPollVoteSummaryViewModel, PostPollVoteSummary>(model)));

            if (data.PostPollId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostPollVoteSummaryViewModel model)
        {
            var data = await unitOfWork.PostPollVoteSummaryRepository.GetAsync(model.PostPollId);
            if (data != null)
            {
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