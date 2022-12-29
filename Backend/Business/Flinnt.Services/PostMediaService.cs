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
    public class PostMediaService : ServiceBase, IPostMediaService
    {
        public PostMediaService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<bool> AddAsync(PostMediumViewModel model)
        {
            var data = await Task.FromResult(await unitOfWork.PostMediaRepository.AddAsync(mapper.Map<PostMediumViewModel, PostMedium>(model)));

            if (data.PostMediaId > 0)
                return true;
            else
                return false;
        }
    }
}