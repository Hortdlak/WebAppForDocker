namespace WebAppForDocker.DTOs
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public UserRoleDto Role { get; set; }
    }
}
