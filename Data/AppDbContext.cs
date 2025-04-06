using Backend.Entities;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
