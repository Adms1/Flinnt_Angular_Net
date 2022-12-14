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
    public class MediaTypeService : ServiceBase, IMediaTypeService
    {
        public MediaTypeService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<MediaTypeViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<MediaTypeViewModel>>(await unitOfWork.MediaTypeRepository.GetAllAsync());
            return result.ToList();
        }
    }
}