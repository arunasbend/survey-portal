using System;

namespace SurveyPortal.DataContracts.Dto
{
    public class UserGroupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }
    }
}