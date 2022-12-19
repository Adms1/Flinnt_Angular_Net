using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Flinnt.Repositories
{
    public class PostAudienceGroupRepository : BaseRepository<PostAudienceGroup>, IPostAudienceGroupRepository
    {
        public PostAudienceGroupRepository(edplexdbContext context) : base(context)
        {
        }

        public Task<List<PostAudienceGroupViewModel>> GetPostAudienceGroupByInstituteIdAndUserId(int instituteId, int userId)
        {
            return (from pg in Context.PostAudienceGroups
                    where pg.InstituteId == instituteId
                        && pg.UserId == userId
                    select new PostAudienceGroupViewModel
                    {
                        AudienceGroupId = pg.AudienceGroupId,
                        UserId = pg.UserId,
                        InstituteId = pg.InstituteId,
                        GroupLogo = pg.GroupLogo,
                        GroupName = pg.GroupName,
                        FilterData = pg.FilterData
                    }).ToListAsync();
        }
    }
}