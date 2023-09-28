using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class UserManagerService
    {
        private readonly IMongoCollection<UserManagerModel> _userManagerCollection;
        private readonly ILogger _logger;

        public UserManagerService(IOptions<DBConnection> DBSettings, ILogger<UserManagerService> logger)
        {
            var mongoClient = new MongoClient(DBSettings.Value.URI);
            var mongoDatabase = mongoClient.GetDatabase(DBSettings.Value.DBName);
            _userManagerCollection = mongoDatabase.GetCollection<UserManagerModel>(DBSettings.Value.UsersCollection);
            _logger = logger;

        }
    }
}