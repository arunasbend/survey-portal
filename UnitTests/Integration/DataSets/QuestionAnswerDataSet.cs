using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class QuestionAnswerDataSet
    {
        public static List<QuestionAnswer> Data()
        {
            return new List<QuestionAnswer>
            {
                new QuestionAnswer
                {
                    Id = Guid.Parse("6ab43d56-8a92-41de-856b-d1e71a47ab2d"),
                    Answer = "Radio1",
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                },
                new QuestionAnswer
                {
                    Id = Guid.Parse("f870a965-f4ec-42f4-82fd-203a496b94d6"),
                    Answer = "Radio2",
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                },
                new QuestionAnswer
                {
                    Id = Guid.Parse("d2966cfa-dcf0-4f18-a0bd-73de0137b8e5"),
                    Answer = "Radio3",
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                },

                
                new QuestionAnswer
                {
                    Id = Guid.Parse("cd5d90b5-4964-4814-99fb-de8cb0880247"),
                    Answer = "Check1",
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                },
                new QuestionAnswer
                {
                    Id = Guid.Parse("132d5e56-0e4e-4196-8aef-0ca783d18338"),
                    Answer = "Check2",
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                },
                new QuestionAnswer
                {
                    Id = Guid.Parse("ee0e755d-c40c-4c25-91bd-326d882e87b3"),
                    Answer = "Check3",
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                },

                
                new QuestionAnswer
                {
                    Id = Guid.Parse("a3864dbc-a5fe-4bae-bcad-3d8f03cdf04f"),
                    Answer = "TextArea answer",
                    QuestionId = Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1"),
                },

                
                new QuestionAnswer
                {
                    Id = Guid.Parse("b7aa2668-1456-4cb5-8230-22452b21d901"),
                    Answer = "1|5|10",
                    QuestionId = Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e"),
                },
            };
        }
    }
}
