using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketReservationManager.Models
{

    public class ReservationsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserId { get; set; } = null!;
        public string TrainId { get; set; } = null!;
        public DateTime ReservationDate { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; } = null!;
    }
}

