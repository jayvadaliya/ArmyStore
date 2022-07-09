using System.Security.Cryptography;
using ArmyStore.Dtos;
using ArmyStore.Entities;
using ArmyStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArmyStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User(request.UserName, passwordHash, passwordSalt);
            await _userRepository.Create(user, false);
            return Accepted();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] RegisterUserDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                return BadRequest("Please provide UserName.");

            var user = await _userRepository.GetById(1, false);
            if (user == null)
                return NotFound("User not found.");

            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong password!");

            return Ok("Login successfull!!");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedPasswordHash.SequenceEqual(passwordHash);
            }
        }
    }
}