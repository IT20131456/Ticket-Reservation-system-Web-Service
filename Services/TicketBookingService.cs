/*
 * Filename: TicketBookingService.cs
 * Author: IT20131456 
 * Modified By: IT20125202, IT20128036
 * Description: Service class for managing ticket bookings in a MongoDB database.
 */

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public class TicketBookingService : ITicketBookingService
    {
        private readonly IMongoCollection<TicketBooking> _ticketBookingCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        // Constructor to initialize MongoDB collection and database settings.
        public TicketBookingService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _ticketBookingCollection = mongoDatabase.GetCollection<TicketBooking>(dbSettings.Value.TicketBookingCollectionName);
        }

        // Retrieves all ticket bookings asynchronously.
        public async Task<IEnumerable<TicketBooking>> GetAllAsync() =>
            await _ticketBookingCollection.Find(_ => true).ToListAsync();

        // Retrieves a specific ticket booking by its unique identifier asynchronously.
        public async Task<TicketBooking> GetById(string id) =>
            await _ticketBookingCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        // Creates a new ticket booking asynchronously.
        public async Task CreateAsync(TicketBooking ticketBooking) =>
            await _ticketBookingCollection.InsertOneAsync(ticketBooking);

        // Updates an existing ticket booking asynchronously.
        public async Task UpdateAsync(string id, TicketBooking ticketBooking) =>
            await _ticketBookingCollection.ReplaceOneAsync(a => a.Id == id, ticketBooking);

        // Deletes a ticket booking by its unique identifier asynchronously.
        public async Task DeleteAsync(string id) =>
            await _ticketBookingCollection.DeleteOneAsync(a => a.Id == id);


        public async Task<IEnumerable<TicketBooking>> GetByReferenceIdAsync(string referenceId) =>
        await _ticketBookingCollection.Find(tb => tb.reference_id == referenceId).ToListAsync();


        // Retrieves specific ticket bookings by reference ID.
        public async Task<IEnumerable<TicketBooking>> GetAllByRefId(string nic) =>
            await _ticketBookingCollection.Find(a => a.reference_id == nic).ToListAsync();

    }
}
