using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteType
    {
        public InstituteType()
        {
            Institutes = new HashSet<Institute>();
        }

        public byte InstTypeId { get; set; }
        public string TypeName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
