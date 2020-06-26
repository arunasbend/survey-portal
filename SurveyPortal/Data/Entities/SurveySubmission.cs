using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public class SurveySubmission : IEntity
    {
        public Guid Id { get; set; }
        public List<Answer> Answers { get; set; }

        public Survey Survey { get; set; }
        public Guid SurveyId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }   
    }
}
