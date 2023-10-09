using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly IMongoCollection<Traveler> _travelerCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;
        private readonly ILogger<TravelerService> _logger;



        public TravelerService(IOptions<DatabaseSettings> dbSettings, ILogger<TravelerService> logger)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _travelerCollection = mongoDatabase.GetCollection<Traveler>(dbSettings.Value.TravelersCollectionName);
            _logger = logger; // Initialize the logger
        }



        public async Task<IEnumerable<Traveler>> GetAllAsyc() =>
            await _travelerCollection.Find(_ => true).ToListAsync();

        public async Task<Traveler> GetById(string id) =>
            await _travelerCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Traveler Traveler) =>

            await _travelerCollection.InsertOneAsync(Traveler);

        public async Task UpdateAsync(string id, Traveler Traveler) =>
            await _travelerCollection
            .ReplaceOneAsync(a => a.Id == id, Traveler);

        public async Task DeleteAysnc(string id) =>
            await _travelerCollection.DeleteOneAsync(a => a.Id == id);



        public async Task<Traveler> GetByNIC(string nic)
        {

            var filter = Builders<Traveler>.Filter.Eq(x => x.NIC, nic);
            var traveler = await _travelerCollection.Find(filter).FirstOrDefaultAsync();


            return traveler;
        }

        public async Task<Traveler> GetByNICS(string nic)
        {
            var filter = Builders<Traveler>.Filter.Eq(x => x.NIC, nic);
            var traveler = await _travelerCollection.Find(filter).FirstOrDefaultAsync();
            return traveler;
        }
        public async Task DeleteByNIC(string nic)
        {
            var filter = Builders<Traveler>.Filter.Eq(x => x.NIC, nic);
            await _travelerCollection.DeleteOneAsync(filter);
        }



    }
}
