using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManagerService _userManagerService;
        private readonly ILogger _loggerInfo;


        public UserManagerController(UserManagerService userManagerService, ILogger<UserManagerController> loggerInfo)
        {
            _userManagerService = userManagerService;
            _loggerInfo = loggerInfo;

        }

        // Create a new user
        [HttpPost]
        public async Task<IActionResult> Post(UserManagerModel createUser)
        {
            _loggerInfo.LogInformation("UserManagerController => Post()");
            await _userManagerService.CreateAsync(createUser);

            return CreatedAtAction(new { id = createUser.Id }, createUser);
        }
    }
}