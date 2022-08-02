using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<List<AccountModel>> GetAllAsync();

        Task<AccountModel> GetAsync(int id);

        Task<bool> AddAsync(Account model);

        Task<bool> UpdateAsync(Account model);

        Task<bool> DeleteAsync(int id);
    }
}