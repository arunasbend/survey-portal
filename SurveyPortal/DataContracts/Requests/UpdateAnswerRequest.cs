using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
    public class UpdateAnswerRequest
    {
        [Required]
        public string Text { get; set; }

        // TODO: Double check usage
        // [Required]
        // public List<int> Selected { get; set; }
    }
}
