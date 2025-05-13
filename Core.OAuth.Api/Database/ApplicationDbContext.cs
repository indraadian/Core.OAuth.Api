using Core.OAuth.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.OAuth.Api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}
