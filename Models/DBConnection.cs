namespace TicketReservationManager.Models
{
    public class DBConnection
    {
        public string URI { get; set; } = null!;
        public string DBName { get; set; } = null!;
        public string AdminsCollection { get; set; } = null!;
    }
}
