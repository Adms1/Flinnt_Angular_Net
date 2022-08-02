using AutoMapper;
using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using Flinnt.Interfaces.Services;
using Flinnt.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flinnt.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
            //_userManager = userManager;
        }

        public async Task<List<AccountModel>> GetAllAsync()
        {
            var result = mapper.Map<List<AccountModel>>(await unitOfWork.AccountRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<AccountModel> GetAsync(int id)
        {
            return mapper.Map<AccountModel>(await unitOfWork.AccountRepository.GetAsync(id));
        }

        public async Task<bool> AddAsync(Account model)
        {
            //var user = new ApplicationUser { UserName = model.Name, Email = "Test@gmail.com" };
            //var result = await _userManager.CreateAsync(user, "xyz@123");
            //if (result.Succeeded)
            //{
            //    await unitOfWork.AccountRepository.AddAsync(model);
            //}
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Account model)
        {
            var person = await unitOfWork.AccountRepository.GetAsync(model.Id);
            if (person != null)
            {
                person.Id = model.Id;
                //MAP other fields
                await unitOfWork.AccountRepository.UpdateAsync(person);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = unitOfWork.AccountRepository.GetAsync(id).Result;
            if (person != null)
            {
                await unitOfWork.AccountRepository.DeleteAsync(person);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}