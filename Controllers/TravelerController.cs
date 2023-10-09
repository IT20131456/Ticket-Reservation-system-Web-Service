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

    }
}
