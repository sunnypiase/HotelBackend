namespace WebAPI.Dto
{
    public class BatchBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }

}
