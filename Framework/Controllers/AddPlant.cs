using IoTVault.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTVault.Controllers
{
    public class AddPlantController
    {
        private string _plantMessage;

        public AddPlantController(string plantMessage)
        {
            _plantMessage = plantMessage;
        }

        public void Execute()
        {
            var plant = TryDeserializePlant();
        }

        private Plant TryDeserializePlant()
        {
            return JsonConvert.DeserializeObject<Plant>(_plantMessage, new JsonSerializerSettings());
        }
    }
}
