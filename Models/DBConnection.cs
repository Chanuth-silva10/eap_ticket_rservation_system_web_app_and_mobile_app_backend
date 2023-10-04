namespace TicketReservationManager.Models
{
    public class DBConnection
    {
        public string URI { get; set; } = null!;
        public string DBName { get; set; } = null!;
        public string UsersCollection { get; set; } = null!;
        public string TrainsCollection { get; set; } = null!;
    }
}
