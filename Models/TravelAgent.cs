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
