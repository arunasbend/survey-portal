using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public class QuestionAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }

        public Question Question { get; set; }
        public Guid QuestionId { get; set; }

        public List<SelectedAnswer> SelectedAnswers { get; set; }
    }
}
