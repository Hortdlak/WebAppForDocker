using WebAppForDocker.DTOs;
using WebAppForDocker.Models;

namespace WebAppForDocker.Repositories.Abstraction
{
    public interface IUserReadRepository
    {
        RoleId CheckUser(LoginDto login);
    }
}
