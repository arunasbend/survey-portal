using System;
using System.Collections.Generic;
using System.Linq;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class QuestionDataSet
    {
        public static List<Question> Data()
        {
            return new List<Question>
            {
                new Question
                {
                    Id = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                    Title = "What is the best color",
                    Description = "Describe the best color",
                    Mandatory = true,
                    SurveyId = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    Type = QuestionType.radio,
                    Answers = QuestionAnswerDataSet.Data().Where(x => x.QuestionId == Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee")).ToList(),
                },
                new Question
                {
                    Id = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                    Title = "What colors match up the best",
                    Description = "Match some colors",
                    Mandatory = false,
                    SurveyId = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    Type = QuestionType.check,
                    Answers = QuestionAnswerDataSet.Data().Where(x => x.QuestionId == Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109")).ToList(),
                },
                new Question
                {
                    Id = Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1"),
                    Title = "What do you know about color",
                    Description = "Expand up a little bit",
                    Mandatory = false,
                    SurveyId = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    Type = QuestionType.text,
                    Answers = QuestionAnswerDataSet.Data().Where(x => x.QuestionId == Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1")).ToList(),
                },
                new Question
                {
                    Id = Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e"),
                    Title = "How much do you like drawing",
                    Description = "Rate it",
                    Mandatory = false,
                    SurveyId = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    Type = QuestionType.range,
                    Answers = QuestionAnswerDataSet.Data().Where(x => x.QuestionId == Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e")).ToList(),
                },
            };
        }
    }
}
