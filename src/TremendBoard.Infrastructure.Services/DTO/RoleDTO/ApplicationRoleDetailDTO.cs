using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TremendBoard.Infrastructure.Services.DTO.RoleDTO
{
    public class ApplicationRoleDetailDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string UserRoleName { get; set; }
        public string StatusMessage { get; set; }
    }
}
