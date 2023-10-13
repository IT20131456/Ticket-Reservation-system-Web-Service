using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITravelerService
    {
        Task<IEnumerable<Traveler>> GetAllAsyc();
        Task<Traveler> GetById(string id);
        Task<Traveler> GetByTravelerIdAsync(string NIC);
        Task<Traveler> GetByNIC(string nic); 
        Task<Traveler> GetByNICS(string username);
        Task CreateAsync(Traveler traveler);
        Task UpdateAsync(string id, Traveler traveler);
        Task DeleteAysnc(string id);
        Task DeleteByNIC(string nic);
        Task UpdateByNic(string nic, Traveler traveler);
        Task UpdateAsyncN(Traveler traveler);
    }
}