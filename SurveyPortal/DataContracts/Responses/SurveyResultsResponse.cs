using System;
using System.Collections.Generic;

namespace SurveyPortal.DataContracts.Responses
{
    public class SurveyResultsResponse
    {
        public SurveyResultsHeading Heading { get; set; }
        public List<SurveyResultsQuestion> Questions { get; set; }
    }

    public class SurveyResultsHeading
    {
        public Guid SurveyId { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public int TotalSubmissions { get; set; }
        public string Title { get; set; }
    }

    public class SurveyResultsQuestion
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public List<SurveyResultsRow> Rows { get; set; }
    }

    public class SurveyResultsRow
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public string UserName { get; set; }
        public string TextArea { get; set; }
        // Add User Groups here when they are implemented
    }
}
