// ----------------------------------------------------------------------------
// File: StaffController.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file contains the implementation of the StaffController class,
// which handles staff-related operations such as listing staff members, adding new staff members,
// updating staff information, deleting staff members, and handling staff member login.
// Version: 1.0.0
// Route: /api/Staff
// ----------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using MongoDB.Driver;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        // GET: api/Staff
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Retrieve all staff members from the database
            var allStaff = await _staffService.GetAllAsync();
            return Ok(allStaff);
        }

        // GET api/Staff/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // Retrieve a staff member by ID from the database
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
            {
                return NotFound(); // Return a not found response if the staff member is not found
            }
            return Ok(staff);
        }

        // POST api/Staff
        [HttpPost]
        public async Task<IActionResult> Post(Staff staff)
        {
            // Check if a user with the same StaffId already exists
            var existingStaff = await _staffService.GetByStaffIdAsync(staff.StaffId);
            if (existingStaff != null)
            {
                // A user with the same StaffId already exists; return a conflict response
                return Conflict("Staff ID already exists.");
            }

            // Check if a user with the same username already exists
            var existingStaffByUsername = await _staffService.GetByUsernameAsync(staff.UserName);
            if (existingStaffByUsername != null)
            {
                // A user with the same username already exists; return a conflict response
                return Conflict("Username already exists.");
            }

            // Hash the password
            var password = staff.HashedPassword;
            staff.SetPassword(password);

            // Create the new staff member in the database
            await _staffService.CreateAsync(staff);

            // Return a success response
            return Ok("created successfully");
        }

        // PUT api/Staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Staff newStaff)
        {
            // Retrieve the existing staff member by ID
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
                return NotFound(); // Return not found if the staff member is not found

            // Update only the specific fields of the staff object
            var updatedFields = new List<UpdateDefinition<Staff>>();

            if (!string.IsNullOrEmpty(newStaff.Name))
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.Name, newStaff.Name));
            }

            if (!string.IsNullOrEmpty(newStaff.Email))
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.Email, newStaff.Email));
            }

            if(!string.IsNullOrEmpty(newStaff.StaffId))
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.StaffId, newStaff.StaffId));
            }

            if(!string.IsNullOrEmpty(newStaff.UserName))
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.UserName, newStaff.UserName));
            }

            if(!string.IsNullOrEmpty(newStaff.MobileNumber))
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.MobileNumber, newStaff.MobileNumber));
            }
            
            if (newStaff.IsAdmin != staff.IsAdmin)
            {
                updatedFields.Add(Builders<Staff>.Update.Set(x => x.IsAdmin, newStaff.IsAdmin));
            }

            if (updatedFields.Count > 0)
            {
                var updateDefinition = Builders<Staff>.Update.Combine(updatedFields);
                await _staffService.UpdateAsync(id, updateDefinition);
                
                // Retrieve the updated staff member
                var updatedStaff = await _staffService.GetByStaffIdAsync(id);
                return Ok(new { Message = "Updated successfully", Data = updatedStaff });
            }
            else
            {
                return Ok(new { Message = "No fields to update", Data = new {} });
            }
        }

        // DELETE api/Staff/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Retrieve the staff member by ID
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
                return NotFound(); // Return not found if the staff member is not found
            
            // Delete the staff member from the database
            await _staffService.DeleteAsync(id);

            return Ok("deleted successfully");
        }

        // POST api/Staff/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Access loginRequest.StaffId and loginRequest.Password
            string staffId = loginRequest.Id;
            string password = loginRequest.Password;

            // Retrieve the staff member by staffId from MongoDB
            var staffMember = await _staffService.GetByStaffIdAsync(staffId);

            // Check if a staff member with the given staffId was found
            if (staffMember != null)
            {
                // Verify the entered password against the stored hashed password
                if (staffMember.VerifyPassword(password))
                {
                    // Password is correct; proceed with login
//                    // Generate a JWT token
//                    byte[] userSpecificDataBytes = Encoding.UTF8.GetBytes(staffId + ":" +staffMember.NIC);
//                    // Use SHA-256 to hash the user-specific data
//                    using (SHA256 sha256 = SHA256.Create())
//                    {
//                        byte[] hashedData = sha256.ComputeHash(userSpecificDataBytes);
//
//                        // Use the hashed data as the key (it will be 256 bits long)
//                        var key = new SymmetricSecurityKey(hashedData);
//
//                        var tokenOptions = new JwtSecurityToken(
//                            issuer: "http://localhost:5041",
//                            audience: "http://localhost:3000",
//                            claims: new[] { new Claim(ClaimTypes.Name, staffMember.StaffId) }, 
//                            expires: DateTime.UtcNow.AddMinutes(15), // Token expiration time
//                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
//                        );
//
//                        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
//                        return Ok(new { Message = "Login successful", Token = token });
//
//                    }

                    // Return a success response when password is correct
                    return Ok(new { Message = "Login successful", Data = staffMember }); // return a token or user information
                }
            }

            // Either the staff member was not found or the password is incorrect
            // Return an unauthorized response
            return Unauthorized("Authentication failed");
        }
    }
}
