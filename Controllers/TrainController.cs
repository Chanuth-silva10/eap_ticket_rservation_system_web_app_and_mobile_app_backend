using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly TrainService _trainManagerService;
        private readonly ILogger _loggerInfo;

        public TrainController(TrainService trainManagerService, ILogger<TrainController> loggerInfo)
        {
            _trainManagerService = trainManagerService;
            _loggerInfo = loggerInfo;

        }

        // Get All Trains
        [HttpGet]
        public async Task<List<TrainModel>> GetAllTrainsAsync()
        {
            _loggerInfo.LogInformation("TrainController => GetAllTrainsAsync()");
            return await _trainManagerService.GetAllTrainsAsync();

        }

        // Get Train using Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TrainModel>> GetById(string id)
        {
            _loggerInfo.LogInformation("TrainController - GetById()");
            var train = await _trainManagerService.GetTrainByIdAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            return train;
        }

        // Create train
        [HttpPost]
        public async Task<IActionResult> Post(TrainModel createTrain)
        {
            _loggerInfo.LogInformation("TrainController => Post()");
            await _trainManagerService.CreateTrainAsync(createTrain);

            return CreatedAtAction(nameof(GetAllTrainsAsync), new { id = createTrain.Id }, createTrain);
        }

        // Update train
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TrainModel updatedTrain)
        {
            _loggerInfo.LogInformation("TainController - Update()");
            var Train = await _trainManagerService.GetTrainByIdAsync(id);
            Console. WriteLine(Train);
            if (Train is null)
            {
                return NotFound();
            }

            updatedTrain.Id = Train.Id;

            await _trainManagerService.UpdateTrainAsync(id, updatedTrain);

            return NoContent();
        }

        // Delete Train
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _loggerInfo.LogInformation("TrainController => Delete()");
            var Train = await _trainManagerService.GetTrainByIdAsync(id);

            if (Train is null)
            {
                return NotFound();
            }

            await _trainManagerService.DeleteTrainAsync(id);

            return NoContent();
        }
    }
}