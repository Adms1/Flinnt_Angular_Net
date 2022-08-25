using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteDivision
    {
        public int InstituteDivisionId { get; set; }
        public string DivisionName { get; set; }
        public int InstituteGroupId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual InstituteGroup InstituteGroup { get; set; }
    }
}
