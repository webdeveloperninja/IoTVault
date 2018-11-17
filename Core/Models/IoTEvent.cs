using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class IoTEvent
    {
        public int MoistureVoltage { get; set; }

        public string DeviceId { get; set; }

        public string Event { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
