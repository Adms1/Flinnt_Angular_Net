using Flinnt.Business.ViewModels;
using System.Collections.Generic;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundParentJobs : IBackgroundJobs
    {
        void ImportParents(List<ParentViewModel> parentViewModels);
    }
}