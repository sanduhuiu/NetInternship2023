using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.DTO.User
{
    public class UserDetailDTO
    {
        public string Id { get; set; }

        //public IEnumerable<ApplicationUserRole> ApplicationRoles { get; set; }

        public string UserRoleId { get; set; }

        public string CurrentUserRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
