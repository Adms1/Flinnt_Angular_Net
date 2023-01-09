using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Flinnt.Business.ViewModels;
using System;

namespace Flinnt.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(edplexdbContext context) : base(context)
        {
        }

        public Task<List<PostViewModel>> GetFeed(int instituteId)
        {
            return (from p in Context.Posts
                    where p.InstituteId == instituteId
                           && p.PublishDateTime != default(DateTime)
                           && p.IsApprove == true
                           && p.DeleteDateTime == null
                    orderby p.PublishDateTime descending
                    select new PostViewModel
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        ApproveByUserId = p.ApproveByUserId,
                        ApproveDateTime = p.ApproveDateTime,
                        AudienceGroupId = p.AudienceGroupId,
                        Broadcast = p.Broadcast,
                        CreateDateTime = p.CreateDateTime,
                        InstituteId = p.InstituteId,
                        IsApprove = p.IsApprove,
                        MessageBody = p.MessageBody,
                        PostTemplateId = p.PostTemplateId,
                        PostTypeId = p.PostTypeId,
                        PublishDateTime = p.PublishDateTime,
                        Title = p.Title,
                    }).ToListAsync();
        }

        public Task<List<PostViewModel>> GetBookmarkedPost(int postId, int userId)
        {
            var postUser = from p in Context.Posts
                           from pu in Context.PostUsers
                           where pu.PostId == postId 
                           && pu.UserId == userId
                           && pu.Bookmark.Value == true
                           && p.PublishDateTime != default(DateTime)
                           && p.IsApprove == true
                           && p.DeleteDateTime == null
                           orderby p.PublishDateTime descending
                           select new PostViewModel
                           {
                               PostId = p.PostId,
                               UserId = p.UserId,
                               ApproveByUserId = p.ApproveByUserId,
                               ApproveDateTime = p.ApproveDateTime,
                               AudienceGroupId = p.AudienceGroupId,
                               Broadcast = p.Broadcast,
                               CreateDateTime = p.CreateDateTime,
                               InstituteId = p.InstituteId,
                               IsApprove = p.IsApprove,
                               MessageBody = p.MessageBody,
                               PostTemplateId = p.PostTemplateId,
                               PostTypeId = p.PostTypeId,
                               PublishDateTime = p.PublishDateTime,
                               Title = p.Title,
                           };

            return postUser.ToListAsync();
        }

        public Task<List<PostViewModel>> GetPostByPostType(int instituteId, int postTypeId)
        {
            var postUser = from p in Context.Posts
                           where p.InstituteId == instituteId
                           && p.PostTypeId == postTypeId
                           && p.PublishDateTime != default(DateTime)
                           && p.IsApprove == true
                           && p.DeleteDateTime == null
                           orderby p.PublishDateTime descending
                           select new PostViewModel
                           {
                               PostId = p.PostId,
                               UserId = p.UserId,
                               ApproveByUserId = p.ApproveByUserId,
                               ApproveDateTime = p.ApproveDateTime,
                               AudienceGroupId = p.AudienceGroupId,
                               Broadcast = p.Broadcast,
                               CreateDateTime = p.CreateDateTime,
                               InstituteId = p.InstituteId,
                               IsApprove = p.IsApprove,
                               MessageBody = p.MessageBody,
                               PostTemplateId = p.PostTemplateId,
                               PostTypeId = p.PostTypeId,
                               PublishDateTime = p.PublishDateTime,
                               Title = p.Title,
                           };

            return postUser.ToListAsync();
        }
    }
}