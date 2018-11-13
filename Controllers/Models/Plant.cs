using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers.Models
{
    public class Plant
    {
        public string _id { get; set; }

        public string PlantName { get; set; }

        public string PlantNumber { get; set; }

        public string PlantDescription { get; set; }

        public string PlantStatus { get; set; }

        public string RoomType { get; set; }

        public string Medium { get; set; }

        public string DeviceId { get; set; }

        public string UserId { get; set; }
    }
}
