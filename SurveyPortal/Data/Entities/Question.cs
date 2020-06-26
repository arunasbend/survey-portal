using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public enum QuestionType { check, radio, text, range }

    public class Question : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Mandatory { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }

        public List<Answer> Answer { get; set; }
    }
}
