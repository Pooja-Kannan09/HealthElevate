using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Data;
using UserService.Exceptions;
using UserService.Models;

namespace UserService.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Function to register a new user with custom exception handling
        public void RegisterUser(User user)
        {
            try
            {
                // Check if the user already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username || u.Email == user.Email);
                if (existingUser != null)
                {
                    throw new UserAlreadyExistsException("User already exists");
                }

                // Hashing the password can be done here if needed
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the user to the database", ex);
            }
        }

        // Function to authenticate the user and return a JWT token
        public string Authenticate(string username, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    throw new InvalidCredentialsException("Invalid username or password");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"],
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username), new Claim("userId", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during authentication", ex);
            }
        }
    }
}
