using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/admin/[controller]")]
    public class AuthAdminController:ControllerBase
    {
    
        private readonly AdminManagerService _userManagerService;

        private readonly ILogger _loggerInfo;

        public AuthAdminController(AdminManagerService userManagerService, ILogger<AdminManagerController> loggerInfo)
        {
            _userManagerService = userManagerService;
            _loggerInfo = loggerInfo;

        }

        // Authenticate admin using NIC and Password
        [HttpPost]
        public async Task<ActionResult<AdminManagerModel>> Authenticate(AdminAuth usermanagerauth)
        {
            _loggerInfo.LogInformation("UserManagerAuthenticationController - Post()");
            var admin = await _userManagerService.VerifyUserByNICAndPasswordAsync(usermanagerauth.NIC, usermanagerauth.Password);

            if (admin is null)
            {
                return NotFound();
            }

            return admin;
        }
    }
}
