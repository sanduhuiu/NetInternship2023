using Hangfire;
using Hangfire.Logging.LogProviders;
using Microsoft.AspNetCore.Mvc;
using System;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using Serilog;

namespace TremendBoard.Mvc.Controllers
{
    public class JobTestController : Controller
    {
        private readonly IJobTestService _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobTestController(IJobTestService jobTestService, 
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }


        [HttpGet("/DelayedJob")]
        public ActionResult DelayedJob()
        {
            Log.Information("The request started");
            _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(30));
            return Ok();
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult ReccuringJob()
        {
            Log.Information("The request for reccuring job started");
            _recurringJobManager.AddOrUpdate("my-recurring-job", () => _jobTestService.ReccuringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("/ContinuationJob")]
        public ActionResult ContinuationJob()
        {
            Log.Information("The request for Continuation Job started");
            var parentJobId = _backgroundJobClient.Enqueue(() => Log.Information("Parent Job"));

            // the continuation job that runs after parentJob is completed
            var continuationJob = _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());


            // Create a continuation job that runs after the parent job (ContinuationJob) is completed
            _backgroundJobClient.ContinueJobWith(continuationJob, () => Log.Information("This is continuation job that announces the prime number continuation job has finished"));
            return Ok();
        }



    }
}
