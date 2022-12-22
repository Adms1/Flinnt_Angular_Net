using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostPollService : IBaseService<PostPoll>
    {
        Task<List<PostPollViewModel>> GetAllAsync(int postId);
        Task<PostPollViewModel> GetAsync(int id);
        Task<bool> AddAsync(PostPollViewModel model);
        Task<bool> UpdateAsync(PostPollViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}