using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TicketRservationManager.Models
{

    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
    }
}

