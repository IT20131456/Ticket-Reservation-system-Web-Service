namespace MongoDotnetDemo.Models {
    public class LoginRequest
    {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }
}