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
    public class ParentService : ServiceBase, IParentService
    {
        public ParentService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<ParentViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<ParentViewModel>>(await unitOfWork.ParentRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<ParentViewModel> GetAsync(int id)
        {
            return mapper.Map<ParentViewModel>(await unitOfWork.ParentRepository.GetAsync(id));
        }

        public async Task<ParentViewModel> AddAsync(ParentViewModel model)
        {
            return mapper.Map<ParentViewModel>(await Task.FromResult(unitOfWork.ParentRepository.CreateParentRecord(mapper.Map<ParentViewModel, Parent>(model))));
        }

        public async Task<List<ParentViewModel>> ValidateParent(ParentViewModel model)
        {
            return mapper.Map<List<ParentViewModel>>
               (await Task.FromResult(await unitOfWork.ParentRepository.FindByAsync(x =>
               x.Parent1EmailId == model.Parent1EmailId
           && x.Parent1MobileNo == model.Parent1MobileNo
           && x.UserId == model.UserId
           && x.Parent1FirstName.ToLower() == model.Parent1FirstName.ToLower()
           && x.Parent1LastName.ToLower() == model.Parent1LastName.ToLower())));
        }
    }
}