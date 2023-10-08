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
    }
}
