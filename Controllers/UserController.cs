using Microsoft.AspNetCore.Mvc;
using WebAppForDocker.DTOs;
using WebAppForDocker.Models;
using WebAppForDocker.Services.Abstractions;

namespace WebAppForDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpPost("add_user")]
        public ActionResult<int> AddUser(UserDto user)
        {
            try
            {
                return Ok(_service.AddUser(user));
            }
            catch (Exception ex) 
            {
                return StatusCode(409, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<RoleId> CheckUser(LoginDto login)
        {
            try
            {
                return Ok(_service.CheckUser(login));
            }
            catch (Exception ex)
            {
                return StatusCode(409, ex.Message);
            }
        }
    }
}
