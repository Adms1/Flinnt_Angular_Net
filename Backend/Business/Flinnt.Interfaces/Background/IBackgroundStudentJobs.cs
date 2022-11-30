using Flinnt.Business.ViewModels;
using System.Collections.Generic;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundStudentJobs : IBackgroundJobs
    {
        void ImportStudents(List<StudentViewModel> studentViewModels);
    }
}