// ----------------------------------------------------------------------------
// File: TravelAgentController.cs
// Author: IT20125202
// Description: Controller class to handle travel agent-related operations such as listing travel agents, adding new agents,
//              updating agent information, deleting agents, and handling agent login.
// Version: 1.0.0
// Route: /api/TravelAgent
// ----------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelAgentController : ControllerBase
    {
        private readonly ITravelAgentService _travelAgentService;
        public TravelAgentController(ITravelAgentService travelAgentService)
        {
            _travelAgentService = travelAgentService;
        }

        // GET: api/TravelAgent
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // get all agent details from database
            var allAgents = await _travelAgentService.GetAllAsync();
            return Ok(allAgents);
        }

        // GET api/TravelAgent/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // get a specific agent details using registration number
            var agent = await _travelAgentService.GetByAgentIdAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        // POST api/TravelAgent
        [HttpPost]
        public async Task<IActionResult> Post(TravelAgent travelAgent)
        {
            // Check if a user with the same RegNo already exists
            var existingTravelAgent = await _travelAgentService.GetByAgentIdAsync(travelAgent.RegNo);
            if (existingTravelAgent != null)
            {
                // A user with the same RegNo already exists; return a conflict response
                return Conflict("Registration Number already exists.");
            }

            // Check if a user with the same username already exists
            var existingAgentByUsername = await _travelAgentService.GetByUsernameAsync(travelAgent.UserName);
            if (existingAgentByUsername != null)
            {
                // A user with the same username already exists; return a conflict response
                return Conflict("Username already exists.");
            }

            // Hash the password
            var password = travelAgent.HashedPassword;
            travelAgent.SetPassword(password);

            // Create the new travel agent
            await _travelAgentService.CreateAsync(travelAgent);

            // Return a success response
            return Ok("created successfully");
        }

        // PUT api/TravelAgent/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TravelAgent newAgent)
        {
            // get the user by registration number
            var agent = await _travelAgentService.GetByAgentIdAsync(id);
            if (agent == null)
                return NotFound();

            // update the user
            await _travelAgentService.UpdateAsync(id, newAgent);
            return Ok("updated successfully");
        }

        // DELETE api/TravelAgent/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // get the user by registration number
            var agent = await _travelAgentService.GetByAgentIdAsync(id);
            if (agent == null)
                return NotFound();

            // delete the user account
            await _travelAgentService.DeleteAsync(id);
            return Ok("deleted successfully");
        }

        // POST api/TravelAgent/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // access loginRequest.Id and loginRequest.Password
            string regNo = loginRequest.Id;
            string password = loginRequest.Password;
            //Console.WriteLine("Login request received for " + regNo);

            // Retrieve the agent by RegNo from MongoDB
            var agent = await _travelAgentService.GetByAgentIdAsync(regNo);

            // Check if a agent with the given agent Reg No was found
            if (agent != null)
            {
                // Verify the entered password against the stored hashed password
                if (agent.VerifyPassword(password))
                {
                    // Password is correct; proceed with login
                    // Return a success response
                    //Console.WriteLine("Login successful");
                    return Ok(new { Message = "Login successful", Data = agent }); // return a token or user information
                }
            }

            // Either the agent member was not found or the password is incorrect
            // Return an unauthorized response
            return Unauthorized("Authentication failed");
        }
    }
}
