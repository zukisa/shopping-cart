using Api.Models;
using Core.Dto;
using Core.Interfaces.Gateways.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="jwtFactory"></param>
        public AccountController(
            IUserRepository userRepository,
            IJwtFactory jwtFactory
            )
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Post(RegisterUserRequest request)
        {
            if (_userRepository.ExistsByEmail(request.Email))
            {
                return BadRequest($"Email Address:{request.Email} has already been taken!!!");
            }
            Core.Entities.User user = _userRepository.CreateUser(new Core.Entities.User(request.Email, request.Password));
            return Ok(user);
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Post(LoginRequest request)
        {
            Core.Entities.User user = _userRepository.FindUserByEmail(request.Email);
            if (user == null) return BadRequest("Invalid username or password");
            if(user != null && !_userRepository.CheckPassword(user, request.Password))
            {
                return BadRequest("Invalid username or password");
            }

            Token token = _jwtFactory.GenerateEncodedToken(user.Id);

            return Ok(token);
        }
    }
}
