namespace Core.Commands
{
    using Core.Interfaces;
    using Core.Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddEvent : IRequest<IoTEvent>
    {
        public IoTEvent IoTEvent;
        public IRepository Repository;
    }

    public class AddEventHandler : IRequestHandler<AddEvent, IoTEvent>
    {
        public async Task<IoTEvent> Handle(AddEvent request, CancellationToken cancellationToken)
        {
            await request.Repository.Add(request.IoTEvent);

            return request.IoTEvent;
        }
    }
}
