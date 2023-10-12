// ----------------------------------------------------------------------------
// File: Staff.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the Staff class, which represents the structure
// of a staff member in the system. It contains properties for unique identifiers (StaffId, NIC),
// personal information (Name, Email, MobileNumber), user credentials (UserName, HashedPassword),
// and admin privileges (IsAdmin). The class also provides methods to hash and verify passwords.
// ----------------------------------------------------------------------------

using MongoDB.Bson;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MongoDotnetDemo.Models
{
    public class Staff
    {
        public ObjectId Id { get; set; }
        public required string StaffId { get; set; }
        public required string NIC { get; set; }
        public required string Name { get; set; } 
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string UserName { get; set; }
        public required string HashedPassword { get; set; }
        public required bool IsAdmin { get; set; }

        // Hash the password and set the PasswordHash property
        public void SetPassword(string password)
        {
            HashedPassword = BCryptNet.HashPassword(password);
        }

        // Verify a password against the stored hash
        public bool VerifyPassword(string password)
        {
            return BCryptNet.Verify(password, HashedPassword);
        }
    }
}
