namespace Controllers
{
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Resolvers;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;

    public class SoilMoistureController
    {
        private readonly string _message;
        private readonly ILogger _log;
        private readonly IMapper _mapper;

        public SoilMoistureController(string message, ILogger log, IMapper mapper)
        {
            _message = message;
            _log = log;
            _mapper = mapper;
        }

        public void Execute()
        {
            var ioTEvent = GetEvent();
        }

        private IoTEvent GetEvent()
        {
            var iotDeviceEvent = JsonConvert.DeserializeObject<IoTEventDTO>(_message, new JsonSerializerSettings()
            {
                ContractResolver = new UnderscorePropertyNames()
            });

            int voltage;
            Int32.TryParse(iotDeviceEvent.Data, out voltage);

            return new IoTEvent
            {
                MoistureVoltage = voltage,
                DeviceId = iotDeviceEvent.DeviceId,
                Event = iotDeviceEvent.Event,
                PublishedAt = _mapper.Map<DateTime>(iotDeviceEvent.PublishedAt)
            };
        }
    }
}
