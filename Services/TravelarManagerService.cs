using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class TravelarManagerService
    {
        private readonly IMongoCollection<TravelarManagerModel> _travelarManagerCollection;
        private readonly ILogger _loggerInfo;

        public TravelarManagerService(IOptions<DBConnection> DBSettings, ILogger<TravelarManagerService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _travelarManagerCollection = ESDatabase.GetCollection<TravelarManagerModel>(DBSettings.Value.TravelarsCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All Travelars inside the Mobile side only
        public async Task<List<TravelarManagerModel>> GetAllTravelarsAsync()
        {
            _loggerInfo.LogInformation("TravelarManagerService using GetAllTravelarsAsync()");
            return await _travelarManagerCollection.Find(_ => true).ToListAsync();

        }
        

        // create new Travelars inside the Mobile side only
        public async Task CreateTravelarAsync(TravelarManagerModel createTravelar)
        {
            _loggerInfo.LogInformation("TravelarManagerService using CreateAsync()");
            await _travelarManagerCollection.InsertOneAsync(createTravelar);

        }

        // Get Travelar By Id inside the Mobile side only
        public async Task<TravelarManagerModel?> GetTravelarByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting Travelars id TravelarManagerService using GetByIdAsync()");
            return await _travelarManagerCollection.Find(TRAVELAR => TRAVELAR.Id == id).FirstOrDefaultAsync();

        }

        // Verify Travelar By NIC and Password inside the Mobile side only
        public async Task<TravelarManagerModel?> VerifyTravelarByNICAndPasswordAsync(String nic, String password)
        {
            _loggerInfo.LogInformation("Verify Travelar TravelarManagerService using VerifyTravelarByNICAndPasswordAsync()");
            return await _travelarManagerCollection.Find(TRAVELAR => TRAVELAR.NIC == nic && TRAVELAR.Password == password).FirstOrDefaultAsync();

        }

        // Update Travelar inside the Mobile side only
        public async Task UpdateTravelarAsync(string id, TravelarManagerModel updatedTravelar)
        {
            _loggerInfo.LogInformation("Update User TravelarManagerService using UpdateUserAsync()");
            await _travelarManagerCollection.ReplaceOneAsync(TRAVELAR => TRAVELAR.Id == id, updatedTravelar);

        }

        // Delete Travelar inside the Mobile side only
        public async Task DeleteTravelarAsync(string id)
        {
            _loggerInfo.LogInformation("Deleting Travelar Manager Service using DeleteTravelarAsync()");
            await _travelarManagerCollection.DeleteOneAsync(TRAVELAR => TRAVELAR.Id == id);

        }
    }
}