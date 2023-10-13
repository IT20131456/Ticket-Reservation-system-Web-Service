/*
 * Filename: TrainScheduleService.cs
 * Author: IT20127046 
 * Modified By: IT20127046
 * Description: Service class responsible for managing TrainSchedule in a MongoDB database.
 *              Implements ITrainScheduleService interface and provides methods for retrieving, creating, updating, and deleting TrainSchedule records.
 */

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotnetDemo.Services
{
    public class TrainScheduleService : ITrainScheduleService
    {
        private readonly IMongoCollection<TrainSchedule> _trainScheduleCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        // Constructor to initialize MongoDB collection and database settings.
        public TrainScheduleService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _trainScheduleCollection = mongoDatabase.GetCollection<TrainSchedule>
                (dbSettings.Value.TrainSchedulesCollectionName);
        }

        // Retrieves all TrainSchedule asynchronously.
        public async Task<IEnumerable<TrainSchedule>> GetAllAsync() =>
            await _trainScheduleCollection.Find(_ => true).ToListAsync();

        // Retrieves a specific TrainSchedule by its unique identifier asynchronously.
        public async Task<TrainSchedule> GetByIdAsync(string id) =>
            await _trainScheduleCollection.Find(schedule => schedule.Id == id).FirstOrDefaultAsync();

        // Creates a new TrainSchedule asynchronously.
        public async Task CreateAsync(TrainSchedule trainSchedule) =>
            await _trainScheduleCollection.InsertOneAsync(trainSchedule);

        // Updates an existing TrainSchedule asynchronously.
        public async Task UpdateAsync(string id, TrainSchedule trainSchedule) =>
            await _trainScheduleCollection.ReplaceOneAsync(schedule => schedule.Id == id, trainSchedule);

        // Deletes a TrainSchedule by its unique identifier asynchronously.
        public async Task DeleteAsync(string id) =>
            await _trainScheduleCollection.DeleteOneAsync(schedule => schedule.Id == id);

        // Retrieves specific TrainSchedule by train number.
        public async Task<TrainSchedule> GetByTrainNoAsync(string id) =>
            await _trainScheduleCollection.Find(a => a.train_number == id).FirstOrDefaultAsync();

    }
}
