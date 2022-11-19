using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class UserParentChildRelationshipService : ServiceBase, IUserParentChildRelationshipService
    {
        public UserParentChildRelationshipService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<UserParentChildRelationshipModel> AddAsync(UserParentChildRelationshipModel model)
        {
            return mapper.Map<UserParentChildRelationshipModel>(await Task.FromResult(await unitOfWork.UserParentChildRelationshipRepository.AddAsync(mapper.Map<UserParentChildRelationshipModel, UserParentChildRelationship>(model))));
        }
    }
}