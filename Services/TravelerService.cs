using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly IMongoCollection<Traveler> _travelerCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public TravelerService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient= new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase= mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _travelerCollection = mongoDatabase.GetCollection<Traveler>
                (dbSettings.Value.TravelersCollectionName);
        }

        //public async Task<IEnumerable<Category>> GetAllAsyc()
        //{
        //    var categories = await _categoryCollection.Find(_ => true).ToListAsync();
        //    return categories;
        //}

        public async Task<IEnumerable<Traveler>> GetAllAsyc()=>
            await _travelerCollection.Find(_ => true).ToListAsync();

        public async Task<Traveler> GetById(string id) =>
            await _travelerCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<Traveler> GetByTravelerIdAsync(string NIC) =>
            await _travelerCollection.Find(a => a.NIC == NIC).FirstOrDefaultAsync();

        public async Task CreateAsync(Traveler Traveler) =>
            
            await _travelerCollection.InsertOneAsync(Traveler);

        public async Task UpdateAsync(string id,Traveler Traveler) =>
            await _travelerCollection
            .ReplaceOneAsync(a => a.Id == id, Traveler);

        public async Task DeleteAysnc(string id) =>
            await _travelerCollection.DeleteOneAsync(a => a.Id == id);
    }
}
