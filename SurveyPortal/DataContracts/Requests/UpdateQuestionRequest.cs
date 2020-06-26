using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
  public class UpdateQuestionRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
