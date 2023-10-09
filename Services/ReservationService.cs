using TicketReservationManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TicketReservationManager.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<ReservationsModel> _reservationManagerCollection;
        private readonly ILogger _loggerInfo;

        public ReservationService(IOptions<DBConnection> DBSettings, ILogger<ReservationService> loggerInfo)
        {
            var DBClient = new MongoClient(DBSettings.Value.URI);
            var ESDatabase = DBClient.GetDatabase(DBSettings.Value.DBName);
            _reservationManagerCollection = ESDatabase.GetCollection<ReservationsModel>(DBSettings.Value.ReservationCollection);
            _loggerInfo = loggerInfo;

        }

        // Get All train Reservations
        public async Task<List<ReservationsModel>> GetAllTrainReservationAsync()
        {
            _loggerInfo.LogInformation("ReservationService using GetAllTrainReservationAsync()");
            return await _reservationManagerCollection.Find(_ => true).ToListAsync();

        }

        // create new Reservations
        public async Task CreateTrainReservationAsync(ReservationsModel reservation)
        {
            _loggerInfo.LogInformation("ReservationService using CreateTrainReservationAsync()");
            await _reservationManagerCollection.InsertOneAsync(reservation);

        }

        // Get Train Schedule By Id 
        public async Task<ReservationsModel?> GetTrainReservationByIdAsync(String id)
        {
            _loggerInfo.LogInformation("Getting ReservationService using GetTrainReservationByIdAsync()");
            return await _reservationManagerCollection.Find(RESERVATIONS => RESERVATIONS.Id == id).FirstOrDefaultAsync();

        }

        // Update Train Schedule
        public async Task UpdateTrainReservationAsync(string id, ReservationsModel updatedTrainSchedule)
        {
            _loggerInfo.LogInformation("Update User ReservationService using UpdateTrainReservationAsync()");
            await _reservationManagerCollection.ReplaceOneAsync(RESERVATIONS => RESERVATIONS.Id == id, updatedTrainSchedule);

        }

        // Delete Train Schedule
        public async Task DeleteTrainReservationAsync(string id)
        {
            _loggerInfo.LogInformation("Deleting User Manager Service using DeleteTrainReservationAsync()");
            await _reservationManagerCollection.DeleteOneAsync(RESERVATIONS => RESERVATIONS.Id == id);

        }
    }
}