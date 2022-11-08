using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class InstituteConfiguration: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteConfigurationId { get; set; }
        /// <summary>
        /// The institute identifier this configuration belongs to.
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The configuration key.
        /// </summary>
        public string ConfigurationKey { get; set; }
        /// <summary>
        /// The configuration value.
        /// </summary>
        public string ConfigurationValue { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual Institute Institute { get; set; }
    }
}
