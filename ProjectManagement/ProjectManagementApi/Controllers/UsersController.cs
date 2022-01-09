using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Users.Queries.CheckIfUserWithEmailExistsQuery;
using ProjectManagement.Core.UseCases.Users.Queries.FindUser;
using ProjectManagement.Core.UseCases.Users.Queries.FindUserInProject;
using ProjectManagement.Core.UseCases.Users.ViewsModels;

namespace ProjectManagementApi.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpGet("ifExists/{email}")]
        public async Task<ActionResult> CheckIfUserWithEmailExists(string email)
        {
            try
            {
                var query = new CheckIfUserWithEmailExistsQuery(email);
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("findUsers")]
        public async Task<ActionResult> FindUsers([FromQuery]string term, [FromQuery]int? projectId)
        {
            try
            {
                UserVm result;
                if (projectId is > 0)
                {
                    var query = new FindUserInProjectQuery(term, projectId.Value);
                    result = await Mediator.Send(query);
                }
                else
                {
                    var query = new FindUserQuery(term);
                    result = await Mediator.Send(query);
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}