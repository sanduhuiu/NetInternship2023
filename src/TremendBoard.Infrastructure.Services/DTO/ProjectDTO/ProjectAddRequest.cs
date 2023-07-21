using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TremendBoard.Infrastructure.Services.DTO.ProjectDTO
{
    /// <summary>
    /// This is a DTO (data transfer object) use to pass data between Infrascructure and Mvc layers. 
    /// Specifically, it passes the input from the client to the service in order to create a new project entity.
    /// </summary>
    public class ProjectAddRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectStatus { get; set; }
    }
}
