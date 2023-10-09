using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class TrainScheduleService
    {
        private readonly IMongoCollection<TrainSchedulesModel> _trainScheduleManagerCollection;
        private readonly ILogger _loggerInfo;

        public TrainScheduleService(IOptions<DBConnection> DBSettings, ILogger<TrainScheduleService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _trainScheduleManagerCollection = ESDatabase.GetCollection<TrainSchedulesModel>(DBSettings.Value.TrainScheduleCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All train schedule
        public async Task<List<TrainSchedulesModel>> GetAllTrainScheduleAsync()
        {
            _loggerInfo.LogInformation("TrainScheduleService using GetAllTrainSchedulesAsync()");
            return await _trainScheduleManagerCollection.Find(_ => true).ToListAsync();

        }

        // create new train schedule
        public async Task CreateTrainScheduleAsync(TrainSchedulesModel createSchedule)
        {
            _loggerInfo.LogInformation("TrainScheduleService using CreateAsync()");
            await _trainScheduleManagerCollection.InsertOneAsync(createSchedule);

        }

        //Get all train schedule by Train Id

        public async Task<TrainSchedulesModel?> GetByTrainIdAsync(string id)
        {
            _loggerInfo.LogInformation("TrainScheduleService - GetByTrainIdAsync()");
            return await _trainScheduleManagerCollection.Find(TRAINSCHEDULE => TRAINSCHEDULE.TrainId == id).FirstOrDefaultAsync();

        }

        // Get Train Schedule By Id 
        public async Task<TrainSchedulesModel?> GetTrainScheduleByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting TrainScheduleService using GetTrainScheduleByIdAsync()");
            return await _trainScheduleManagerCollection.Find(TRAINSCHEDULE => TRAINSCHEDULE.Id == id).FirstOrDefaultAsync();

        }

        // Update Train Schedule
        public async Task UpdateTrainScheduleAsync(string id, TrainSchedulesModel updatedTrainSchedule)
        {
            _loggerInfo.LogInformation("Update User TrainScheduleService using UpdateTrainScheduleAsync()");
            await _trainScheduleManagerCollection.ReplaceOneAsync(TRAINSCHEDULE => TRAINSCHEDULE.Id == id, updatedTrainSchedule);

        }

        // Delete Train Schedule
        public async Task DeleteTrainScheduleAsync(string id)
        {
            _loggerInfo.LogInformation("Deleting User Manager Service using DeleteTrainScheduleAsync()");
            await _trainScheduleManagerCollection.DeleteOneAsync(TRAINSCHEDULE => TRAINSCHEDULE.Id == id);

        }
    }
}