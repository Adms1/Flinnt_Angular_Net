using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserParentChildRelationshipService : IBaseService<UserParentChildRelationship>
    {
        Task<UserParentChildRelationshipModel> AddAsync(UserParentChildRelationshipModel model);
    }
}