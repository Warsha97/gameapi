using Microsoft.EntityFrameworkCore;
using MyTestDotnetApp.Entities;

namespace MyTestDotnetApp.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) 
        : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();
    }
}
