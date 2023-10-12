// ----------------------------------------------------------------------------
// File: IStaffService.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the IStaffService interface, which specifies the contract
// for interacting with staff-related data and operations. Implementations of this interface
// provide methods for retrieving staff members, creating, updating, and deleting staff records.
// ----------------------------------------------------------------------------

using MongoDB.Driver;
using MongoDotnetDemo.Models;

namespace MongoDotnetDemo.Services
{
    public interface IStaffService
    {
        // Retrieve all staff members asynchronously.
        Task<IEnumerable<Staff>> GetAllAsync();

        // Retrieve a staff member by their staffId asynchronously.
        Task<Staff> GetByStaffIdAsync(string staffId);

        // Retrieve a staff member by their username asynchronously.
        Task<Staff> GetByUsernameAsync(string username); 

        // Create a new staff member asynchronously.
        Task CreateAsync(Staff staff);

        // Update a staff member by their staffId asynchronously.
        Task UpdateAsync(string id, Staff staff);

        // Update a staff member by their staffId using an update definition asynchronously.
        Task UpdateAsync(string id, UpdateDefinition<Staff> updateDefinition);

        // Delete a staff member by their staffId asynchronously.
        Task DeleteAsync(string id);
    }
}
