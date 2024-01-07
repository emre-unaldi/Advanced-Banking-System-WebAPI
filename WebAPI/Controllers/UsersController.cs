using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
        {
            CreateUserResponse response = await Mediator.Send(createUserCommand);

            return Created("User Created Successfly", response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdateUserResponse response = await Mediator.Send(updateUserCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteUserCommand deleteUserCommand = new() { Id = id };
            DeleteUserResponse response = await Mediator.Send(deleteUserCommand);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemResponse> response = await Mediator.Send(getListUserQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdUserQuery getByIdUserQuery = new() { Id = id };
            GetByIdUserResponse response = await Mediator.Send(getByIdUserQuery);

            return Ok(response);
        }
    }
}
