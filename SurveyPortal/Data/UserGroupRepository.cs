using System.Linq;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace SurveyPortal.Data
{
    public interface IUserGroupRepository : IGenericCrudRepository<UserGroup, UpdateUserGroupRequest, CreateUserGroupRequest>
    {
        UserGroup GetByTitle(string title);
    }

    public class UserGroupRepository : GenericCrudRepository<UserGroup, UpdateUserGroupRequest, CreateUserGroupRequest>, IUserGroupRepository
    {
        private readonly AppDbContext _dbContext;

        public UserGroupRepository(AppDbContext dbContext) : base(dbContext, dbContext.UserGroups)
        {
            _dbContext = dbContext;
        }

        public UserGroup GetByTitle(string title)
        {
            return _dbContext.UserGroups.Single(x => x.Title == title);
        }
    }
}
