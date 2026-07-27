using AuthService.DTOs;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
