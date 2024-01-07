using Application.Features.Accounts.Commands.Create;
using Application.Features.Accounts.Commands.Delete;
using Application.Features.Accounts.Commands.Update;
using Application.Features.Accounts.Queries.GetById;
using Application.Features.Accounts.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAccountCommand createAccountCommand)
        {
            CreateAccountResponse response = await Mediator.Send(createAccountCommand);

            return Created("Account Created Successfly", response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAccountCommand updateAccountCommand)
        {
            UpdateAccountResponse response = await Mediator.Send(updateAccountCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteAccountCommand deleteAccountCommand = new() { Id = id };
            DeleteAccountResponse response = await Mediator.Send(deleteAccountCommand);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAccountQuery getListAccountQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAccountListItemResponse> response = await Mediator.Send(getListAccountQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdAccountQuery getByIdAccountQuery = new() { Id = id };
            GetByIdAccountResponse response = await Mediator.Send(getByIdAccountQuery);

            return Ok(response);
        }
    }
}
