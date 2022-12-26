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
    public class MediaEmbedServiceService : ServiceBase, IMediaEmbedService
    {
        public MediaEmbedServiceService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<MediaEmbedServiceViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<MediaEmbedServiceViewModel>>(await unitOfWork.MediaEmbedRepository.GetAllAsync());
            return result.ToList();
        }
    }
}