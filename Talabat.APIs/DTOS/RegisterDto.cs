using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string displayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]

        public string PhoneNumber { get; set; }

    }
}
