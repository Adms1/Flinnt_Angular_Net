using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteGroup : BaseEntity
    {
        public InstituteGroup()
        {
            InstituteDivisions = new HashSet<InstituteDivision>();
        }

        public int InstituteGroupId { get; set; }
        public int InstituteId { get; set; }
        public byte? BoardId { get; set; }
        public byte? MediumId { get; set; }
        public byte? StandardId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Board Board { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual ICollection<InstituteDivision> InstituteDivisions { get; set; }
    }
}
