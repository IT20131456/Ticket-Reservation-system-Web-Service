// ----------------------------------------------------------------------------
// File: StaffService.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the StaffService class, which implements the IStaffService
// interface. It provides methods for interacting with staff-related data in the MongoDB database.
// The class handles tasks such as retrieving staff members, creating, updating, and deleting staff records.
// ----------------------------------------------------------------------------

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

        // Retrieve all staff members asynchronously.
        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _staffCollection.Find(_ => true).ToListAsync();

        // Retrieve a staff member by their staff ID asynchronously.
        public async Task<Staff> GetByStaffIdAsync(string staffId) =>
            await _staffCollection.Find(a => a.StaffId == staffId).FirstOrDefaultAsync();

        // Retrieve a staff member by their username asynchronously.
        public async Task<Staff> GetByUsernameAsync(string username)
        {
            return await _staffCollection.Find(a => a.UserName == username).FirstOrDefaultAsync();
        }

        // Create a new staff member asynchronously.
        public async Task CreateAsync(Staff staff) {
            await _staffCollection.InsertOneAsync(staff);
        }

        // Update a staff member by their ID asynchronously.
        public async Task UpdateAsync(string id, Staff staff) =>
            await _staffCollection
            .ReplaceOneAsync(a => a.StaffId == id, staff);

        // Delete a staff member by their ID asynchronously.
        public async Task DeleteAsync(string id) =>
            await _staffCollection.DeleteOneAsync(a => a.StaffId == id);

        // Update a staff member by their ID using a custom update definition asynchronously.
        public async Task UpdateAsync(string id, UpdateDefinition<Staff> updateDefinition) =>
            await _staffCollection
                .UpdateOneAsync(a => a.StaffId == id, updateDefinition);
    }
}
