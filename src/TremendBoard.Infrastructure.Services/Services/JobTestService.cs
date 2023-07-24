using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class JobTestService: IJobTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<JobTestService> _logger;
        public JobTestService(IUnitOfWork unitOfWork, ILogger<JobTestService> logger) 
        {
            _unitOfWork = unitOfWork;   
            _logger = logger;
        }
        public void FireAndForgetJob()
        {
            System.Threading.Thread.Sleep(500);
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Hello from a Fire and Forget job!");
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Sleep for 5 seconds.");
            System.Threading.Thread.Sleep(5000);
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Fire and Forget job finished.");
        }
        public void ReccuringJob()
        {
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Recurring job starting.");

            Console.Beep(1000, 2000);
            Console.Beep();
            Console.Beep(1000, 2000);

            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Recurring job finished.");
        }
        public async Task DelayedJob()
        {
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Delayed job starting.");

            var projectsFromDb = await _unitOfWork.Project.GetAllAsync();
            foreach(var project in projectsFromDb)
            {
                if (string.IsNullOrEmpty(project.Name))
                    continue;

                if (char.IsLower(project.Name[0]))
                    project.Name = project.Name.Substring(0, 1).ToUpper() + project.Name.Substring(1);
            }

            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Delayed job finshed.");
        }
        public void ContinuationJob(string parentId)
        {
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Hello from a Continuation job! Parent ID: {parentId}.");
        }
    }
}
