using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService : ITimeService
    {
        private DateTime CurrentTime;

        public TimeService() 
        {
            CurrentTime = DateTime.Now;
        }

        public DateTime GetCurrentTime()
        {
            return CurrentTime;
        }
    }
}
