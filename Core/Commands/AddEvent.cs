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

        public IEventsRepository EventsRepository;

        public IPlantsRepository PlantsRepository;
    }

    public class AddEventHandler : IRequestHandler<AddEvent, IoTEvent>
    {
        public async Task<IoTEvent> Handle(AddEvent request, CancellationToken cancellationToken)
        {
            var plant = request.PlantsRepository.SelectByDeviceId(request.IoTEvent.DeviceId);

            if (plant != null)
            {
                var plantEvent = new PlantEvent
                {
                    Plant = plant,
                    IoTEvent = request.IoTEvent
                };

                await request.EventsRepository.Add(plantEvent);
            }

            return request.IoTEvent;
        }
    }
}
