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
        [HttpPut("{nic}")]
        public async Task<IActionResult> Update(string nic, TravelarManagerModel updatedTravelar)
        {
            _loggerInfo.LogInformation("TRavelarController - Update()");
            var Travelar = await _travelarManagerService.GetTravelarByNICAsync(nic);
            Console. WriteLine(Travelar);
            if (Travelar is null)
            {
                return NotFound();
            }

            updatedTravelar.NIC = Travelar.NIC;

            await _travelarManagerService.UpdateTravelarAsync(nic, updatedTravelar);

            return NoContent();
        }
        
        // Activated travelar
        [HttpPut]
        [Route("updateTravelarStatus/{id}")]
        public async Task<IActionResult> UpdateTravelarStatus(string id, TravelarManagerModel updatedTravelarStatus)
        {
            _loggerInfo.LogInformation("UpdateTravelarStatus - Update()");
            var Travelar = await _travelarManagerService.GetTravelarByIdAsync(id);
          
            if (Travelar is null)
            {
                return NotFound();
            }

            Travelar.IsActive = updatedTravelarStatus.IsActive;

            await _travelarManagerService.UpdateTravelarAccountStatusAsync(id, Travelar);

            return NoContent();
        }

        // Delete Travelar
        [HttpDelete("{nic}")]
        public async Task<IActionResult> Delete(string nic)
        {
            _loggerInfo.LogInformation("TravelarManagerController => Delete()");
            var Travelar = await _travelarManagerService.GetTravelarByNICAsync(nic);

            if (Travelar is null)
            {
                return NotFound();
            }

            await _travelarManagerService.DeleteTravelarAsync(nic);

            return NoContent();
        }
    }
}