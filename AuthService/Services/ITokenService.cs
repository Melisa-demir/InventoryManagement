using AuthService.Entities;

namespace AuthService.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
