using System.Threading;
using System.Threading.Tasks;

namespace Sample.Services.BackgroundJobs.ScopedTasks
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}