using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostPollVoteService : IBaseService<PostPollVote>
    {
        Task<List<PostPollVoteViewModel>> GetAsync(int id);
        Task<bool> AddAsync(PostPollVoteViewModel model);
        Task<bool> UpdateAsync(PostPollVoteViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}