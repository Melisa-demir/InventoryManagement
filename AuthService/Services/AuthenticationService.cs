using AuthService.Data;
using AuthService.DTOs;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class AuthenticationService
        : IAuthenticationService
    {
        private readonly AuthDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            AuthDbContext context,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(
            RegisterRequest request)
        {
            var usernameExists =
                await _context.Users.AnyAsync(user =>
                    user.UserName == request.UserName);

            if (usernameExists)
            {
                return false;
            }

            var user = new User
            {
                UserName = request.UserName.Trim(),

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        request.Password),

                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthResponse?> LoginAsync(
            LoginRequest request)
        {
            var user =
                await _context.Users.FirstOrDefaultAsync(
                    user =>
                        user.UserName == request.UserName);

            if (user is null)
            {
                return null;
            }

            var passwordIsValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!passwordIsValid)
            {
                return null;
            }

            return CreateAuthResponse(user);
        }

        private AuthResponse CreateAuthResponse(User user)
        {
            var expireMinutes =
                _configuration.GetValue<int>(
                    "Jwt:ExpireMinutes");

            return new AuthResponse
            {
                Token = _tokenService.CreateToken(user),

                Expiration =
                    DateTime.UtcNow.AddMinutes(
                        expireMinutes),

                UserName = user.UserName,
                Role = user.Role
            };
        }
    }
}