namespace Core.Interfaces
{
    using Core.Models;
    using System.Threading.Tasks;

    public interface IEventsRepository
    {
        Task Add(PlantEvent deviceEvent);
    }
}
