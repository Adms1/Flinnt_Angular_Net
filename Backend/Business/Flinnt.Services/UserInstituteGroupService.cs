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
    public class UserInstituteGroupService : ServiceBase, IUserInstituteGroupService
    {
        public UserInstituteGroupService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<UserInstituteGroupModel> AddAsync(UserInstituteGroupModel model)
        {
            return mapper.Map<UserInstituteGroupModel>(await Task.FromResult(await unitOfWork.UserInstituteGroupRepository.AddAsync(mapper.Map<UserInstituteGroupModel, UserInstituteGroup>(model))));
        }
    }
}