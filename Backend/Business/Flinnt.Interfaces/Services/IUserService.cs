using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetAsync(long id);

        Task<User> AddAsync(User model);

        Task<bool> UpdateAsync(User model);

        Task<bool> DeleteAsync(int id);
    }
}