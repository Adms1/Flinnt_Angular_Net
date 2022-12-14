using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostTemplateService : IBaseService<PostTemplateViewModel>
    {
        Task<List<PostTemplateViewModel>> GetAllAsync();
        Task<List<PostTemplateViewModel>> GetByCategoryIdAsync(int postTemplateCategoryId);
    }
}