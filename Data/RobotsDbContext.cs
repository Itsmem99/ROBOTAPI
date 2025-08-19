using Microsoft.EntityFrameworkCore;

namespace RobotAPI.Data
{
    public class RobotsDbContext : DbContext
    {
        public RobotsDbContext(DbContextOptions<RobotsDbContext> options) : base(options) { }

        public DbSet<MoodEntry> MoodEntries { get; set; }
    }
}
