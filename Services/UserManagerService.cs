using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class UserManagerService
    {
        private readonly IMongoCollection<UserManagerModel> _userManagerCollection;
        private readonly ILogger _loggerInfo;

        public UserManagerService(IOptions<DBConnection> DBSettings, ILogger<UserManagerService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _userManagerCollection = ESDatabase.GetCollection<UserManagerModel>(DBSettings.Value.UsersCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All Users inside the Admin side only
        public async Task<List<UserManagerModel>> GetAllUsersAsync()
        {
            _loggerInfo.LogInformation("UserManagerService using GetAllUsersAsync()");
            return await _userManagerCollection.Find(_ => true).ToListAsync();

        }
        

        // create new user inside the admin side only
        public async Task CreateUserAsync(UserManagerModel createUser)
        {
            _loggerInfo.LogInformation("UserManagerService using CreateAsync()");
            await _userManagerCollection.InsertOneAsync(createUser);

        }

        // Get User By Id inside the admin side only
        public async Task<UserManagerModel?> GetUserByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting user id UserManagerService using GetByIdAsync()");
            return await _userManagerCollection.Find(USER => USER.Id == id).FirstOrDefaultAsync();

        }

        // Verify User By NIC and Password inside the admin side only
        public async Task<UserManagerModel?> VerifyUserByNICAndPasswordAsync(String nic, String password)
        {
            _loggerInfo.LogInformation("Verify User UserManagerService using VerifyUserByNICAndPasswordAsync()");
            return await _userManagerCollection.Find(USER => USER.NIC == nic && USER.Password == password).FirstOrDefaultAsync();

        }

        // Update user inside the admin side only
        public async Task UpdateUserAsync(string id, UserManagerModel updatedUser)
        {
            _loggerInfo.LogInformation("Update User UserManagerService using UpdateUserAsync()");
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