using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.VSA.DTOs;
using Modules.VSA.Persistence;

namespace Modules.VSA.Features
{
    public class UpdateOrderItemQuantityCommand
    {
        public class Request : IRequest<Response>
        {
            public Ulid Id { get; set; }
            public Ulid OrderItemId { get; set; }
            public int Quantity { get; set; }
        }

        public class Response : OrderDTO
        {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var order = await _appDbContext.orders.Include(o => o.OrderItems).FirstAsync(o => o.Id == request.Id);
                order.UpdateOrderItemQuantity(request.OrderItemId, request.Quantity);

                _appDbContext.Update(order);
                await _appDbContext.SaveChangesAsync();

                var result = order.Adapt<Response>();
                return result;
            }
        }
    }
}