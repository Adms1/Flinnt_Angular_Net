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
    public class StudentService : ServiceBase, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork, IMapper _mapper) : base(unitOfWork, _mapper)
        {
        }

        public async Task<List<StudentViewModel>> GetAllAsync()
        {
            var result = mapper.Map<List<StudentViewModel>>(await unitOfWork.StudentRepository.GetAllAsync());
            return result.ToList();
        }

        public async Task<StudentViewModel> GetAsync(int id)
        {
            return mapper.Map<StudentViewModel>(await unitOfWork.StudentRepository.GetAsync(id));
        }

        public async Task<StudentViewModel> AddAsync(StudentViewModel model)
        {
            return mapper.Map<StudentViewModel>(await Task.FromResult(await unitOfWork.StudentRepository.AddAsync(mapper.Map<StudentViewModel, Student>(model))));
        }

        public async Task<List<StudentViewModel>> ValidateStudent(StudentViewModel model)
        {
            return mapper.Map<List<StudentViewModel>>
                (await Task.FromResult(await unitOfWork.StudentRepository.FindByAsync(x => 
                x.EmailId == model.EmailId
            && x.RollNo == model.RollNo
            && x.MobileNo == model.MobileNo
            && x.Grno == model.Grno
            && x.UserId == model.UserId
            && x.FirstName.ToLower() == model.FirstName.ToLower()
            && x.LastName.ToLower() == model.LastName.ToLower())));
        }
    }
}