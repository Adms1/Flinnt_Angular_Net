using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IPostLogService : IBaseService<PostLog>
    {
        Task<List<PostLogViewModel>> GetAllAsync();
        Task<bool> AddAsync(PostLogViewModel model);
    }
}