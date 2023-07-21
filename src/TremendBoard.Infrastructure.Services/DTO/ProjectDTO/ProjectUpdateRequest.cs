using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TremendBoard.Infrastructure.Services.DTO.RoleDTO;
using TremendBoard.Infrastructure.Services.DTO.User;

namespace TremendBoard.Infrastructure.Services.DTO.ProjectDTO
{
    /// <summary>
    /// This is a DTO (data transfer object) use to pass data between Infrascructure and Mvc layers. 
    /// Specifically, it passes the input from the client to the service in order to update an existing project entity.
    /// </summary>
    public class ProjectUpdateRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectStatus { get; set; }

        public IList<ProjectUserDetailDTO> ProjectUsers { get; set; }
        public IEnumerable<UserDetailDTO> Users { get; set; }
        public IEnumerable<ApplicationRoleDetailDTO> Roles { get; set; }
    }
}
