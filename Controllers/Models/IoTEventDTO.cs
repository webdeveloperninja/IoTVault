using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers.Models
{
    public class IoTEventDTO
    {
        public string Data { get; set; }

        public string DeviceId { get; set; }

        public string Event { get; set; }

        public string PublishedAt { get; set; }
    }
}
