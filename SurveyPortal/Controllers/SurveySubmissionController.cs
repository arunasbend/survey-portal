using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data.Entities;
using SurveyPortal.Data;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;

namespace SurveyPortal.Controllers
{
    [Route("api/survey-submission")]
    public class SurveySubmissionController : CrudController<SurveySubmission, object, CreateSurveySubmissionRequest>
    {
        private readonly ISurveySubmissionRepository _repo;
        private readonly ISurveyRepository _surveyRepository;
        private readonly UserManager<User> _userManager;

        public SurveySubmissionController(
            ISurveySubmissionRepository repo,
            ISurveyRepository surveyRepository,
            UserManager<User> userManager) : base(repo)
        {
            _repo = repo;
            _surveyRepository = surveyRepository;
            _userManager = userManager;
        }

        [HttpGet("results/{surveyId}")]
        public IActionResult GetSubmissionResults(Guid surveyId)
        {
            try
            {
                var survey = _surveyRepository.GetById(surveyId);

                if (survey == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        ErrorMessage = "There are no surveys with that Id."
                    });
                }

                var submissions = _repo.GetBySurveyId(surveyId);

                if (submissions.Count == 0)
                {
                    return BadRequest(new ErrorResponse
                    {
                        ErrorMessage = "There are no survey submissions for the given survey."
                    });
                }

                var questions = _repo.GetSurveyQuestionResults(survey, _userManager);

                return Ok(new SurveyResultsResponse
                {
                    Heading = new SurveyResultsHeading
                    {
                        Description = survey.Description,
                        DueDate = survey.DueDate.ToString("yyyy-MM-dd"),
                        TotalSubmissions = submissions.Count,
                        SurveyId = survey.Id,
                        Title = survey.Title,
                    },
                    Questions = questions,
                });
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public new IActionResult Create([FromBody] CreateSurveySubmissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                _repo.Create(request, userId);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}