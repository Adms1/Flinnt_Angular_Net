using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<PostViewModel>> GetFeed(int instituteId);
        Task<List<PostViewModel>> GetBookmarkedPost(int postId, int userId);
        Task<List<PostViewModel>> GetPostByPostType(int instituteId, int postTypeId);
    }
}