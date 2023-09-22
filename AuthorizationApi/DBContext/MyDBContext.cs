using Microsoft.EntityFrameworkCore;
using AuthorizationApi.Models;

namespace AuthorizationApi.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
