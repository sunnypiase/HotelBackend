using WebAPI.Entity;

namespace WebAPI.Dto
{
    public interface IRequestDto<T>
        where T : class, IEntity
    {
        T ToEntity();
    }
}