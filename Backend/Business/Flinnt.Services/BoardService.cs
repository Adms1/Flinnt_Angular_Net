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
    public class BoardService : ServiceBase, IBoardService
    {
        public BoardService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<BoardViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<BoardViewModel>>(await unitOfWork.BoardRepository.GetAllAsync());
            return result.ToList();
        }
    }
}