using WebAPI.Entity;

namespace WebAPI.Dto
{
    public class RoomDto : IRequestDto<Room>
    {
        public Room ToEntity() => new()
        {
        };
    }
}
