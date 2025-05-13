namespace Core.OAuth.Api.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username);
    }
}
