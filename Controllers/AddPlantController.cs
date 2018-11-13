using Controllers.Models;
using System;
using Newtonsoft.Json;

namespace Controllers
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
