/*
 * Filename: TravelerController.cs
 * Author: IT20128036
* Modified By: IT20127046
 * Description: Controller class for handling traveler operations in the Traveler API.
    *              Provides endpoints for retrieving, creating, updating, and deleting traveler records.
    *              Additional endpoints for retrieving, deleting, and updating travelers by NIC are also implemented.
 */

using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerController : ControllerBase
    {
        private readonly ITravelerService _travelerService;
        public TravelerController(ITravelerService travelerService)
        {
            _travelerService = travelerService;
        }
        // GET: api/traveler
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var travelers = await _travelerService.GetAllAsyc();
            return Ok(travelers);
        }

        // GET api/traveler/652231bdc0273fd1118a104f
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return Ok(traveler);
        }

        // GET api/traveler/556765456V
        [HttpGet("getbyNIC/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return Ok(traveler);
        }


        // POST api/traveler
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Traveler traveler)
        {
            if (traveler == null)
            {
                return BadRequest("Invalid traveler data"); // Return a bad request response for null traveler
            }

            // Check if a traveler with the same 'nic' already exists
            var existingTraveler = await _travelerService.GetByNIC(traveler.NIC);

            if (existingTraveler != null)
            {
                // A traveler with the same 'nic' already exists, return a conflict response
                return Conflict(new { ErrorMessage = "A traveler with the same NIC already exists." });
            }

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(traveler.PasswordHash);
            traveler.PasswordHash = hashedPassword;

            // Create the new traveler
            await _travelerService.CreateAsync(traveler);

            // Return a success response
            return Ok(new { StatusCode = "created successfully" });
        }

        // PUT api/traveler/652231bdc0273fd1118a104f
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Traveler newTraveler)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
                return NotFound();
            await _travelerService.UpdateAsync(id, newTraveler);
            return Ok("updated successfully");
        }

        // DELETE api/traveler/652231bdc0273fd1118a104f
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
                return NotFound();
            await _travelerService.DeleteAysnc(id);
            return Ok("deleted successfully");
        }


        // POST api/traveler/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // access loginRequest.Id and loginRequest.Password
            string NIC = loginRequest.Id;
            string password = loginRequest.Password;
            Console.WriteLine("Login request received for " + NIC);
            Console.WriteLine("Login request received for " + password);

            // Retrieve the agent by RegNo from MongoDB
            var agent = await _travelerService.GetByTravelerIdAsync(NIC);

            //Console.WriteLine("Login request agent agent " + agent);

            // Check if a agent with the given agent Reg No was found
            if (agent != null)
            {
                // Verify the entered password against the stored hashed password
                if (agent.VerifyPassword(password))
                {
                    // Password is correct; proceed with login
                    // Return a success response
                    //Console.WriteLine("Login successful");
                    return Ok(new { Message = "Login successful", Data = agent }); // TODO: return a token or user information here
                }
            }

            // Either the agent member was not found or the password is incorrect
            // Return an unauthorized response
            return Unauthorized("Authentication failed");
        }


        // GET api/traveler/654567567V
        [HttpGet("nic/{nic}")]
        public async Task<IActionResult> GetByNICS(string nic)
        {
            var traveler = await _travelerService.GetByNICS(nic);
            if (traveler == null)
            {
                return NotFound();
            }
            return Ok(traveler);
        }

        // DELETE api/traveler/nic/{nic}
        [HttpDelete("nic/{nic}")]
        public async Task<IActionResult> DeleteByNIC(string nic)
        {
            var traveler = await _travelerService.GetByNIC(nic);

            if (traveler == null)
            {
                return NotFound();
            }

            // Implement the logic to delete the traveler by NIC
            await _travelerService.DeleteByNIC(nic);

            // Return a custom response message
            return Ok(new { Message = "Traveler deleted successfully." });
        }


        // PUT api/traveler/nic/123456789
        [HttpPut("nics/{nic}")]
        public async Task<IActionResult> PutByNic(string nic, [FromBody] Traveler newTraveler)
        {
            var traveler = await _travelerService.GetByNIC(nic);
            if (traveler == null)
                return NotFound();

            await _travelerService.UpdateByNic(nic, newTraveler);
            return Ok("Updated successfully");
        }

        [HttpPut("nic/{nic}")]
        public async Task<IActionResult> PutByNIC(string nic, [FromBody] Traveler updatedTraveler)
        {
            // Validate the incoming data
            if (string.IsNullOrEmpty(nic) || updatedTraveler == null)
            {
                return BadRequest("Invalid input.");
            }

            // Retrieve the existing traveler
            var traveler = await _travelerService.GetByNIC(nic);
            if (traveler == null)
            {
                return NotFound();
            }

            try
            {
                // Update the traveler's properties
                traveler.NIC = updatedTraveler.NIC;
                traveler.FullName = updatedTraveler.FullName;
                traveler.DOB = updatedTraveler.DOB;
                traveler.Gender = updatedTraveler.Gender;
                traveler.Contact = updatedTraveler.Contact;
                traveler.Email = updatedTraveler.Email;
                traveler.Address = updatedTraveler.Address;
                traveler.Username = updatedTraveler.Username;
                traveler.Profile = updatedTraveler.Profile;
                traveler.TravelerType = updatedTraveler.TravelerType;
                traveler.AccountStatus = updatedTraveler.AccountStatus;
                traveler.CreatedAt = updatedTraveler.CreatedAt;

                // Update the traveler in the database
                await _travelerService.UpdateByNicRequirdOnly(nic, traveler);
                return Ok(new { Message = "Updated successfully" });
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                // Consider logging the exception
                return StatusCode(500, "An error occurred during the update.");
            }
        }

    }
}
