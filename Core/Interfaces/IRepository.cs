namespace Core.Interfaces
{
    using System.Threading.Tasks;

    public interface IRepository
    {
        Task Add<T>(T plant);
    }
}
