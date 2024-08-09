using WebAppForDocker.DTOs;
using WebAppForDocker.Models;
using WebAppForDocker.Repositories.Abstraction;
using WebAppForDocker.Services.Abstractions;

namespace WebAppForDocker.Services
{
    public class UserService(IUserReadRepository readRepository, IUserWriteRepository writeRepository) : IUserService
    {
        private readonly IUserReadRepository _readRepository = readRepository;
        private readonly IUserWriteRepository _writeRepository = writeRepository;

        public int AddUser(UserDto user)
        {
            return _writeRepository.AddUser(user);
        }

        public RoleId CheckUser(LoginDto login)
        {
            return _readRepository.CheckUser(login);
        }
    }
}
