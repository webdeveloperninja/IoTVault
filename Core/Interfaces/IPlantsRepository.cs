namespace Core.Interfaces
{
    using Core.Models;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IPlantsRepository
    {
        Task Add(Plant plant);

        Plant SelectByDeviceId(string deviceId);

        void DeletePlantByDeviceId(string deviceId);
    }
}
