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

        // GET api/TravelerController/5
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

        // GET api/TravelerController/5
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

        // POST api/TravelerController
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Traveler traveler)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(traveler.PasswordHash);
            traveler.PasswordHash = hashedPassword;
            
           await _travelerService.CreateAsync(traveler);
            return Ok(new { StatusCode = "created successfully" });
        }

        // PUT api/TravelerController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Traveler newTraveler)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
                return NotFound();
            await _travelerService.UpdateAsync(id, newTraveler);
            return Ok("updated successfully");
        }

        // DELETE api/TravelerController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var traveler = await _travelerService.GetById(id);
            if (traveler == null)
                return NotFound();
            await _travelerService.DeleteAysnc(id);
            return Ok("deleted successfully");
        }

        // POST api/Traveler/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // access loginRequest.Id and loginRequest.Password
            string NIC = loginRequest.Id;
            string password = loginRequest.Password;
            //Console.WriteLine("Login request received for " + NIC);
            //Console.WriteLine("Login request received for " + password);

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
    }
}
