using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        Task DelayedJob();
        void ContinuationJob();
    }
}
