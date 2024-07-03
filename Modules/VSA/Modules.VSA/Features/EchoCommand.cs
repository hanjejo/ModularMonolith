using MediatR;

namespace Modules.VSA.Features
{
    public class EchoCommand
    {
        // 요청
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
        }

        // 응답
        public class Response
        {
            public string Name { get; set; }
        }

        // 핸들러
        public class Handler : IRequestHandler<Request, Response>
        {
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response() { Name = request.Name };
            }
        }
    }
}
