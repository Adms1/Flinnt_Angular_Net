using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostViewModel
    {
        public PostViewModel()
        {
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        public byte PostTypeId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public long? UserId { get; set; }
        public int? InstituteId { get; set; }
        public short? PostTemplateId { get; set; }
        public bool? ApprovalRequire { get; set; }
        public bool? IsApprove { get; set; }
        public long? ApproveByUserId { get; set; }
        public DateTime? ApproveDateTime { get; set; }
        public bool? Broadcast { get; set; }
        public int? AudienceGroupId { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }
    }
}
