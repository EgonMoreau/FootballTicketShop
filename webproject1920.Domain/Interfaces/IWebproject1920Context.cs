using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Domain.Interfaces
{
    public interface IWebproject1920Context : IDisposable
    {
        DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        DbSet<AspNetRoles> AspNetRoles { get; set; }
        DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        DbSet<AspNetUsers> AspNetUsers { get; set; }
        DbSet<Competitions> Competitions { get; set; }
        DbSet<Locations> Locations { get; set; }
        DbSet<Match> Match { get; set; }
        DbSet<MatchStadiumLocation> MatchStadiumLocation { get; set; }
        DbSet<TeamCompetitionLocation> TeamCompetitionLocation { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderLine> OrderLine { get; set; }
        DbSet<StadiumLocations> StadiumLocations { get; set; }
        DbSet<Stadiums> Stadiums { get; set; }
        DbSet<Subscription> Subscription { get; set; }
        DbSet<Teams> Teams { get; set; }
        DbSet<Ticket> Ticket { get; set; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;

        DatabaseFacade Database { get; }
    }
}
