using System;
using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Dto;

namespace SurveyPortal.Controllers
{

    [Route("api/surveys")]
    public class SurveyController : CrudController<Survey, SurveyDto, SurveyDto>
    {
        private readonly ISurveyRepository _repo;

        public SurveyController(ISurveyRepository repo): base(repo)
        {
            _repo = repo;
        }

        [HttpGet("edit/{id}")]
        public IActionResult GetForUpdateById(Guid id)
        {
            try
            {
                return Ok(_repo.GetForUpdateById(id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("overview")]
        public IActionResult GetOverview()
        {
            try
            {
                return Ok(_repo.GetSurveyOverview());
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }
    }
}
