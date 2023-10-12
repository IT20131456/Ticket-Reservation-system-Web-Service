// ----------------------------------------------------------------------------
// File: TravelAgentService.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the TravelAgentService class, which implements the ITravelAgentService
// interface. It provides methods for interacting with travel agent-related data in the MongoDB database.
// The class handles tasks such as retrieving travel agents, creating, updating, and deleting agent records.
// ----------------------------------------------------------------------------

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

        // Retrieve all travel agents asynchronously.
        public async Task<IEnumerable<TravelAgent>> GetAllAsync() =>
            await _travelAgentCollection.Find(_ => true).ToListAsync();

        // Retrieve a travel agent by their registration number asynchronously.
        public async Task<TravelAgent> GetByAgentIdAsync(string regNo) =>
            await _travelAgentCollection.Find(a => a.RegNo == regNo).FirstOrDefaultAsync();

        // Retrieve a travel agent by their username asynchronously.
        public async Task<Staff> GetByUsernameAsync(string username)
        {
            return await _travelAgentCollection.Find(a => a.UserName == username).FirstOrDefaultAsync();
        }

        // Create a new travel agent asynchronously.
        public async Task CreateAsync(TravelAgent travelAgent) {
            await _travelAgentCollection.InsertOneAsync(travelAgent);
        }

        // Update a travel agent by their registration number asynchronously.
        public async Task UpdateAsync(string id, TravelAgent travelAgent) =>
            await _travelAgentCollection
            .ReplaceOneAsync(a => a.RegNo == id, travelAgent);

        // Delete a travel agent by their registration number asynchronously.
        public async Task DeleteAsync(string id) =>
            await _travelAgentCollection.DeleteOneAsync(a => a.RegNo == id);
    }
}
