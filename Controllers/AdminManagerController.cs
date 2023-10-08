using TicketReservationManager.Models;
using TicketReservationManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketReservationManager.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AdminManagerController : ControllerBase
    {
        private readonly AdminManagerService _userManagerService;
        private readonly ILogger _loggerInfo;


        public AdminManagerController(AdminManagerService userManagerService, ILogger<AdminManagerController> loggerInfo)
        {
            _userManagerService = userManagerService;
            _loggerInfo = loggerInfo;

        }

        // Get All Users
        [HttpGet]
        public async Task<List<AdminManagerModel>> GetUsers()
        {
            _loggerInfo.LogInformation("AdminManagerController => GetUsers()");
            return await _userManagerService.GetAllUsersAsync();

        }

        // Get User using Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<AdminManagerModel>> GetById(string id)
        {
            _loggerInfo.LogInformation("AdminManagerController - GetById()");
            var user = await _userManagerService.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // Create user
        [HttpPost]
        public async Task<IActionResult> Post(AdminManagerModel createUser)
        {
            _loggerInfo.LogInformation("AdminManagerController => Post()");
            await _userManagerService.CreateUserAsync(createUser);

            return CreatedAtAction(nameof(GetUsers), new { id = createUser.Id }, createUser);
        }

        // Update user
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, AdminManagerModel updatedUser)
        {
            _loggerInfo.LogInformation("UserController - Update()");
            var User = await _userManagerService.GetUserByIdAsync(id);
            Console. WriteLine(User);
            if (User is null)
            {
                return NotFound();
            }

            updatedUser.Id = User.Id;

            await _userManagerService.UpdateUserAsync(id, updatedUser);

            return NoContent();
        }

        // Delete User
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            _loggerInfo.LogInformation("AdminManagerController => Delete()");
            var User = await _userManagerService.GetUserByIdAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            await _userManagerService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}