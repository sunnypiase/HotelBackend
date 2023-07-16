using WebAPI.Entity;

namespace WebAPI.Dto
{
    public class UserDto : IRequestDto<User>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public User ToEntity() => new()
        {
            Name = Name,
            PhoneNumber = PhoneNumber,
            Email = Email
        };

    }
}
