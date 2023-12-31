/**
 * Filename: TrainScheduleController.cs
 * Author: IT20127046 
 * Modified By: IT20127046
 * Description: This controller class handles HTTP requests for TrainSchedule objects.
 *              It implements GET, POST, PUT and DELETE methods for TrainSchedule objects.
 *              The controller uses ITrainScheduleService to interact with the database.
 */
/**
 
*/

using Microsoft.AspNetCore.Mvc;
using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDotnetDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainScheduleController : ControllerBase
    {
        private readonly ITrainScheduleService _trainScheduleService;

        public TrainScheduleController(ITrainScheduleService trainScheduleService)
        {
            _trainScheduleService = trainScheduleService;
        }

        // GET: api/TrainSchedule
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trainSchedules = await _trainScheduleService.GetAllAsync();
            return Ok(trainSchedules);
        }

        // GET api/TrainSchedule/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var trainSchedule = await _trainScheduleService.GetByIdAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }
            return Ok(trainSchedule);
        }

        // POST api/TrainSchedule
        [HttpPost]
        public async Task<IActionResult> Post(TrainSchedule trainSchedule)
        {
            await _trainScheduleService.CreateAsync(trainSchedule);
            return Ok("Created successfully");
        }

        // PUT api/TrainSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TrainSchedule newTrainSchedule)
        {
            var trainSchedule = await _trainScheduleService.GetByIdAsync(id);
            if (trainSchedule == null)
                return NotFound();

            await _trainScheduleService.UpdateAsync(id, newTrainSchedule);
            return Ok("Updated successfully");
        }

        // DELETE api/TrainSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trainSchedule = await _trainScheduleService.GetByIdAsync(id);
            if (trainSchedule == null)
                return NotFound();

            await _trainScheduleService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }

        // GET api/TrainSchedule/tno/5
        [HttpGet("tno/{id}")]
        public async Task<IActionResult> GetByTrainNo(string id)
        {
            var trainSchedule = await _trainScheduleService.GetByTrainNoAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }
            return Ok(trainSchedule);
        }
    }
}
