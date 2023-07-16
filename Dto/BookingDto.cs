using WebAPI.Entity;

namespace WebAPI.Dto
{
    public class BookingDto : IRequestDto<Booking>
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public Booking ToEntity()
        {
            return new Booking()
            {
                Date = Date,
                UserId = UserId,
                RoomId = RoomId
            };
        }
    }
}
