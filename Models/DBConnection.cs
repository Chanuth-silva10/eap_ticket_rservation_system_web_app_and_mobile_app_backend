namespace TicketReservationManager.Models
{
    public class DBConnection
    {
        public string URI { get; set; } = null!;
        public string DBName { get; set; } = null!;
        public string AdminsCollection { get; set; } = null!;
        public string TravelarsCollection { get; set; } = null!;
        public string TrainsCollection { get; set; } = null!;
        public string TrainScheduleCollection { get; set; } = null!;
        public string ReservationCollection { get; set; } = null!;
    }
}
