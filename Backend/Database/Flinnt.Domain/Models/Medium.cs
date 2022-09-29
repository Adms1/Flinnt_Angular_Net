using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Medium : BaseEntity
    {
        public Medium()
        {
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        public byte MediumId { get; set; }
        public string MediumName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
    }
}
