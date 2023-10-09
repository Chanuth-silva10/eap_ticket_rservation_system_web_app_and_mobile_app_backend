using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketReservationManager.Models
{

    public class TrainModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string TrainName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}

