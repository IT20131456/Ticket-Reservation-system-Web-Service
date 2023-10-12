// ----------------------------------------------------------------------------
// File: TravelAgent.cs
// Author: IT20125202
// Description: Class to represent the structure of a travel agent in the system. It contains properties for unique identifiers (RegNo, NIC),
//              personal information (Name, Email, MobileNumber), and user credentials (UserName, HashedPassword).
//              Provides methods to hash and verify passwords.
// ----------------------------------------------------------------------------

using MongoDB.Bson;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MongoDotnetDemo.Models
{
    public class TravelAgent
    {
        public ObjectId Id { get; set; }
        public required string RegNo { get; set; }
        public required string NIC { get; set; }
        public required string Name { get; set; } 
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string UserName { get; set; }
        public required string HashedPassword { get; set; }

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
