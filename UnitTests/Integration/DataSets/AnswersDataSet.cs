using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class AnswersDataSet
    {
        public static List<Answer> Data()
        {
            return new List<Answer>
            {
                new Answer
                {
                    Id = Guid.Parse("2377a487-5a04-4452-b883-4085c6ea5cbd"),
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Blue",
                },
                new Answer
                {
                    Id = Guid.Parse("304a71f4-8fd7-4002-95c3-b7a111f51078"),
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Red",
                },
                new Answer
                {
                    Id = Guid.Parse("8b328870-93f4-435b-8dd6-b25d5a5b5f49"),
                    QuestionId = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Black",
                },


                new Answer
                {
                    Id = Guid.Parse("bf1eb3a0-b753-4cca-bcda-16c0ffb78e62"),
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Yellow",
                },
                new Answer
                {
                    Id = Guid.Parse("e249a513-a5ab-44a5-9568-16a558fa23fd"),
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Red",
                },
                new Answer
                {
                    Id = Guid.Parse("04551457-0aff-4ed3-b36d-15c513596fba"),
                    QuestionId = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Black",
                },


                new Answer
                {
                    Id = Guid.Parse("596d3269-8542-49e5-ad1e-8265a8361a3b"),
                    QuestionId = Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "Best color",
                },


                new Answer
                {
                    Id = Guid.Parse("c8671bdc-eca1-4e93-ad76-98fbee39f92c"),
                    QuestionId = Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e"),
                    //Selected = new List<SelectedAnswer>(),
                    SurveySubmissionId = Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"),
                    Text = "5",
                },
            };
        }
    }
}
