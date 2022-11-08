using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class InstituteConfigureSession: BaseEntity
    {
        public int InstituteConfigureSessionId { get; set; }
        public byte? CurrentStep { get; set; }
        public byte? InstituteTypeId { get; set; }
        public byte? GroupStructureId { get; set; }
        public int? InstituteId { get; set; }
        public byte? BoardId { get; set; }
        public byte? MediumId { get; set; }

        public virtual Board Board { get; set; }
        public virtual GroupStructure GroupStructure { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual InstituteType InstituteType { get; set; }
        public virtual Medium Medium { get; set; }
    }
}
