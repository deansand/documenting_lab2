using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.DAL.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Apartment> Apartments => Set<Apartment>();
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.ContactInfo).HasMaxLength(300).IsRequired();
            entity.HasIndex(x => new { x.Name, x.ContactInfo }).IsUnique();
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Address).HasMaxLength(300).IsRequired();
            entity.Property(x => x.Price).HasColumnType("decimal(18,2)");
            entity.HasIndex(x => new { x.Address, x.OwnerId }).IsUnique();

            entity
                .HasOne(x => x.Owner)
                .WithMany(x => x.Apartments)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Url).HasMaxLength(500).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasIndex(x => new { x.ApartmentId, x.Url, x.Description }).IsUnique();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Text).HasMaxLength(2000).IsRequired();
            entity.HasIndex(x => new { x.ApartmentId, x.Name, x.Text }).IsUnique();
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.ApartmentId, x.Score }).IsUnique();
            entity.ToTable(x => x.HasCheckConstraint("CK_Rating_Score", "Score >= 1 AND Score <= 5"));
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Status).HasMaxLength(100).IsRequired();
            entity.HasIndex(x => new { x.ApartmentId, x.StartDate, x.EndDate, x.Status }).IsUnique();
        });
    }
}
