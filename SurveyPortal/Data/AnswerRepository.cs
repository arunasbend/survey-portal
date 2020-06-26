using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace SurveyPortal.Data
{
    public interface IAnswerRepository : IGenericCrudRepository<Answer, UpdateAnswerRequest, Answer>
    {
    }

    public class AnswerRepository : GenericCrudRepository<Answer, UpdateAnswerRequest, Answer>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext dbContext) : base(dbContext, dbContext.Answers)
        {
        }
    }
}
