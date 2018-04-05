using Microsoft.EntityFrameworkCore;

namespace TechTalksAPI.Models
{
    public class TechTalksDBContext : DbContext
    {
        public TechTalksDBContext(DbContextOptions<TechTalksDBContext> options)
            : base(options)
        {
        }

        public DbSet<KeyValue> KeyValue { get; set; }
        public DbSet<TechTalk> TechTalk { get; set; }

        public DbSet<Categories> Categories { get; set; }
    }
}
