using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDotnetDemo.Models
{
    public class Traveler
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NIC { get; set; }
        public string? FullName { get; set; }
        public string? DOB { get; set; }

        public string? Gender { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; } 
        public string? PasswordHash { get; set; }


        public string? Profile { get; set; }
        public string? TravelerType { get; set; }
        public string? AccountStatus { get; set; }

        public string? CreatedAt { get; set; }


        // Hash and set the password when saving a new traveler
    public void SetPassword(string password)
    {
        // Generate a salt and hash the password
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    

    

    // Verify a password during login
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }


    }
}





