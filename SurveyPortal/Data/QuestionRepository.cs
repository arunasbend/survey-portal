using System;
using System.Linq;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace SurveyPortal.Data
{
    public interface IQuestionRepository: IGenericCrudRepository<Question, UpdateQuestionRequest, CreateQuestionRequest>
    {
    }

    public class QuestionRepository : GenericCrudRepository<Question, UpdateQuestionRequest, CreateQuestionRequest>, IQuestionRepository
    {
        private readonly AppDbContext _dbContext;

        public QuestionRepository(AppDbContext dbContext) : base(dbContext, dbContext.Questions)
        {
            _dbContext = dbContext;
        }

        public new Question Create(CreateQuestionRequest request)
        {
            var id = new Guid();
            
            var item = new Question
            {
                Id = id,
                SurveyId = request.SurveyId,
                Title = request.Title,
                Mandatory = request.Mandatory,
                Description = request.Description,
                Type = request.Type,
            };
            var answers =
                request.Answers.Select(x => new QuestionAnswer { Answer = x, Id = Guid.NewGuid(), Question = item})
                    .ToList();
            
            _dbContext.Questions.Add(item);

            _dbContext.QuestionAnswers.AddRange(answers);

            return item;
        }
    }
}
