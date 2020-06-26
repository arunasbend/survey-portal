using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class SelectedAnswerDataSet
    {
        public static List<SelectedAnswer> Data()
        {
            return new List<SelectedAnswer>
            {
                new SelectedAnswer
                {
                    Id = Guid.Parse("dd8704c1-4878-4ed9-bd5b-be6223a57d64"),
                    AnswerId = Guid.Parse("2377a487-5a04-4452-b883-4085c6ea5cbd"),
                    QuestionAnswerId = Guid.Parse("6ab43d56-8a92-41de-856b-d1e71a47ab2d"),
                    Selected = "False"
                },
                new SelectedAnswer
                {
                    Id = Guid.Parse("56ff3db4-5472-4295-8ec4-af1d02ec4860"),
                    AnswerId = Guid.Parse("304a71f4-8fd7-4002-95c3-b7a111f51078"),
                    QuestionAnswerId = Guid.Parse("f870a965-f4ec-42f4-82fd-203a496b94d6"),
                    Selected = "False"
                },
                new SelectedAnswer
                {
                    Id = Guid.Parse("f352727b-949f-428d-9ba8-8d8cb9203ae6"),
                    AnswerId = Guid.Parse("8b328870-93f4-435b-8dd6-b25d5a5b5f49"),
                    QuestionAnswerId = Guid.Parse("d2966cfa-dcf0-4f18-a0bd-73de0137b8e5"),
                    Selected = "True"
                },

                
                new SelectedAnswer
                {
                    Id = Guid.Parse("473d8214-9790-4496-be82-ff5851a87270"),
                    AnswerId = Guid.Parse("bf1eb3a0-b753-4cca-bcda-16c0ffb78e62"),
                    QuestionAnswerId = Guid.Parse("cd5d90b5-4964-4814-99fb-de8cb0880247"),
                    Selected = "False"
                },
                new SelectedAnswer
                {
                    Id = Guid.Parse("c89f8a9c-2c2a-4f24-be0a-2fb6f0ed1759"),
                    AnswerId = Guid.Parse("e249a513-a5ab-44a5-9568-16a558fa23fd"),
                    QuestionAnswerId = Guid.Parse("132d5e56-0e4e-4196-8aef-0ca783d18338"),
                    Selected = "False"
                },
                new SelectedAnswer
                {
                    Id = Guid.Parse("53267743-05c4-4ebc-983d-d8f85a0d9972"),
                    AnswerId = Guid.Parse("04551457-0aff-4ed3-b36d-15c513596fba"),
                    QuestionAnswerId = Guid.Parse("ee0e755d-c40c-4c25-91bd-326d882e87b3"),
                    Selected = "True"
                },
            };
        }
    }
}
