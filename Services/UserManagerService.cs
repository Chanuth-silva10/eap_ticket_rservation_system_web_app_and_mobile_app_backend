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

        //Get All Users
        public async Task<List<UserManagerModel>> GetAllAsync()
        {
            _loggerInfo.LogInformation("UserService - GetAllAsync()");
            return await _userManagerCollection.Find(_ => true).ToListAsync();

        }

        //create new user
        public async Task CreateAsync(UserManagerModel createUser)
        {
            _loggerInfo.LogInformation("Start UserManagerService using CreateAsync()");
            await _userManagerCollection.InsertOneAsync(createUser);

        }
    }
}