/*
 * Filename: ITravelerService.cs
 * Author: IT20128036
 * Description: Interface defining the contract for traveler operations in the Traveler API.
    *              Contains methods for retrieving, creating, updating, and deleting traveler records.
    *              Additional methods for retrieving, deleting, and updating travelers by NIC are defined.
 *             
 *              
 */

using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITravelerService
    {
        // Retrieves all travelers asynchronously.
        Task<IEnumerable<Traveler>> GetAllAsyc();
        // Retrieves a specific traveler by its unique identifier asynchronously.
        Task<Traveler> GetById(string id);
        // Retrieves a specific traveler by its unique identifier asynchronously.
        Task<Traveler> GetByTravelerIdAsync(string NIC);
        // Retrieves a specific traveler by its unique identifier asynchronously.
        Task<Traveler> GetByNIC(string nic); 
        // Retrieves a specific traveler by its unique identifier asynchronously.
        Task<Traveler> GetByNICS(string username);
        // Creates a new traveler asynchronously.
        Task CreateAsync(Traveler traveler);
        // Updates an existing traveler asynchronously.
        Task UpdateAsync(string id, Traveler traveler);
        // Deletes a traveler by its unique identifier asynchronously.
        Task DeleteAysnc(string id);
        //Deletes a traveler by its unique identifier asynchronously.
        Task DeleteByNIC(string nic);
        //Updates a traveler by its unique identifier asynchronously.
        Task UpdateByNic(string nic, Traveler traveler);
        //Updates a traveler by its unique identifier asynchronously.
        Task UpdateAsyncN(Traveler traveler);
        //Updates a traveler by its unique identifier asynchronously.
        Task UpdateByNicRequirdOnly(string nic, Traveler traveler);
    }
}