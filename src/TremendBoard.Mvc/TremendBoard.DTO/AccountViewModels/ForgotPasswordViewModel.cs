using System.ComponentModel.DataAnnotations;

namespace TremendBoard.DTO.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
