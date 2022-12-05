using Flinnt.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundParentJobs : IBackgroundJobs
    {
        Task ImportParentsAsync(List<ParentViewModel> parentViewModels);
    }
}