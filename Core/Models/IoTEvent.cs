namespace Core.Models
{
    using System;

    public class IoTEvent
    {
        public int MoistureVoltage { get; set; }

        public string DeviceId { get; set; }

        public string Event { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
