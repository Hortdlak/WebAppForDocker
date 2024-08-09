using WebAppForDocker.DTOs;

namespace WebAppForDocker.Repositories.Abstraction
{
    public interface IUserWriteRepository
    {
        int AddUser(UserDto user);
    }
}
