using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dto;
using WebAPI.Entity;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : BaseController<Booking, BookingDto>
    {

        public BookingController(IRepository<Booking> repository) : base(repository)
        {
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            var entities = _repository.Query()
                .Include(b => b.User)
                .Include(b => b.Room)
                .ToList();

            return Ok(entities);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.Query()
                .Include(b => b.User)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet("room/{roomId}/year/{year}/month/{month}")]
        public IActionResult GetBookingsForRoom(int roomId, int year, int month)
        {
            var bookings = _repository.Query()
                .Include(b => b.User)
                .Include(b => b.Room)
                .Where(b => b.Room.Id == roomId && b.Date.Year == year && b.Date.Month == month)
                .ToList();

            if (!bookings.Any())
            {
                return NotFound();
            }

            return Ok(bookings);
        }
        [HttpPost("BatchCreate")]
        public async Task<IActionResult> BatchCreate([FromBody] BatchBookingDto batchBooking)
        {
            var startDate = batchBooking.StartDate;
            var endDate = batchBooking.EndDate;
            var totalDays = (endDate - startDate).Days + 1;

            var bookings = Enumerable.Range(0, totalDays)
                                     .Select(offset => new Booking
                                     {
                                         Date = startDate.AddDays(offset).ToUniversalTime(),
                                         UserId = batchBooking.UserId,
                                         RoomId = batchBooking.RoomId
                                     })
                                     .ToList();

            var result = await _repository.BatchAddAsync(bookings);
            return Ok(result);
        }
    }
}
