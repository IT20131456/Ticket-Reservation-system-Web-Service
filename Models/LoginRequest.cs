// ----------------------------------------------------------------------------
// File: LoginRequest.cs
// Author: IT20125202
// Created: 2023-10-09
// Description: This file defines the LoginRequest class, which represents the structure
// of a request object used for user login. It contains properties for user identification (Id)
// and password (Password).
// ----------------------------------------------------------------------------

namespace MongoDotnetDemo.Models {
    public class LoginRequest
    {
        public required string Id { get; set; }
        public required string Password { get; set; }
    }
}