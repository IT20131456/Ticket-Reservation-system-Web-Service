/*
 * Filename: ITrainScheduleService.cs
 * Author: IT20127046 
 * Modified By: IT20127046
 * Description: Interface defining the contract for TrainSchedule operations in the TrainSchedule API.
 *              Contains methods for retrieving, creating, updating, and deleting TrainSchedule records.
 *              Additional methods for retrieving TrainSchedule by reference id.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITrainScheduleService
    {
        // Define the method signatures for the CRUD operations.
        Task<IEnumerable<TrainSchedule>> GetAllAsync();
        Task<TrainSchedule> GetByIdAsync(string id);

        Task CreateAsync(TrainSchedule trainSchedule);

        Task UpdateAsync(string id, TrainSchedule trainSchedule);

        Task DeleteAsync(string id);

        Task<TrainSchedule> GetByTrainNoAsync(string id);
    }
}
