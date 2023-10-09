using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly TravelarManagerService _travelarManagerService;
         private readonly TrainService _trainService;
        private readonly TrainScheduleService _trainScheduleService;
        private readonly ILogger _loggerInfo;

        public ReservationsController(ReservationService reservationService, TravelarManagerService travelarManagerService, TrainService trainService , TrainScheduleService trainScheduleService , ILogger<ReservationsController> loggerInfo)
        {
            _reservationService = reservationService;
            _travelarManagerService = travelarManagerService;
            _trainService = trainService;
            _trainScheduleService = trainScheduleService;
            _loggerInfo = loggerInfo;

        }

        // Get All Train  Reservations
        [HttpGet]
        public async Task<List<ReservationsModel>> GetAllTrainReservations()
        {
            _loggerInfo.LogInformation("ReservationsController => GetAllTrainReservations()");
            return await _reservationService.GetAllTrainReservationAsync();

        }

        // Get Train Reservation
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ReservationsModel>> GetById(string id)
        {
            _loggerInfo.LogInformation("ReservationsController - GetById()");
            var reservations = await _reservationService.GetTrainReservationByIdAsync(id);

            if (reservations is null)
            {
                return NotFound();
            }

            return reservations;
        }

        // Create reservations
        [HttpPost]
        public async Task<IActionResult> Post(ReservationsModel createReservations)
        {
            _loggerInfo.LogInformation("ReservationsController => Post()");
            await _reservationService.CreateTrainReservationAsync(createReservations);

            return CreatedAtAction(nameof(GetAllTrainReservations), new { id = createReservations.Id }, createReservations);
        }

        // Update reservations
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ReservationsModel updatedReservation)
        {
            _loggerInfo.LogInformation("TainController - Update()");
            var TrainSchedule = await _reservationService.GetTrainReservationByIdAsync(id);
            Console. WriteLine(TrainSchedule);
            if (TrainSchedule is null)
            {
                return NotFound();
            }

            updatedReservation.Id = TrainSchedule.Id;

            await _reservationService.UpdateTrainReservationAsync(id, updatedReservation);

            return NoContent();
        }

        // Delete TrainSchedule
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _loggerInfo.LogInformation("ReservationsController => Delete()");
            var TrainSchedule = await _reservationService.GetTrainReservationByIdAsync(id);

            if (TrainSchedule is null)
            {
                return NotFound();
            }

            await _reservationService.DeleteTrainReservationAsync(id);

            return NoContent();
        }
    }
}