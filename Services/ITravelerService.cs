using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITravelerService
    {
        Task<IEnumerable<Traveler>> GetAllAsyc();
        Task<Traveler> GetById(string id);
        Task<Traveler> GetByTravelerIdAsync(string NIC);
        Task CreateAsync(Traveler traveler);
        Task UpdateAsync(string id, Traveler traveler);
        Task DeleteAysnc(string id);
    }
}