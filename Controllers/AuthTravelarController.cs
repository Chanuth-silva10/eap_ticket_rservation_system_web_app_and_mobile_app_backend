using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/travelar/[controller]")]
    public class AuthTravelarController:ControllerBase
    {
    
        private readonly TravelarManagerService _userManagerService;

        private readonly ILogger _loggerInfo;

        public AuthTravelarController(TravelarManagerService userManagerService, ILogger<AdminManagerController> loggerInfo)
        {
            _userManagerService = userManagerService;
            _loggerInfo = loggerInfo;

        }

        // Authenticate admin using NIC and Password
        [HttpPost]
        public async Task<ActionResult<TravelarManagerModel>> Authenticate(AdminAuth usermanagerauth)
        {
            _loggerInfo.LogInformation("UserManagerAuthenticationController - Post()");
            var admin = await _userManagerService.VerifyTravelarByNICAndPasswordAsync(usermanagerauth.NIC, usermanagerauth.Password);

            if (admin is null)
            {
                return NotFound();
            }

            return admin;
        }
    }
}
