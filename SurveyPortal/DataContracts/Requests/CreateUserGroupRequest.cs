using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
  public class CreateUserGroupRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
