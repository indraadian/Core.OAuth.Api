using Core.OAuth.Api.Database;
using Core.OAuth.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Core.OAuth.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var result = new AuthResponse
            {
                Success = true,
            };
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Userame == request.Username);
            if (user == null)
            {
                result.Success = false;
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                result.Success = false;
            }

            if (result.Success)
            {
                result.Token = _jwtService.GenerateToken(user.Id, user.Userame);
            }

            return result;
        }

        public async Task<AuthResponse> Register(AuthRequest request)
        {
            var result = new AuthResponse
            {
                Success = true
            };
            var user = new User
            {
                Id = Guid.NewGuid(),
                Userame = request.Username,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            _context.Users.Add(user);
            var saveResult = await _context.SaveChangesAsync();
            if (saveResult <= 0)
            {
                result.Success = false;
            }

            return await this.Login(request);
        }
    }
}
