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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=HalisahaDB;User ID=sa;Password=Tatyuz51+;Encrypt=true;TrustServerCertificate=true;");
        //optionsBuilder.UseSqlServer("Server=tcp:halisahadeneme-server.database.windows.net,1433;Initial Catalog=halisahadeneme-database;Persist Security Info=False;User ID=halisahadeneme-server-admin;Password=51053N1NK7VQ474H$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<ReservedSession>().HasKey(a => new { a.PlayerId, a.SessionId });
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
    }
}