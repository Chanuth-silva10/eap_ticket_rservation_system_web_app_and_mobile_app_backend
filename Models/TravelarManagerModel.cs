using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketReservationManager.Models
{

    public class TravelarManagerModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserName { get; set; } = null!;
        public string NIC { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}

