using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostUserService : IBaseService<PostUser>
    {
        Task<List<PostUserViewModel>> GetAllAsync(int postId);
        Task<PostUserViewModel> GetAsync(int id);
        Task<bool> AddAsync(PostUserViewModel model);
        Task<bool> UpdateAsync(PostUserViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}