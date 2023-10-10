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

        public ReservationsController(ReservationService reservationService, TravelarManagerService travelarManagerService, TrainService trainService, TrainScheduleService trainScheduleService, ILogger<ReservationsController> loggerInfo)
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
            
            _loggerInfo.LogInformation("CheckTrainReservationsAsync => Count()");
            int count = await _reservationService.CheckTrainReservationsAsync(createReservations.UserId);

            if (count <= 4)
            {
                return BadRequest("Maximum 4 reservations allowed your acount.");
            }

            // if (createReservations.ReservationDate < DateTime.Now.AddDays(1) || createReservations.ReservationDate > DateTime.Now.AddDays(30))
            // {
            //     return BadRequest("Invalid reservation date.");
            // }

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

            if (TrainSchedule is null)
            {
                return NotFound();
            }

            var daysUntilReservation = (TrainSchedule.ReservationDate - DateTime.Now).Days;

            if (daysUntilReservation <= 5)
            {
                return BadRequest("Reservation cannot be updated less than 5 days before the reservation date.");
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

            var daysUntilReservation = (TrainSchedule.ReservationDate - DateTime.Now).Days;

            if (daysUntilReservation <= 5)
            {
                return BadRequest("Reservation cannot be canceled less than 5 days before the reservation date.");
            }

            await _reservationService.DeleteTrainReservationAsync(id);

            return Ok("Reservation canceled successfully.");
        }
    }
}