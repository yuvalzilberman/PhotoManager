using Microsoft.EntityFrameworkCore;
using PhotoManager.Data.Models;

namespace PhotoManager.Data
{
    public class PhotoDbContext : DbContext
    {
        public PhotoDbContext(DbContextOptions<PhotoDbContext> options)
        : base(options) { }

        public DbSet<Photo> Photos { get; set; }
    }
}
