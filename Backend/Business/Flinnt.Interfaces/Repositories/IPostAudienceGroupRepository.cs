using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Repositories
{
    public interface IPostAudienceGroupRepository : IBaseRepository<PostAudienceGroup>
    {
        Task<List<PostAudienceGroupViewModel>> GetPostAudienceGroupByInstituteIdAndUserId(int instituteId, int userId);
    }
}