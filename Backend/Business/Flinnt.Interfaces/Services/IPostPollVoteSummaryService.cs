using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostPollVoteSummaryService : IBaseService<PostPollVoteSummary>
    {
        Task<PostPollVoteSummaryViewModel> GetAsync(int id);
        Task<bool> AddAsync(PostPollVoteSummaryViewModel model);
        Task<bool> UpdateAsync(PostPollVoteSummaryViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}