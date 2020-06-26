using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
    public class UpdateUserGroupRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
