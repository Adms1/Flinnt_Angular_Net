using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostService : IBaseService<Post>
    {
        Task<List<PostViewModel>> GetAllAsync(int instituteId);
        Task<List<PostViewModel>> GetAllBookmarksAsync(int postId, int userId);
        Task<List<PostViewModel>> GetPostByPostType(int instituteId, int postTypeId);
        Task<List<PostViewModel>> GetPostByMediaType(int instituteId, int mediaTypeId);
        Task<PostViewModel> GetAsync(int id);
        Task<List<PostViewModel>> GetApprovalRequestByInstituteId(int instituteId);
        Task<bool> AddAsync(PostViewModel model);
        Task<bool> UpdateAsync(PostViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}