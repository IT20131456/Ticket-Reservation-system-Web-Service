using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class TravelAgentService : ITravelAgentService
    {
        private readonly IMongoCollection<TravelAgent> _travelAgentCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public TravelAgentService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _travelAgentCollection = mongoDatabase.GetCollection<TravelAgent>
                (dbSettings.Value.TravelAgentCollectionName);
        }

        public async Task<IEnumerable<TravelAgent>> GetAllAsync() =>
            await _travelAgentCollection.Find(_ => true).ToListAsync();

        public async Task<TravelAgent> GetByAgentIdAsync(string regNo) =>
            await _travelAgentCollection.Find(a => a.RegNo == regNo).FirstOrDefaultAsync();

        public async Task CreateAsync(TravelAgent travelAgent) {
            await _travelAgentCollection.InsertOneAsync(travelAgent);
        }

        public async Task UpdateAsync(string id, TravelAgent travelAgent) =>
            await _travelAgentCollection
            .ReplaceOneAsync(a => a.RegNo == id, travelAgent);

        public async Task DeleteAsync(string id) =>
            await _travelAgentCollection.DeleteOneAsync(a => a.RegNo == id);

    }
}
