using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class JobTestService: IJobTestService
    {

        private readonly IUnitOfWork _unitOfWork;

        public JobTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }
        public async Task ReccuringJob()
        {

            try
            {
                Log.Information("Recurring job started");
                var projects = await _unitOfWork.Project.GetAllAsync();
                foreach (var project in projects)
                {
                    project.CompletedDate = DateTime.Now;
                }
                await _unitOfWork.SaveAsync();

                Log.Information("Recurring job completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in recurring job"); 
            }
        }
        public async Task DelayedJob()
        {
            Log.Information("Delayed job started");
            var users = await _unitOfWork.User.GetAllAsync();
            foreach (var user in users)
            {
                user.FirstName += "-delayedByJob-" + DateTime.Now.ToString("HH:mm:ss");
            }
            await _unitOfWork.SaveAsync();
            Log.Information("Delayed job ended");
        } 

        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}
