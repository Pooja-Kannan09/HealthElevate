using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Exceptions;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = new User { Username = request.Username, Email = request.Email, Password = request.Password };
                _authService.RegisterUser(user);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred during registration", Details = ex.Message });
            }
        }
       
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _authService.Authenticate(request.Username, request.Password);
                if (token == null)
                {
                    return Unauthorized(new { Error = "Invalid username or password" });
                }

                return Ok(new { Token = token });
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred during login", Details = ex.Message });
            }
        }
    }
}
