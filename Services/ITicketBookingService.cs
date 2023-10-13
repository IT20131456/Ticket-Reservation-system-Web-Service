/*
 * Filename: ITicketBookingService.cs
 * Author: IT20131456 
 * Modified By: IT20125202, IT20128036
 * Description: Interface defining the contract for ticket booking operations in the Ticket Booking API.
 *              Contains methods for retrieving, creating, updating, and deleting ticket booking records.
 *              Additional methods for retrieving bookings by reference ID and booking history are defined.
 */


using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITicketBookingService
    {
        // Retrieves all ticket bookings asynchronously.
        Task<IEnumerable<TicketBooking>> GetAllAsync();

        // Retrieves a specific ticket booking by its unique identifier asynchronously.
        Task<TicketBooking> GetById(string id);

        // Creates a new ticket booking asynchronously.
        Task CreateAsync(TicketBooking ticketBooking);

        // Updates an existing ticket booking asynchronously.
        Task UpdateAsync(string id, TicketBooking newTicketBooking);

        // Deletes a ticket booking by its unique identifier asynchronously.
        Task DeleteAsync(string id);


        Task<IEnumerable<TicketBooking>> GetByReferenceIdAsync(string referenceId);

        // Retrieves a specific ticket booking by its unique identifier asynchronously.
        Task<IEnumerable<TicketBooking>> GetAllByRefId(string nic);

    }
}
