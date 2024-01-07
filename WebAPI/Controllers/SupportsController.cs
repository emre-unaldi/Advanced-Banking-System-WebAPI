using Application.Features.Supports.Commands.Create;
using Application.Features.Supports.Commands.Delete;
using Application.Features.Supports.Commands.Update;
using Application.Features.Supports.Queries.GetById;
using Application.Features.Supports.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSupportCommand createSupportCommand)
        {
            CreateSupportResponse response = await Mediator.Send(createSupportCommand);

            return Created("Support Created Successfly", response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSupportCommand updateSupportCommand)
        {
            UpdateSupportResponse response = await Mediator.Send(updateSupportCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteSupportCommand deleteSupportCommand = new() { Id = id };
            DeleteSupportResponse response = await Mediator.Send(deleteSupportCommand);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSupportQuery getListSupportQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSupportListItemResponse> response = await Mediator.Send(getListSupportQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdSupportQuery getByIdSupportQuery = new() { Id = id };
            GetByIdSupportResponse response = await Mediator.Send(getByIdSupportQuery);

            return Ok(response);
        }
    }
}
