using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostCommentService : IBaseService<PostComment>
    {
        Task<List<PostCommentViewModel>> GetAsync(int postId);
        Task<List<PostCommentViewModel>> GetApprovalRequestByPostId(int postId);
        Task<bool> AddAsync(PostCommentViewModel model);
        Task<bool> UpdateAsync(PostCommentViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}