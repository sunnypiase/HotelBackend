using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Entity;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : BaseController<Room, RoomDto>
    {
        public RoomController(IRepository<Room> repository) : base(repository)
        {
        }
    }
}
