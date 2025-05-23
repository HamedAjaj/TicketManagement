using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Commands.Model;
using TicketManagement.Queries.Model;

namespace TicketManagement.Controllers
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
            var id = await _mediator.Send(command);
            return Ok(id);
        }


        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var tickets = await _mediator.Send(new GetTicketsQuery { Page = page, PageSize = pageSize });
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
