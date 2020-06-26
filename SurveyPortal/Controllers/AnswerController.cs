using Microsoft.AspNetCore.Mvc;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.Data.Entities;
using SurveyPortal.Data;

namespace SurveyPortal.Controllers
{
    [Route("api/answers")]
    public class AnswerController : CrudController<Answer, UpdateAnswerRequest, Answer>
    {
        public AnswerController(IAnswerRepository repo) : base(repo)
        {
        }
    }
}