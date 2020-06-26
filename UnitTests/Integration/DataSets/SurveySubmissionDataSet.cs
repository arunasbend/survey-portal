using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class SurveySubmissionDataSet
    {
        public static List<SurveySubmission> Data()
        {
            return new List<SurveySubmission>
            {
                new SurveySubmission
                {
                    Id = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    SurveyId = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    UserId = "10f9cf2f-d0dc-4396-817e-1293a92eacf3",
                    //Answers = AnswersDataSet.Data(),
                }
            };
        }
    }
}
