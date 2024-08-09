using WebAppForDocker.DTOs;
using WebAppForDocker.Models;

namespace WebAppForDocker.Services.Abstractions
{
    public interface IUserService
    {
        int AddUser(UserDto user);
        RoleId CheckUser(LoginDto login);
    }
}
