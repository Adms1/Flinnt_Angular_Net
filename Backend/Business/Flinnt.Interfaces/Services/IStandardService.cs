using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IStandardService : IBaseService<Standard>
    {
        Task<List<StandardViewModel>> GetAllAsync();
    }
}