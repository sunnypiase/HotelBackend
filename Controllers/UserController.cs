using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Entity;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<User, UserDto>
    {
        public UserController(IRepository<User> repository) : base(repository)
        {
        }
    }
}
