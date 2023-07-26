using System.Collections.Generic;

namespace TremendBoard.DTO.UserViewModels
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserDetailViewModel> Users { get; set; }
    }
}
