using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TravelarManagerController : ControllerBase
    {
        private readonly TravelarManagerService _travelarManagerService;
        private readonly ILogger _loggerInfo;

        public TravelarManagerController(TravelarManagerService travelarManagerService, ILogger<TravelarManagerController> loggerInfo)
        {
            _travelarManagerService = travelarManagerService;
            _loggerInfo = loggerInfo;

        }

        // Get All Travelars
        [HttpGet]
        public async Task<List<TravelarManagerModel>> GetTravelars()
        {
            _loggerInfo.LogInformation("TravelarManagerController => GetTravelars()");
            return await _travelarManagerService.GetAllTravelarsAsync();

        }

        // Get Travelar using Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TravelarManagerModel>> GetById(string id)
        {
            _loggerInfo.LogInformation("TravelarManagerController - GetById()");
            var admin = await _travelarManagerService.GetTravelarByIdAsync(id);

            if (admin is null)
            {
                return NotFound();
            }

            return admin;
        }

        // Create travelar
        [HttpPost]
        public async Task<IActionResult> Post(TravelarManagerModel createTravelar)
        {
            _loggerInfo.LogInformation("TravelarManagerController => Post()");
            await _travelarManagerService.CreateTravelarAsync(createTravelar);

            return CreatedAtAction(nameof(GetTravelars), new { id = createTravelar.Id }, createTravelar);
        }

        // Update travelar
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TravelarManagerModel updatedTravelar)
        {
            _loggerInfo.LogInformation("TRavelarController - Update()");
            var Travelar = await _travelarManagerService.GetTravelarByIdAsync(id);
            Console. WriteLine(Travelar);
            if (Travelar is null)
            {
                return NotFound();
            }

            updatedTravelar.Id = Travelar.Id;

            await _travelarManagerService.UpdateTravelarAsync(id, updatedTravelar);

            return NoContent();
        }

        // Delete Travelar
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _loggerInfo.LogInformation("TravelarManagerController => Delete()");
            var Travelar = await _travelarManagerService.GetTravelarByIdAsync(id);

            if (Travelar is null)
            {
                return NotFound();
            }

            await _travelarManagerService.DeleteTravelarAsync(id);

            return NoContent();
        }
    }
}