using Microsoft.EntityFrameworkCore;
using MyProject.Model;

namespace MyProject.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<MyEntity> MyEntities { get; set; }
    }
}
