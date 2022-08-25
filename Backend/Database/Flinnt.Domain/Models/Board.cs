using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Board
    {
        public Board()
        {
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        public byte BoardId { get; set; }
        public string BoardName { get; set; }
        public string BoardShortName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
    }
}
