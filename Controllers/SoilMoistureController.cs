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

        public SoilMoistureController(string message, ILogger log)
        {
            _message = message;
            _log = log;
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
                PublishedAt = Mapper.Map<DateTime>(iotDeviceEvent.PublishedAt)
            };
        }
    }
}
