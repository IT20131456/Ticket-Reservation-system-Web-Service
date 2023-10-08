using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITrainScheduleService
    {
        Task<IEnumerable<TrainSchedule>> GetAllAsync();
        Task<TrainSchedule> GetByIdAsync(string id);
        Task CreateAsync(TrainSchedule trainSchedule);
        Task UpdateAsync(string id, TrainSchedule trainSchedule);
        Task DeleteAsync(string id);
    }
}
