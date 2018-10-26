namespace TeamPlayerRecords.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamPlayer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<TeamPlayer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.TeamName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.CoachName)
                .IsUnicode(false);
        }
    }
}
