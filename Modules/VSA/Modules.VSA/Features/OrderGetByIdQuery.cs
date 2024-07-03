using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.VSA.DTOs;
using Modules.VSA.Persistence;

namespace Modules.VSA.Features
{
    public class OrderGetByIdQuery
    {
        // 요청
        public class Request : IRequest<Response>
        {
            public Ulid Id { get; set; }
        }

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
                var response = await _appDbContext.orders.Include(e=>e.OrderItems).FirstAsync(o=>o.Id == request.Id);
                return response.Adapt<Response>();
            }
        }
    }
}
