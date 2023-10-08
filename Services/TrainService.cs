using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<TrainModel> _trainManagerCollection;
        private readonly ILogger _loggerInfo;

        public TrainService(IOptions<DBConnection> DBSettings, ILogger<TrainService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _trainManagerCollection = ESDatabase.GetCollection<TrainModel>(DBSettings.Value.TrainsCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All Trains inside the Admin side only
        public async Task<List<TrainModel>> GetAllTrainsAsync()
        {
            _loggerInfo.LogInformation("TrainService using GetAllUsersAsync()");
            return await _trainManagerCollection.Find(_ => true).ToListAsync();

        }
        

        // create new train
        public async Task CreateTrainAsync(TrainModel createTrain)
        {
            _loggerInfo.LogInformation("TrainService using CreateAsync()");
            await _trainManagerCollection.InsertOneAsync(createTrain);

        }

        // Get Train By Id
        public async Task<TrainModel?> GetTrainByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting train id TrainService using GetByIdAsync()");
            return await _trainManagerCollection.Find(TRAIN => TRAIN.Id == id).FirstOrDefaultAsync();

        }

        // Update train
        public async Task UpdateTrainAsync(string id, TrainModel updatedUser)
        {
            _loggerInfo.LogInformation("Update train TrainService using UpdateTrainAsync()");
            await _trainManagerCollection.ReplaceOneAsync(USER => USER.Id == id, updatedUser);

        }

        // Delete Train
        public async Task DeleteTrainAsync(string id)
        {
            _loggerInfo.LogInformation("Deleting Train");
            await _trainManagerCollection.DeleteOneAsync(USER => USER.Id == id);

        }
    }
}