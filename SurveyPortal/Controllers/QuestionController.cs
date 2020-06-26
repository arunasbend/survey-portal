using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace SurveyPortal.Controllers
{
    [Route("api/questions")]
    public class QuestionController: CrudController<Question, UpdateQuestionRequest, CreateQuestionRequest>
    {
        public QuestionController(IQuestionRepository repo): base(repo)
        {
        }
    }
}
