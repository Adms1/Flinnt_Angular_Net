using Flinnt.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundStudentJobs : IBackgroundJobs
    {
        Task ImportStudentsAsync(List<StudentViewModel> studentViewModels);
    }
}