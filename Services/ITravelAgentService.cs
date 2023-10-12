// ----------------------------------------------------------------------------
// File: ITravelAgentService.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the ITravelAgentService interface, which specifies the contract
// for interacting with travel agent-related data and operations. Implementations of this interface
// provide methods for retrieving travel agents, creating, updating, and deleting agent records.
// ----------------------------------------------------------------------------

using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface ITravelAgentService
    {
        // Retrieve all travel agents asynchronously.
        Task<IEnumerable<TravelAgent>> GetAllAsync();

        // Retrieve a travel agent by their Regiatration No asynchronously.
        Task<TravelAgent> GetByAgentIdAsync(string regNo);

        // Retrieve a travel agent by their username asynchronously.
        Task<TravelAgent> GetByUsernameAsync(string username); 

        // Create a new travel agent asynchronously.
        Task CreateAsync(TravelAgent travelAgent);

        // Update a travel agent by their Regiatration No asynchronously.
        Task UpdateAsync(string id, TravelAgent travelAgent);

        // Delete a travel agent by their Regiatration No asynchronously.
        Task DeleteAsync(string id);
    }
}
