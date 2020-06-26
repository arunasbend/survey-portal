using Microsoft.AspNetCore.Mvc;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.Data.Entities;
using SurveyPortal.Data;

namespace SurveyPortal.Controllers
{
    [Route("api/usergroup")]
    public class UserGroupController : CrudController<UserGroup, UpdateUserGroupRequest, CreateUserGroupRequest>
    {
        public UserGroupController(IUserGroupRepository repo) : base(repo)
        {
        }
    }
}