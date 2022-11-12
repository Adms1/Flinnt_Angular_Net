using Flinnt.Business.ViewModels;
using Flinnt.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Services
{
    public interface IStudentService : IBaseService<Student>
    {
        Task<List<StudentViewModel>> GetAllAsync();
        Task<StudentViewModel> GetAsync(int id);
        Task<StudentViewModel> AddAsync(StudentViewModel model);
        Task<List<StudentViewModel>> ValidateStudent(StudentViewModel model);
    }
}