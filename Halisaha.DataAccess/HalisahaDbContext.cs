using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess;

public class HalisahaDbContext : DbContext
{
    //burada eklemiş olduğumuz dbset'ler sayesinde database tabloları oluşturulabiliyor ve kullanılabiliyor

    public DbSet<Player> Players { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<ReservedSession> ReservedSessions { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<PlayerTeam> PlayerTeams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlServer("Server=tcp:halisahadeneme-server.database.windows.net,1433;Initial Catalog=halisahadeneme-database;Persist Security Info=False;User ID=halisahadeneme-server-admin;Password=51053N1NK7VQ474H$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // modelBuilder.Entity<PlayerTeam>().HasKey(a => new { a.PlayerId, a.TeamId });
        modelBuilder.Entity<Owner>()
        .Property(e => e.CreateDate)
        .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<Player>()
        .Property(e => e.CreateDate)
        .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<ReservedSession>()
        .Property(e => e.CreateDate)
        .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<Session>()
        .Property(e => e.CreateDate)
        .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<PlayerTeam>()
            .HasKey(pt => new { pt.PlayerId, pt.TeamId });


        modelBuilder.Entity<ReservedSession>()
                .HasOne(rs => rs.EvSahibiTakim)
                .WithMany(t => t.ReservedSessions)
                .HasForeignKey(rs => rs.EvSahibiTakimId)
                .OnDelete(DeleteBehavior.Restrict);;

        modelBuilder.Entity<ReservedSession>()
                .HasOne(rs => rs.DeplasmanTakim)
                .WithMany()
                .HasForeignKey(rs => rs.DeplasmanTakimId)
                .OnDelete(DeleteBehavior.Restrict);;

        // modelBuilder.Entity<Team>()
        //     .Ignore(t => t.ReservedSessions);
        // modelBuilder.Entity<ReservedSession>()
        // .HasOne(r => r.EvSahibiTakım)
        // .WithMany(t => t.EvSahibiRezervasyonlar)
        // .HasForeignKey(r => r.EvSahibiTakımID)
        // .OnDelete(DeleteBehavior.Restrict);

        // modelBuilder.Entity<Rezervasyon>()
        //     .HasOne(r => r.MisafirTakım)
        //     .WithMany(t => t.MisafirRezervasyonlar)
        //     .HasForeignKey(r => r.MisafirTakımID)
        //     .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
