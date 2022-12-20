using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostCommentViewModel
    {
        public PostCommentViewModel()
        {
        }

        public int PostCommentId { get; set; }
        public int PostId { get; set; }
        public string CommentText { get; set; }
        public long? UserId { get; set; }
        public bool? Approve { get; set; }
        public long? ApproveUserId { get; set; }
        public DateTime? ApproveDateTime { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
