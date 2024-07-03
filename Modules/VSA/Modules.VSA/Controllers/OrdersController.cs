using BuildingBlocks.Infra.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.VSA.Features;

namespace Modules.VSA.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<OrderQuery.Response> Get()
        {
            OrderQuery.Request request = new();
            return await _mediator.Send(request);
        }

        [HttpGet("{Id:ulid}")]
        public async Task<OrderGetByIdQuery.Response> GetById([FromRoute] Ulid Id)
        {
            OrderGetByIdQuery.Request request = new() { Id = Id };
            return await _mediator.Send(request);
        }

        [HttpPost]
        [TransactionFilter]
        public async Task<int> Create(CreateOrderCommand.Request request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("{id:ulid}")]
        public async Task<UpdateOrderCommand.Response> Put([FromRoute] Ulid id, [FromBody] UpdateOrderCommand.Request request)
        {
            request.Id = id.ToString();
            return await _mediator.Send(request);
        }

        [HttpPatch("{id:ulid}")]
        [TransactionFilter]
        public async Task<UpdateOrderItemQuantityCommand.Response> PatchOrderItemQuantity([FromRoute] Ulid id, [FromBody] UpdateOrderItemQuantityCommand.Request request)
        {
            request.Id = id;
            return await _mediator.Send(request);
        }
    }
}
