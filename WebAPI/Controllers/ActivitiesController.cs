using Application.Features.Activities.Commands.Delete;
using Application.Features.Activities.Commands.DepositMoney;
using Application.Features.Activities.Commands.MoneyTransfer;
using Application.Features.Activities.Commands.WithdrawMoney;
using Application.Features.Activities.Queries.GetById;
using Application.Features.Activities.Queries.GetList;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController
    {
        [HttpPost("depositMoney")]
        public async Task<IActionResult> DepositMoney([FromBody] DepositMoneyActivitiesCommand command)
        {
            DepositMoneyActivitiesResponse response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("withdrawMoney")]
        public async Task<IActionResult> WithdrawMoney([FromBody] WithdrawMoneyActivitiesCommand command)
        {
            WithdrawMoneyActivitiesResponse response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("moneyTransfer")]
        public async Task<IActionResult> MoneyTransfer([FromBody] MoneyTransferActivitiesCommand command)
        {
            MoneyTransferActivitiesResponse response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteActivityCommand deleteActivityCommand = new() { Id = id };
            DeleteActivityResponse response = await Mediator.Send(deleteActivityCommand);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListActivityQuery getListActivityQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListActivityListItemResponse> response = await Mediator.Send(getListActivityQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdActivityQuery getByIdActivityQuery = new() { Id = id };
            GetByIdActivityResponse response = await Mediator.Send(getByIdActivityQuery);

            return Ok(response);
        }
    }
}
