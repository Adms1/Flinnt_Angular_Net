using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostMediaService : IBaseService<PostMedium>
    {
        Task<bool> AddAsync(PostMediumViewModel model);
    }
}