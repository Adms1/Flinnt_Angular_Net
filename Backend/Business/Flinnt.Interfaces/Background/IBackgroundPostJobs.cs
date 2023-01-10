using Flinnt.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundPostJobs : IBackgroundJobs
    {
        Task ScheduledPostAsync(int postId);
    }
}