using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> GetByStaffIdAsync(string staffId);
        Task CreateAsync(Staff staff);
        Task UpdateAsync(string id, Staff staff);
        Task UpdateAsync(string id, UpdateDefinition<Staff> updateDefinition);
        Task DeleteAsync(string id);
    }
}