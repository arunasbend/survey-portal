using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
    public class LoginUserRequest
    {
        [Required]
        public string Username { get; set; }
    
        [Required]
        public string Password { get; set; }
    }
}
