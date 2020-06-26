using System;

namespace SurveyPortal.Data.Entities
{
    public class SelectedAnswer
    {
        public Guid Id { get; set; }
        public string Selected { get; set; }

        public Answer Answer { get; set; }
        public Guid AnswerId { get; set; }

        public Guid QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
    }
}
