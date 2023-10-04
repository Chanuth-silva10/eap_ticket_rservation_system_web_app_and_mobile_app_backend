using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<TrainModel> _trainCollection;
        private readonly ILogger _loggerInfo;

        public TrainService(IOptions<DBConnection> DBSettings, ILogger<TrainService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _trainCollection = ESDatabase.GetCollection<TrainModel>(DBSettings.Value.TrainsCollection);
            _loggerInfo = loggerInfo;

        }

        // Get all trains
        public async Task<List<TrainModel>> GetAllTrainsAsync()
        {
            _loggerInfo.LogInformation("TrainService getting all trains using GetAllTrainsAsync()");
            return await _trainManagerCollection.Find(_ => true).ToListAsync();

        }


        // create new train
        public async Task CreateTrainAsync(TrainModel newTrain)
        {
            _loggerInfo.LogInformation("TrainService creating a train using CreateAsync()");
            await _trainCollection.InsertOneAsync(newTrain);

        }

        // Get train by id
        public async Task<TrainModel?> GetTrainByIdAsync(String id)
        {
            _loggerInfo.LogInformation("TrainService getting a train by id using GetByIdAsync()");
            return await _trainCollection.Find(TRAIN => TRAIN.Id == id).FirstOrDefaultAsync();

        }

        // Update train
        public async Task UpdateTrainAsync(string id, TrainModel updatedTrain)
        {
            _loggerInfo.LogInformation("TrainService updating train using UpdateTrainAsync()");
            await _trainCollection.ReplaceOneAsync(TRAIN => TRAIN.Id == id, updatedTrain);

        }

        // Delete train
        public async Task DeleteTrainAsync(string id)
        {
            _loggerInfo.LogInformation("TrainService deleting train using DeleteTrainAsync()");
            await _trainCollection.DeleteOneAsync(TRAIN => TRAIN.Id == id);

        }
    }
}