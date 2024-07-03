using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.VSA.Features;
using Modules.VSA.Notifications;

namespace Modules.VSA.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("returns")]
        public async Task<EchoCommand.Response> echo(EchoCommand.Request request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("notifications")]
        public async Task Notify(SampleNofitication message)
        {
            await _mediator.Publish(message);
        }
    }
}
