using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        Task ReccuringJob();
        Task DelayedJob();
        void ContinuationJob();
    }
}
