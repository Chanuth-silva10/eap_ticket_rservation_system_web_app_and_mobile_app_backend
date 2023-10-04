using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly TrainService _trainService;
        private readonly ILogger _loggerInfo;


        public TrainController(TrainService trainService, ILogger<TrainController> loggerInfo)
        {
            _trainService = trainService;
            _loggerInfo = loggerInfo;

        }

    // Get All Trains
    [HttpGet]
    public async Task<List<TrainModel>> GetAllTrains()
    {
        _loggerInfo.LogInformation("TrainController => GetAllTrains()");
        return await _trainService.GetAllTrainsAsync();

    }

    // Get train by id
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TrainModel>> GetById(string id)
    {
        _loggerInfo.LogInformation("TrainController - GetById()");
        var train = await _trainService.GetTrainByIdAsync(id);

        if (train is null)
        {
            return NotFound();
        }

        return train;
    }

    // Create a train
    [HttpPost]
    public async Task<IActionResult> Post(TrainModel createTrain)
    {
        _loggerInfo.LogInformation("TrainController => Post()");
        await _trainService.CreateTrainAsync(createTrain);

        return CreatedAtAction(nameof(GetAllTrains), new { id = createTrain.Id }, createTrain);
    }

    // Update Train
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TrainModel updatedTrain)
    {
        _loggerInfo.LogInformation("TrainController - Update()");
        var Train = await _trainService.GetTrainByIdAsync(id);
        Console.WriteLine(Train);
        if (Train is null)
        {
            return NotFound();
        }

        updatedTrain.Id = Train.Id;

        await _trainService.UpdateTrainAsync(id, updatedTrain);

        return NoContent();
    }

    // Delete Train
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        _loggerInfo.LogInformation("TrainController => Delete()");
        var Train = await _trainService.GetTrainByIdAsync(id);

        if (Train is null)
        {
            return NotFound();
        }

        await _trainService.DeleteTrainAsync(id);

        return NoContent();
    }
}
}