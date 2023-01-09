using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class PostService : ServiceBase, IPostService
    {
        public PostService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostViewModel>> GetAllAsync(int instituteId)
        {
            return await unitOfWork.PostRepository.GetFeed(instituteId);
        }

        public async Task<List<PostViewModel>> GetAllBookmarksAsync(int postId, int userId)
        {
            return await unitOfWork.PostRepository.GetBookmarkedPost(postId, userId);

        }
        public async Task<List<PostViewModel>> GetApprovalRequestByInstituteId(int instituteId)
        {
            var result = mapper.Map<List<PostViewModel>>(await unitOfWork.PostRepository.FindByAsync(x=>x.ApprovalRequire.Value == true));
            return result.ToList();
        }

        public async Task<PostViewModel> GetAsync(int id)
        {
            return mapper.Map<PostViewModel>(await unitOfWork.PostRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(PostViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostRepository.AddAsync(mapper.Map<PostViewModel, Post>(model)));

            if (data.PostId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostViewModel model)
        {
            var post = await unitOfWork.PostRepository.GetAsync(model.PostId);
            if (post != null)
            {
                await unitOfWork.PostRepository.UpdateAsync(post);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = unitOfWork.PostRepository.GetAsync(id).Result;
            if (post != null)
            {
                await unitOfWork.PostRepository.DeleteAsync(post);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        
    }
}