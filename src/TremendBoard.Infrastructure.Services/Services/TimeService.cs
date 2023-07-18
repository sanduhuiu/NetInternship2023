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
        private readonly IDateTime _time;
        public TimeService(IDateTime time) 
        {
            _time = time;
        }


        public DateTime GetCurrentTime()
        {
            return _time.Now;
        }
    }
}
