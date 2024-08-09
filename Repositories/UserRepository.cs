using System.Text;
using WebAppForDocker.DB;
using WebAppForDocker.DTOs;
using WebAppForDocker.Models;
using System.Security.Cryptography;
using WebAppForDocker.Repositories.Abstraction;

namespace WebAppForDocker.Repositories
{
    public class UserRepository : IUserWriteRepository, IUserReadRepository
    {
        public int AddUser(UserDto user)
        {
            using (var context = new UserContext())
            {
                if (context.Users.Any(x => x.Name == user.Name))
                    throw new Exception("User is already exist!");
                if (user.Role == UserRoleDto.Admin)
                    if (context.Users.Any(x => x.RoleId == RoleId.Admin))
                        throw new Exception("Admin is already exist!");

                var entity = new User
                {
                    Name = user.Name,
                    RoleId = (RoleId)user.Role,
                    Salt = new byte[16]
                };

                new Random().NextBytes(entity.Salt);
                var data = Encoding.UTF8.GetBytes(user.Password).Concat(entity.Salt).ToArray();

                entity.Password = SHA512.HashData(data);

                context.Users.Add(entity);
                context.SaveChanges();

                return entity.Id;
            }
        }

        public RoleId CheckUser(LoginDto login)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Name == login.Name)
                    ?? throw new Exception("No user like this!");

                var data = Encoding.UTF8.GetBytes(login.Password).Concat(user.Salt).ToArray();
                var hash = SHA512.HashData(data);

                if (user.Password.SequenceEqual(hash))
                    return user.RoleId;

                throw new Exception("Wrong password!");
            }
        }
    }
}
