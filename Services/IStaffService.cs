using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> GetByStaffIdAsync(string staffId);
        Task CreateAsync(Staff staff);
        Task UpdateAsync(string id, Staff staff);
        Task DeleteAsync(string id);
    }
}