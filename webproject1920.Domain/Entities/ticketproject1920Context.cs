using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using webproject1920.Domain.Interfaces;

namespace webproject1920.Domain.Entities
{
    public partial class ticketproject1920Context : DbContext, IWebproject1920Context
    {
        public ticketproject1920Context()
        {
        }

        public ticketproject1920Context(DbContextOptions<ticketproject1920Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Competitions> Competitions { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchStadiumLocation> MatchStadiumLocation { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderLine> OrderLine { get; set; }
        public virtual DbSet<StadiumLocations> StadiumLocations { get; set; }
        public virtual DbSet<Stadiums> Stadiums { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<TeamCompetitionLocation> TeamCompetitionLocation { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Competitions>(entity =>
            {
                entity.ToTable("Competitions", "db_owner");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Season)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.ToTable("Locations", "db_owner");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match", "db_owner");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatch375604");

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatch16313");

                entity.HasOne(d => d.TeamAwayNavigation)
                    .WithMany(p => p.MatchTeamAwayNavigation)
                    .HasForeignKey(d => d.TeamAway)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatch685544");

                entity.HasOne(d => d.TeamHomeNavigation)
                    .WithMany(p => p.MatchTeamHomeNavigation)
                    .HasForeignKey(d => d.TeamHome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatch484343");
            });

            modelBuilder.Entity<MatchStadiumLocation>(entity =>
            {
                entity.ToTable("MatchStadiumLocation", "db_owner");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.MatchStadiumLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MatchStadiumLocation_FK");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchStadiumLocation)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatchStadi641239");

                entity.HasOne(d => d.StadiumLocation)
                    .WithMany(p => p.MatchStadiumLocation)
                    .HasForeignKey(d => d.StadiumLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMatchStadi952846");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "db_owner");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.ToTable("OrderLine", "db_owner");

                entity.Property(e => e.ReturnedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrderLine150902");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FKOrderLine495538");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FKOrderLine71984");
            });

            modelBuilder.Entity<StadiumLocations>(entity =>
            {
                entity.ToTable("StadiumLocations", "db_owner");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.StadiumLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKStadiumLoc241621");

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.StadiumLocations)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKStadiumLoc26983");
            });

            modelBuilder.Entity<Stadiums>(entity =>
            {
                entity.ToTable("Stadiums", "db_owner");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.GeoLat).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.GeoLong).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription", "db_owner");

                entity.HasIndex(e => e.Code)
                    .HasName("Subscription_Code")
                    .IsUnique();

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.TeamCompetitionLocation)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.TeamCompetitionLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubscripti477304");
            });

            modelBuilder.Entity<TeamCompetitionLocation>(entity =>
            {
                entity.ToTable("TeamCompetitionLocation", "db_owner");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.TeamCompetitionLocation)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTeamCompet234309");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TeamCompetitionLocation)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTeamCompet89656");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCompetitionLocation)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTeamCompet189864");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("Teams", "db_owner");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Logo).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTeams582192");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket", "db_owner");

                entity.HasIndex(e => e.Code)
                    .HasName("Ticket_Code")
                    .IsUnique();

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Valid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_FK");

                entity.HasOne(d => d.MatchStadiumLocation)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.MatchStadiumLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTicket837131");
            });
        }
    }
}
