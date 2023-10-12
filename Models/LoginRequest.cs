// ----------------------------------------------------------------------------
// File: LoginRequest.cs
// Author: IT20125202
// Description: Class to represent the structure of a request object used for user login. 
// ----------------------------------------------------------------------------

namespace MongoDotnetDemo.Models {
    public class LoginRequest
    {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }
}