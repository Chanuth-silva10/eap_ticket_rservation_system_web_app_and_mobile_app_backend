using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class AdminManagerService
    {
        private readonly IMongoCollection<AdminManagerModel> _userManagerCollection;
        private readonly ILogger _loggerInfo;

        public AdminManagerService(IOptions<DBConnection> DBSettings, ILogger<AdminManagerService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _userManagerCollection = ESDatabase.GetCollection<AdminManagerModel>(DBSettings.Value.AdminsCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All Users inside the Admin side only
        public async Task<List<AdminManagerModel>> GetAllUsersAsync()
        {
            _loggerInfo.LogInformation("AdminManagerService using GetAllUsersAsync()");
            return await _userManagerCollection.Find(_ => true).ToListAsync();

        }
        

        // create new admin inside the admin side only
        public async Task CreateUserAsync(AdminManagerModel createUser)
        {
            _loggerInfo.LogInformation("AdminManagerService using CreateAsync()");
            await _userManagerCollection.InsertOneAsync(createUser);

        }

        // Get User By Id inside the admin side only
        public async Task<AdminManagerModel?> GetUserByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting admin id AdminManagerService using GetByIdAsync()");
            return await _userManagerCollection.Find(USER => USER.Id == id).FirstOrDefaultAsync();

        }

        // Verify User By NIC and Password inside the admin side only
        public async Task<AdminManagerModel?> VerifyUserByNICAndPasswordAsync(String nic, String password)
        {
            _loggerInfo.LogInformation("Verify User AdminManagerService using VerifyUserByNICAndPasswordAsync()");
            return await _userManagerCollection.Find(USER => USER.NIC == nic && USER.Password == password).FirstOrDefaultAsync();

        }

        // Update admin inside the admin side only
        public async Task UpdateUserAsync(string id, AdminManagerModel updatedUser)
        {
            _loggerInfo.LogInformation("Update User AdminManagerService using UpdateUserAsync()");
            await _userManagerCollection.ReplaceOneAsync(USER => USER.Id == id, updatedUser);

        }

        // Delete User inside the admin side only
        public async Task DeleteUserAsync(string id)
        {
            _loggerInfo.LogInformation("Deleting User Manager Service using DeleteUserAsync()");
            await _userManagerCollection.DeleteOneAsync(USER => USER.Id == id);

        }
    }
}