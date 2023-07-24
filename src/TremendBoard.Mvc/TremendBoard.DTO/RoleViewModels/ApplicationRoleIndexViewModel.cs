using System.Collections.Generic;

namespace TremendBoard.DTO.RoleViewModels
{
    public class ApplicationRoleIndexViewModel
    {
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }
    }
}
