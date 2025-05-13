namespace Core.OAuth.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Userame { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
