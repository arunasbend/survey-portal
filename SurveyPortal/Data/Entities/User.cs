using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurveyPortal.Data.Entities
{
    public class User : IdentityUser
    {
        public UserGroup UserGroup { get; set; }
        public Guid? UserGroupId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}
