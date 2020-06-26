using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
