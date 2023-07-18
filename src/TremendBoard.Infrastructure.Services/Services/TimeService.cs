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
        private readonly DateTime _time;
        public TimeService() 
        {
            _time = DateTime.Now;
        }


        public DateTime GetCurrentTime()
        {
            return _time;
        }
    }
}
