using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Standard : BaseEntity
    {
        public Standard()
        {
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        public byte StandardId { get; set; }
        public string StandardName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
    }
}
