using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Application.Commands.Model;
using TicketManagement.Application.Queries.Model;

namespace TicketManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data); 
        }


        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var tickets = await _mediator.Send(new GetTicketsQuery( page,pageSize));
            return Ok(tickets);
        }


        [HttpPut("handle/{id}")]
        public async Task<IActionResult> HandleTicket(Guid id)
        {
            await _mediator.Send(new HandleTicketCommand { TicketId = id });
            return Ok();
        }


    }
}
