using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Data
{
    public class CodingTaskContext : DbContext
    {
        public CodingTaskContext(DbContextOptions<CodingTaskContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchParticipant> MatchParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<User>().Property(t => t.UserName).HasMaxLength(250);
            modelBuilder.Entity<User>().Property(t => t.Password).HasMaxLength(250);
            
            modelBuilder.Entity<Match>().ToTable(nameof(Match));
            modelBuilder.Entity<Match>().Property(t => t.MatchName).HasMaxLength(250);

            modelBuilder.Entity<MatchParticipant>().ToTable(nameof(MatchParticipant));
        }
    }
}
