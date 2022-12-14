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
    public class PostTemplateCategoryService : ServiceBase, IPostTemplateCategoryService
    {
        public PostTemplateCategoryService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
            
        }

        public async Task<List<PostTemplateCategoryViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<PostTemplateCategoryViewModel>>(await unitOfWork.PostTemplateCategoryRepository.GetAllAsync());
            return result.ToList();
        }
    }
}