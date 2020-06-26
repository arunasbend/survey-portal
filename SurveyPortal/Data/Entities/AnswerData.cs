using System;

namespace SurveyPortal.DataContracts.Requests
{
    public class AnswerData
    {
        public Guid Id { get; set; }
        public bool Checked { get; set; }
    }
}