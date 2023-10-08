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

        public TrainScheduleService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _trainScheduleCollection = mongoDatabase.GetCollection<TrainSchedule>
                (dbSettings.Value.TrainSchedulesCollectionName);
        }

        public async Task<IEnumerable<TrainSchedule>> GetAllAsync() =>
            await _trainScheduleCollection.Find(_ => true).ToListAsync();

        public async Task<TrainSchedule> GetByIdAsync(string id) =>
            await _trainScheduleCollection.Find(schedule => schedule.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TrainSchedule trainSchedule) =>
            await _trainScheduleCollection.InsertOneAsync(trainSchedule);

        public async Task UpdateAsync(string id, TrainSchedule trainSchedule) =>
            await _trainScheduleCollection.ReplaceOneAsync(schedule => schedule.Id == id, trainSchedule);

        public async Task DeleteAsync(string id) =>
            await _trainScheduleCollection.DeleteOneAsync(schedule => schedule.Id == id);
    }
}
