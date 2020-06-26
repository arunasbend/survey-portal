using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.DataContracts.Dto
{
    public class SurveyDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<UserGroupDto> UserGroups { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
