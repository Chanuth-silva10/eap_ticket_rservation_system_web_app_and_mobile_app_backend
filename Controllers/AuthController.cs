using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        

        private readonly AdminManagerService _userManagerService;

        private readonly ILogger _loggerInfo;

        public AuthController(AdminManagerService userManagerService, ILogger<AdminManagerController> loggerInfo)
        {
            _userManagerService = userManagerService;
            _loggerInfo = loggerInfo;

        }

        // Authenticate user using NIC and Password
        [HttpPost]
        public async Task<ActionResult<AdminManagerModel>> Authenticate(AdminAuth usermanagerauth)
        {
            _loggerInfo.LogInformation("UserManagerAuthenticationController - Post()");
            var user = await _userManagerService.VerifyUserByNICAndPasswordAsync(usermanagerauth.NIC, usermanagerauth.Password);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
