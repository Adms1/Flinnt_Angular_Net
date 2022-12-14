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
    public class PostTemplateService : ServiceBase, IPostTemplateService
    {
        public PostTemplateService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<PostTemplateViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<PostTemplateViewModel>>(await unitOfWork.PostTemplateRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<List<PostTemplateViewModel>> GetByCategoryIdAsync(int postTemplateCategoryId)
        {
            var result = mapper.Map<List<PostTemplateViewModel>>(await unitOfWork.PostTemplateRepository.FindByAsync(x=>x.PostTemplateCategoryId == postTemplateCategoryId));
            return result.ToList();
        }
    }
}