using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.VSA.DTOs;
using Modules.VSA.Persistence;

namespace Modules.VSA.Features
{
    public class OrderQuery
    {
        // 요청
        public class Request : IRequest<Response> { }

        // 응답
        public class Response : List<OrderDTO> { }

        //핸들러
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var orders = await _appDbContext.orders.Include(o => o.OrderItems).ToListAsync();
                var result = orders.Adapt<Response>();
                return result;
            }
        }
    }
}
