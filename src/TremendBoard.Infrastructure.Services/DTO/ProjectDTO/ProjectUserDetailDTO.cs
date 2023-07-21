﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.DTO.ProjectDTO
{
    public class ProjectUserDetailDTO
    {
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRoleName { get; set; }
    }
}
