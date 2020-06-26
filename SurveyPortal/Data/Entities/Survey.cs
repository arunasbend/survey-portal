using System;
using System.Collections.Generic;

namespace SurveyPortal.Data.Entities
{
    public enum Status { Disabled, Published, Completed }
    public class Survey : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public List<SurveySubmission> Submissions { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        // TODO: change to GUID
        public int OwnerId { get; set; }
        public Status Status { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Description { get; set; }
    }
}
