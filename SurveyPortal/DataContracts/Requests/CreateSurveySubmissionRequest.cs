using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Requests
{
    public class CreateSurveySubmissionRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public List<CreateSurveySubmissionQuestion> questions { get; set; }
    }

    public class CreateSurveySubmissionQuestion
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public int type { get; set; }
        public object data { get; set; }
    }
}
