using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TrainScheduleController : ControllerBase
    {
        private readonly TrainScheduleService _trainScheduleService;
        private readonly TrainService _trainService;
        private readonly ILogger _loggerInfo;

        public TrainScheduleController(TrainScheduleService trainScheduleService, TrainService trainService, ILogger<TrainScheduleController> loggerInfo)
        {
            _trainScheduleService = trainScheduleService;
            _trainService = trainService;
            _loggerInfo = loggerInfo;

        }

        // Get All Train Schedule
        [HttpGet]
        public async Task<List<TrainSchedulesModel>> GetAllTrainSchedules()
        {
            _loggerInfo.LogInformation("TrainScheduleController => GetAllTrains()");
            return await _trainScheduleService.GetAllTrainScheduleAsync();

        }

        // Get TrainSchedule using Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TrainSchedulesModel>> GetById(string id)
        {
            _loggerInfo.LogInformation("TrainScheduleController - GetById()");
            var trainSchedule = await _trainScheduleService.GetTrainScheduleByIdAsync(id);

            if (trainSchedule is null)
            {
                return NotFound();
            }

            return trainSchedule;
        }

         //Get train schedule by Train Id
        [HttpGet]
        [Route("train/{id}")]
        public async Task<ActionResult<TrainSchedulesModel>> GetByTrainId(string id)
        {
            _loggerInfo.LogInformation("TrainSchduleController - GetByTrainId()");
            var TrainSchedules = await _trainScheduleService.GetByTrainIdAsync(id);

            if (TrainSchedules is null)
            {
                return NotFound();
            }

            return TrainSchedules;
        }

        // Create trainSchedule
        [HttpPost]
        public async Task<IActionResult> Post(TrainSchedulesModel createTrainSchedule)
        {
            _loggerInfo.LogInformation("TrainScheduleController => Post()");
            await _trainScheduleService.CreateTrainScheduleAsync(createTrainSchedule);

            return CreatedAtAction(nameof(GetAllTrainSchedules), new { id = createTrainSchedule.Id }, createTrainSchedule);
        }

        // Update trainSchedule
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TrainSchedulesModel updatedTrainSchedule)
        {
            _loggerInfo.LogInformation("TainController - Update()");
            var TrainSchedule = await _trainScheduleService.GetTrainScheduleByIdAsync(id);
            Console. WriteLine(TrainSchedule);
            if (TrainSchedule is null)
            {
                return NotFound();
            }

            updatedTrainSchedule.Id = TrainSchedule.Id;

            await _trainScheduleService.UpdateTrainScheduleAsync(id, updatedTrainSchedule);

            return NoContent();
        }

        // Delete TrainSchedule
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _loggerInfo.LogInformation("TrainScheduleController => Delete()");
            var TrainSchedule = await _trainScheduleService.GetTrainScheduleByIdAsync(id);

            if (TrainSchedule is null)
            {
                return NotFound();
            }

            await _trainScheduleService.DeleteTrainScheduleAsync(id);

            return NoContent();
        }
    }
}