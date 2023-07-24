using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Mvc.Controllers
{
    public class JobTestController : Controller
    {
        private readonly IJobTestService _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogger<JobTestController> _logger;

        public JobTestController(IJobTestService jobTestService, 
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            ILogger<JobTestController> logger)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _logger = logger;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob(double timeInSeconds)
        {
            var jobId = _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), System.TimeSpan.FromSeconds(timeInSeconds));
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] Job ID: {jobId}. Delayed job will start in {timeInSeconds} seconds.");
            return Ok();
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            RecurringJob.AddOrUpdate(() => _jobTestService.ReccuringJob(), Cron.Minutely);
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}]. Recurring job initiated.");
            return Ok();
        }
        
        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var parentId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            _logger.LogInformation($"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}]. Fire and Forget job initiated. Job ID: {parentId}.");

            _backgroundJobClient.ContinueJobWith(parentId, () => _jobTestService.ContinuationJob(parentId));

            return Ok();
        }
    }
}