using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITravelAgentService
    {
        Task<IEnumerable<TravelAgent>> GetAllAsync();
        Task<TravelAgent> GetByAgentIdAsync(string regNo);
        Task CreateAsync(TravelAgent travelAgent);
        Task UpdateAsync(string id, TravelAgent travelAgent);
        Task DeleteAsync(string id);
    }
}