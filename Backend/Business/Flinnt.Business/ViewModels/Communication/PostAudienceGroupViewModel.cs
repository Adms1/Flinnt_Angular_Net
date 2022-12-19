using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostAudienceGroupViewModel
    {
        public PostAudienceGroupViewModel()
        {
        }

        public int AudienceGroupId { get; set; }
        public long UserId { get; set; }
        public int? InstituteId { get; set; }
        public string GroupName { get; set; }
        public string GroupLogo { get; set; }
        /// The filter data selected while creating this group. Store the data in the JSON format. {
        ///  &quot;version&quot;: &quot;1.0&quot;,
        ///  &quot;board&quot;: 1,
        ///  &quot;medium&quot;: 2,
        ///  &quot;standards&quot;: [1, 2, 3, 4],
        ///  &quot;divisions&quot;: [2,3,4],
        ///  &quot;roles&quot;: [&quot;student&quot;, &quot;teacher&quot;]
        /// }
        /// </summary>
        public string FilterData { get; set; }
    }
}
