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
            var allStaff = await _staffService.GetAllAsync();
            return Ok(allStaff);
        }

        // GET api/Staff/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // POST api/Staff
        [HttpPost]
        public async Task<IActionResult> Post(Staff staff)
        {
            // TODO: Add the validations to username
            // Check if a user with the same StaffId already exists
            var existingStaff = await _staffService.GetByStaffIdAsync(staff.StaffId);
            if (existingStaff != null)
            {
                // A user with the same StaffId already exists; return a conflict response
                return Conflict("Staff ID already exists.");
            }

            // Hash the password
            var password = staff.HashedPassword;
            staff.SetPassword(password);

            // Create the new staff member
            await _staffService.CreateAsync(staff);

            // Return a success response
            return Ok("created successfully");
        }

        // PUT api/Staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Staff newStaff)
        {
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
                return NotFound();
            await _staffService.UpdateAsync(id, newStaff);
            return Ok("updated successfully");
        }

        // DELETE api/Staff/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var staff = await _staffService.GetByStaffIdAsync(id);
            if (staff == null)
                return NotFound();
            await _staffService.DeleteAsync(id);
            return Ok("deleted successfully");
        }

        // POST api/Staff/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // access loginRequest.StaffId and loginRequest.Password
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
                    // Return a success response
                    return Ok(new { Message = "Login successful", Data = staffMember }); // TODO: return a token or user information here
                }
            }

            // Either the staff member was not found or the password is incorrect
            // Return an unauthorized response
            return Unauthorized("Authentication failed");
        }
    }
}
