using MediatR;
using Modules.VSA.Domain;
using Modules.VSA.DTOs;
using Modules.VSA.Persistence;

namespace Modules.VSA.Features
{
    public class CreateOrderCommand
    {
        // 요청
        public class Request : OrderDTO, IRequest<int> { }

        // 핸들러
        public class Handler : IRequestHandler<Request, int>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<int> Handle(Request request, CancellationToken cancellationToken)
            {
                //var order = request.Adapt<Order>();
                //var orderItems = request.OrderItems.Adapt<List<OrderItem>>();
                //order.OrderItems = orderItems;
                Order order = new();
                order.Id = Ulid.NewUlid();
                order.Name = "test";
                order.OrderItems.Add(new OrderItem
                {
                    Id = Ulid.NewUlid(),
                    Name = "testItem",
                    Quantity = 1,
                });
                await _appDbContext.AddAsync(order);
                int result = await _appDbContext.SaveChangesAsync();
                return result;
            }
        }
    }
}
