using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostAudienceGroupService : IBaseService<PostAudienceGroup>
    {
        Task<List<PostAudienceGroupViewModel>> GetAllAsync();
        Task<PostAudienceGroupViewModel> GetAsync(int id);
        Task<List<PostAudienceGroupViewModel>> GetPostAudienceGroupByInstituteIdAndUserId(int instituteId, int userId);
        Task<bool> AddAsync(PostAudienceGroupViewModel model);
        Task<bool> UpdateAsync(PostAudienceGroupViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}