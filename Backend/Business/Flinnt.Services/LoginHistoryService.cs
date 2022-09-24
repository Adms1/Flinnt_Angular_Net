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
    public class LoginHistoryService : ServiceBase, ILoginHistoryService
    {
        public LoginHistoryService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<LoginHistory> AddAsync(LoginHistory model)
        {
            return await Task.FromResult(await unitOfWork.LoginHistoryRepository.AddAsync(model));
        }
    }
}