/*
 * Filename: TicketBookingController.cs
 * Description: Controller class for handling ticket booking operations.
 */

using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketBookingController : ControllerBase
    {
        private readonly ITicketBookingService _ticketBookingService;

        // Constructor for TicketBookingController
        public TicketBookingController(ITicketBookingService ticketBookingService)
        {
            _ticketBookingService = ticketBookingService;
        }

        // GET: api/TicketBooking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketBooking>>> Get()
        {
            var ticketBookings = await _ticketBookingService.GetAllAsync();
            return Ok(ticketBookings);
        }

        // GET: api/TicketBooking/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketBooking>> Get(string id)
        {

            var ticketBooking = await _ticketBookingService.GetById(id);
            if (ticketBooking == null)
            {
                return NotFound();
            }

            return Ok(ticketBooking);
        }

        // POST: api/TicketBooking
        [HttpPost]
        public async Task<IActionResult> Post(TicketBooking ticketBooking)
        {

            await _ticketBookingService.CreateAsync(ticketBooking);
            return Ok("Ticket booking created successfully");
        }

        // PUT: api/TicketBooking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, TicketBooking newTicketBooking)
        {

            var ticketBooking = await _ticketBookingService.GetById(id);
            if (ticketBooking == null)
                return NotFound();

            await _ticketBookingService.UpdateAsync(id, newTicketBooking);

            return Ok("Ticket booking updated successfully");
        }

        // DELETE: api/TicketBooking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var ticketBooking = await _ticketBookingService.GetById(id);
            if (ticketBooking == null)
                return NotFound();

            await _ticketBookingService.DeleteAsync(id);

            return Ok("Ticket booking deleted successfully");
        }



        // GET: api/TicketBooking/{nic}
        [HttpGet("nic/{nic}")]
        public async Task<ActionResult<IEnumerable<TicketBooking>>> GetByNic(string nic)
        {
            var ticketBookings = await _ticketBookingService.GetByReferenceIdAsync(nic);
            return Ok(ticketBookings);
        }


        // GET: api/TicketBooking/History/{nic}
        [HttpGet("History/{nic}")]
        public async Task<ActionResult<IEnumerable<TicketBooking>>> GetByNIC(string nic)
        {
           
            var ticketBookings = await _ticketBookingService.GetAllByRefId(nic);   
       
            if (ticketBookings == null)
            {
                return NotFound();
            }

            return Ok(ticketBookings);
        }

    }
}
