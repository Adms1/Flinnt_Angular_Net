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
    public class PostUserService : ServiceBase, IPostUserService
    {
        public PostUserService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostUserViewModel>> GetAllAsync(int postId)
        {
            var result = mapper.Map<List<PostUserViewModel>>(await unitOfWork.PostUserRepository.GetAllAsync());
            return result.Where(x => x.PostId == postId).ToList();
        }

        public async Task<PostUserViewModel> GetAsync(long id)
        {
            return mapper.Map<PostUserViewModel>(await unitOfWork.PostUserRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostUserViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostUserRepository.AddAsync(mapper.Map<PostUserViewModel, PostUser>(model)));

            if (data.PostId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostUserViewModel model)
        {
            var postUser = await unitOfWork.PostUserRepository.GetAsync(model.PostUserId);
            if (postUser != null)
            {
                postUser.IsView = model.IsView;
                postUser.Likes = model.Likes;
                postUser.LikeDateTime = model.LikeDateTime;
                postUser.ViewDateTime = model.ViewDateTime;
                postUser.Bookmark = model.Bookmark;
                postUser.BookmarkDateTime = model.BookmarkDateTime;

                await unitOfWork.PostUserRepository.UpdateAsync(postUser);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var postUser = unitOfWork.PostUserRepository.GetAsync(id).Result;
            if (postUser != null)
            {
                await unitOfWork.PostUserRepository.DeleteAsync(postUser);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}