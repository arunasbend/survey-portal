using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public class Answer: IEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<SelectedAnswer> Selected { get; set; }

        public Guid SurveySubmissionId { get; set; }
        public SurveySubmission SurveySubmission { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}