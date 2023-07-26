using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.DTO.RoleViewModels;
using TremendBoard.DTO.UserViewModels;

namespace TremendBoard.DTO.ProjectViewModels
{
    public class ProjectDetailViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }

        [Display(Name = "Project Status")]
        public string ProjectStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Project Deadline")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectDeadline { get; set; }

        public IList<ProjectUserDetailViewModel> ProjectUsers { get; set; }
        public IEnumerable<UserDetailViewModel> Users { get; set; }
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }
    }
}
