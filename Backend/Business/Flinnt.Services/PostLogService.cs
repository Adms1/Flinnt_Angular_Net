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
    public class PostLogService : ServiceBase, IPostLogService
    {
        public PostLogService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }
        public async Task<List<PostLogViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<PostLogViewModel>>(await unitOfWork.PostLogRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<bool> AddAsync(PostLogViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostLogRepository.AddAsync(mapper.Map<PostLogViewModel, PostLog>(model)));

            if (data.PostId > 0)
                return true;
            else
                return false;
        }
    }
}