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
    public class PostCommentService : ServiceBase, IPostCommentService
    {
        public PostCommentService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostCommentViewModel>> GetAsync(int postId)
        {
            var result = mapper.Map< List<PostCommentViewModel>>(await unitOfWork.PostCommentRepository.FindByAsync(x=> x.PostId == postId));
            return result.ToList();
        }

        public async Task<List<PostCommentViewModel>> GetApprovalRequestByPostId(int postId)
        {
            var result = mapper.Map<List<PostCommentViewModel>>(await unitOfWork.PostCommentRepository.FindByAsync(x => x.Approve.Value == true));
            return result.ToList();
        }

        public async Task<bool> AddAsync(PostCommentViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostCommentRepository.AddAsync(mapper.Map<PostCommentViewModel, PostComment>(model)));

            if (data.PostCommentId > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateAsync(PostCommentViewModel model)
        {
            var postComment = await unitOfWork.PostCommentRepository.GetAsync(model.PostCommentId);
            if (postComment != null)
            {
                postComment.CommentText = model.CommentText;
                postComment.UpdateDateTime = model.UpdateDateTime;

                if(model.Approve.Value){
                    postComment.Approve = model.Approve;
                    postComment.ApproveDateTime = model.ApproveDateTime;
                    postComment.ApproveUserId = model.ApproveUserId;
                }

                await unitOfWork.PostCommentRepository.UpdateAsync(postComment);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postComment = unitOfWork.PostCommentRepository.GetAsync(id).Result;
            if (postComment != null)
            {
                await unitOfWork.PostCommentRepository.DeleteAsync(postComment);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}