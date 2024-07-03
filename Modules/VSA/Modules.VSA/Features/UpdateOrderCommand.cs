using Mapster;
using MediatR;
using Modules.VSA.Domain;
using Modules.VSA.DTOs;
using Modules.VSA.Persistence;

namespace Modules.VSA.Features
{
    public class UpdateOrderCommand
    {
        // 요청
        public class Request : OrderDTO, IRequest<Response> { }

        // 응답
        public class Response : OrderDTO { }

        // 핸들러
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var order = request.Adapt<Order>();
                var updatedOrder = _appDbContext.Update(order).Entity;

                await _appDbContext.SaveChangesAsync();
                return updatedOrder.Adapt<Response>();
            }
        }
    }
}
