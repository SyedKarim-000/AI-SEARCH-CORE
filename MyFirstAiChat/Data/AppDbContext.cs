using Microsoft.EntityFrameworkCore;
using MyFirstAiChat.ChatModel;

namespace MyFirstAiChat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}