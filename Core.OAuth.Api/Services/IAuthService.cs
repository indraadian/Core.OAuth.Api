using Core.OAuth.Api.Models;

namespace Core.OAuth.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(AuthRequest request);
        Task<AuthResponse> Login(AuthRequest request);
    }
}
