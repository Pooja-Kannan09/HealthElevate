using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserService.Data;
using UserService.Exceptions;
using UserService.Models;
using UserService.Services;

namespace userServiceTestes
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AppDbContext _dbContext;

        public AuthServiceTests()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                          .UseInMemoryDatabase(databaseName: "TestDatabase")
                          .Options;
            _dbContext = new AppDbContext(options);

            // Mock IConfiguration for JWT settings
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("SuperSecretKey");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            // Initialize AuthService with the mocked dependencies
            _authService = new AuthService(_dbContext, _mockConfig.Object);
        }

        [Fact]
        public void RegisterUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = new User { Username = "existingUser", Email = "existing@example.com", Password = "password" };
            _dbContext.Users.Add(existingUser);
            _dbContext.SaveChanges();

            var newUser = new User { Username = "existingUser", Email = "newuser@example.com", Password = "password" };

            // Act & Assert
            var ex = Assert.Throws<UserAlreadyExistsException>(() => _authService.RegisterUser(newUser));
            Assert.Equal("User already exists", ex.Message);
        }

        [Fact]
        public void RegisterUser_ShouldAddNewUser_WhenUserDoesNotExist()
        {
            // Arrange
            var newUser = new User { Username = "newUser", Email = "newuser@example.com", Password = "password" };

            // Act
            _authService.RegisterUser(newUser);

            // Assert
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == "newUser");
            Assert.NotNull(user);
            Assert.Equal("newuser@example.com", user.Email);
        }

        [Fact]
        public void Authenticate_ShouldReturnToken_WhenValidCredentialsProvided()
        {
            // Arrange
            var username = "validUser";
            var password = "validPassword";
            var email = "validUser@example.com";  // Required email property

            // Setting up the in-memory database options
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserAuthDb")
                .Options;

            // Using the in-memory database for testing
            using (var context = new AppDbContext(options))
            {
                // Adding a test user with required properties to the in-memory database
                context.Users.Add(new User
                {
                    Username = username,
                    Password = password,
                    Email = email  // Ensure required Email property is set
                });
                context.SaveChanges();

                // Mocking configuration values for JWT
                var configMock = new Mock<IConfiguration>();
                configMock.Setup(c => c["Jwt:Key"]).Returns("YourVeryStrongSecretKeyForTesting123456789");
                configMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
                configMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

                // Creating the AuthService instance with the in-memory context and mock configuration
                var authService = new AuthService(context, configMock.Object);

                // Act
                var token = authService.Authenticate(username, password);

                // Assert
                Assert.NotNull(token);
                Assert.IsType<string>(token);
            }
        }

        [Fact]
        public void Authenticate_ShouldThrowInvalidCredentialsException_WhenInvalidCredentialsProvided()
        {
            // Arrange
            var username = "invalidUser";
            var password = "invalidPassword";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _authService.Authenticate(username, password));

            // Check if the inner exception is of type InvalidCredentialsException
            Assert.IsType<InvalidCredentialsException>(exception.InnerException);
        }
    }
}