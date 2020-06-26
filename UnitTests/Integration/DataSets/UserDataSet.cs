using System;
using System.Collections.Generic;
using SurveyPortal.Data.Entities;

namespace UnitTests.Integration.DataSets
{
    public static class UserDataSet
    {
        public static List<User> Data()
        {
            return new List<User>
            {
                new User
                {
                    Id = "a034898c-e6d8-4856-8125-cf7817c3075f",
                    UserName = "root",
                    Email = "root@root.com",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    UserGroupId = Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"),
                },
                new User
                {
                    Id = "10f9cf2f-d0dc-4396-817e-1293a92eacf3",
                    UserName = "user",
                    Email = "user@user.com",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    UserGroupId = Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05"),
                },
                new User
                {
                    Id = "25c88635-850b-49de-a369-b2d69ef4361f",
                    UserName = "nogroup",
                    Email = "nogroup@nogroup.com",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    UserGroupId = null,
                },
            };
        }
    }
}
