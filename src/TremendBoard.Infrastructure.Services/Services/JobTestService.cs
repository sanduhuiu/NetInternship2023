﻿using System;
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
        public void ReccuringJob()
        {
            Console.WriteLine("Hello from a Scheduled job!");
        }
        public async Task DelayedJob()
        {
            Console.WriteLine("Delayed job started at: " + DateTime.Now);

            var users = await _unitOfWork.User.GetAllAsync();
            foreach (var user in users)
            {
                user.FirstName += "-delayedByJob-" + DateTime.Now.ToString("HH:mm:ss");
            }
            await _unitOfWork.SaveAsync();
            Console.WriteLine("Delayed job ended at: " + DateTime.Now);
        } 

        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}
