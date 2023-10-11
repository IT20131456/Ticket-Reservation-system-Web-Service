/*
 * Filename: ITicketBookingService.cs
 * Description: Interface defining contract for ticket booking operations.
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
    }
}
