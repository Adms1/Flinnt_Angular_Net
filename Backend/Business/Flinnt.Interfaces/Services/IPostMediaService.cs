using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostMediaService : IBaseService<PostMedium>
    {
        Task<List<PostMediumViewModel>> GetAsync(int postId);
        Task<bool> AddAsync(PostMediumViewModel model);
        Task<bool> UpdateAsync(PostMediumViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}