using System.Collections.Generic;

namespace SurveyPortal.DataContracts.Dto
{
    public class QuestionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Mandatory { get; set; }
        public List<string> Check { get; set; }
        public List<string> Radio { get; set; }
        public RangeDto Range { get; set; }
        public TextareaDto Textarea { get; set; }
    }
}