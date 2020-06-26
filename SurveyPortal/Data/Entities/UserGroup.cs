using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public class UserGroup : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public List<User> Users { get; set; }
    }
}