using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostPollOptionService : IBaseService<PostPollOption>
    {
        Task<PostPollOptionViewModel> GetAsync(int id);
        Task<bool> AddAsync(PostPollOptionViewModel model);
        Task<bool> UpdateAsync(PostPollOptionViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}