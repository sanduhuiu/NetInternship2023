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
            RecurringJob.AddOrUpdate<JobTestService>(x => x.ReccuringJob(), Cron.Minutely); 
            return Ok();
        }


    }
}
