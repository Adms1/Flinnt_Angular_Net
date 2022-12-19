using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Business.ViewModels.Institute;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class PostTypeService : ServiceBase, IPostTypeService
    {
        public PostTypeService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostTypeViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<PostTypeViewModel>>(await unitOfWork.PostTypeRepository.GetAllAsync());
            return result.ToList();
        }
    }
}