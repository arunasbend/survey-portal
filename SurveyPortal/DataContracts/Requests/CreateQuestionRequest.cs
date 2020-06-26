using SurveyPortal.Data.Entities;
using System;
using System.Collections.Generic;

namespace SurveyPortal.DataContracts.Requests
{

    public class CreateQuestionRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public QuestionType Type { get; set; }
        public Guid SurveyId { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
    }
}
