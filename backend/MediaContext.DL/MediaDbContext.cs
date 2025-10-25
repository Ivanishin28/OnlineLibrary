using MediaContext.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaContext.DL
{
    public class MediaDbContext : DbContext
    {
        public DbSet<MediaFile> MediaFiles { get; set; }

        public MediaDbContext(DbContextOptions<MediaDbContext> options) : base(options)
        {
        }
    }
}
