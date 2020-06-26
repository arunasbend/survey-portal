using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class SurveyDataSet
    {
        public static List<Survey> Data()
        {
            return new List<Survey>
            {
                new Survey
                {
                    Id = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    Title = "Survey1",
                    Description = "Description",
                    CreatedOn = new DateTime(2020,1,1,12,0,0),
                    ModifiedOn = new DateTime(2020,1,2,12,0,0),
                    Status = Status.Published,
                    DueDate = new DateTimeOffset(new DateTime(2020,2,1,12,0,0)),
                    UserGroups = UserGroupDataSet.Data(),
                    OwnerId = 1,
                }
            };
        }
    }
}
