using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TremendBoard.DTO.ProjectViewModels
{
    public class ProjectUserListViewModel
    {
        public IEnumerable<ProjectUserDetailViewModel> ProjectUsers { get; set; }
    }
}
