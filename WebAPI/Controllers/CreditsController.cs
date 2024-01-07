using Application.Features.Credits.Commands.Application;
using Application.Features.Credits.Commands.Approval;
using Application.Features.Credits.Commands.Delete;
using Application.Features.Credits.Commands.Update;
using Application.Features.Credits.Queries.GetById;
using Application.Features.Credits.Queries.GetList;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : BaseController
    {
        [HttpPost("creditApplication")]
        public async Task<IActionResult> CreditApplication([FromBody] ApplicationCreditCommand applicationCreditCommand)
        {
            ApplicationCreditResponse response = await Mediator.Send(applicationCreditCommand);

            return Ok(response);
        }

        [HttpPost("creditApproval/{id}")]
        public async Task<IActionResult> CreditApproval([FromRoute] int id)
        {
            ApprovalCreditCommand approvalCreditCommand = new() { Id = id };
            ApprovalCreditResponse response = await Mediator.Send(approvalCreditCommand);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCreditCommand updateCreditCommand)
        {
            UpdateCreditResponse response = await Mediator.Send(updateCreditCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteCreditCommand deleteCreditCommand = new() { Id = id };
            DeleteCreditResponse response = await Mediator.Send(deleteCreditCommand);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdCreditQuery getByIdCreditQuery = new() { Id = id };
            GetByIdCreditResponse response = await Mediator.Send(getByIdCreditQuery);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCreditQuery getListCreditQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCreditListItemResponse> response = await Mediator.Send(getListCreditQuery);

            return Ok(response);
        }
    }
}