using MediatR;
using SharedKernel;

namespace Modules.VSA.Notifications
{
    public class SampleNofitication : DomainEvent, INotification
    {
        public DateTime OccurredOn => DateTime.Now;

        public string Message { get; set; }
    }

    public class SampleNotificationHandler : INotificationHandler<SampleNofitication>
    {
        public async Task Handle(SampleNofitication notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Consumed message: {notification.OccurredOn}");
        }
    }
}
