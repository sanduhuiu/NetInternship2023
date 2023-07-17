using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public Task CreateProject();
    }
}
