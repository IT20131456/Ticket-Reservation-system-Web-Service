namespace MongoDotnetDemo.Models {
    public class LoginRequest
    {
        public required string StaffId { get; set; }
        public required string Password { get; set; }
    }
}