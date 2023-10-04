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
        public string TrainNo { get; set; } = null!;
        public string AvailableClasses { get; set; } = null!;
        public string StartStation { get; set; } = null!;
        public string EndStation { get; set; } = null!;
        public string ArrivalTime { get; set; } = null!;
        public string DepartureTime { get; set; } = null!;
        public bool Active { get; set; } = false!;
        public bool Published { get; set; } = false!;
    }
}

