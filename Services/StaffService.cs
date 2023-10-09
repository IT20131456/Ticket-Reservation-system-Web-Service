using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class StaffService : IStaffService
    {
        private readonly IMongoCollection<Staff> _staffCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public StaffService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _staffCollection = mongoDatabase.GetCollection<Staff>
                (dbSettings.Value.StaffCollectionName);
        }

        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _staffCollection.Find(_ => true).ToListAsync();

        public async Task<Staff> GetByStaffIdAsync(string staffId) =>
            await _staffCollection.Find(a => a.StaffId == staffId).FirstOrDefaultAsync();

        public async Task CreateAsync(Staff staff) {
            await _staffCollection.InsertOneAsync(staff);
        }

        public async Task UpdateAsync(string id, Staff staff) =>
            await _staffCollection
            .ReplaceOneAsync(a => a.StaffId == id, staff);

        public async Task DeleteAsync(string id) =>
            await _staffCollection.DeleteOneAsync(a => a.StaffId == id);

    }
}
