using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserAccountHistoryService : IBaseService<UserAccountHistory>
    {
        Task<UserAccountHistory> GetAsync(int id);

        Task<UserAccountHistory> AddAsync(UserAccountHistory model);

        Task<bool> UpdateAsync(UserAccountHistory model);

        Task<bool> DeleteAsync(int id);
    }
}