using System;
using System.Collections.Generic;
using System.Linq;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class UserGroupDataSet
    {
        public static List<UserGroup> Data()
        {
            var users = UserDataSet.Data();

            return new List<UserGroup>
            {
                new UserGroup
                {
                    Id = Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"),
                    Title = "Admins",
                    Users = users.Where(x => x.UserGroupId == Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c")).ToList(),
                },
                new UserGroup
                {
                    Id = Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05"),
                    Title = "Users",
                    Users = users.Where(x => x.UserGroupId == Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05")).ToList(),
                },
            };
        }
    }
}
