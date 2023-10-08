using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllAsyc();
        Task<Staff> GetByStaffIdAsync(string staffId);
        Task CreateAsync(Staff Product);
        Task UpdateAsync(string id, Staff Product);
        Task DeleteAsync(string id);
    }
}