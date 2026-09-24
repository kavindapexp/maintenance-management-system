using Microsoft.EntityFrameworkCore;
using MaintenanceManagement.Api.Models;

namespace MaintenanceManagement.Api.Data;

public class MaintenanceDbContext : DbContext
{
    public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Currency> Currencies { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Site> Sites { get; set; }

    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    public DbSet<AssetValue> AssetValues { get; set; }

    public DbSet<Depreciation> Depreciations { get; set; }

    public DbSet<AssetValuation> AssetValuations { get; set; }

    public DbSet<InflationIndex> InflationIndexes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(c => c.CountryId);

            entity.Property(c => c.CountryCode)
                .HasMaxLength(2)
                .IsRequired();

            entity.Property(c => c.CountryName)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(c => c.CountryCode)
                .IsUnique();

            entity.HasIndex(c => c.CountryName)
                .IsUnique();
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(c => c.CurrencyId);

            entity.Property(c => c.CurrencyCode)
                .HasMaxLength(3)
                .IsRequired();

            entity.Property(c => c.CurrencyName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.Symbol)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(c => c.DecimalPlaces)
                .IsRequired();

            entity.Property(c => c.IsDefault)
                .IsRequired();

            entity.Property(c => c.IsActive)
                .IsRequired();

            entity.HasIndex(c => c.CurrencyCode)
                .IsUnique();

            entity.HasIndex(c => c.IsDefault)
                .IsUnique()
                .HasFilter("\"IsDefault\" = true");
        });
        modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.CompanyId);

                entity.Property(c => c.CompanyCode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(c => c.CompanyName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(c => c.IsActive)
                    .IsRequired();

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(c => c.CompanyCode)
                    .IsUnique();

                entity.HasOne<Country>()
                    .WithMany()
                    .HasForeignKey(c => c.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(s => s.SiteId);

                entity.Property(s => s.SiteCode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(s => s.SiteName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(s => s.AddressLine1)
                    .HasMaxLength(250)
                    .IsRequired();

                entity.Property(s => s.AddressLine2)
                    .HasMaxLength(250);

                entity.Property(s => s.City)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(s => s.PostalCode)
                    .HasMaxLength(30);

                entity.Property(s => s.IsActive)
                    .IsRequired();

                entity.Property(s => s.CreatedAt)
                    .IsRequired();

                entity.Property(s => s.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(s => new { s.CompanyId, s.SiteCode })
                    .IsUnique();

                entity.HasOne<Company>()
                    .WithMany()
                    .HasForeignKey(s => s.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Country>()
                    .WithMany()
                    .HasForeignKey(s => s.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.EquipmentId);

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Description);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(100);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(150);

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(100);

                entity.Property(e => e.EquipmentStatus)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.AcquisitionDate);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(e => new { e.SiteId, e.EquipmentCode })
                    .IsUnique();

                entity.HasIndex(e => e.SerialNumber)
                    .IsUnique();

                entity.HasOne<Site>()
                    .WithMany()
                    .HasForeignKey(e => e.SiteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasKey(m => m.MaintenanceRequestId);

                entity.Property(m => m.RequestNumber)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(m => m.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(m => m.Description)
                    .IsRequired();

                entity.Property(m => m.Priority)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(m => m.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(m => m.RequestedAt)
                    .IsRequired();

                entity.Property(m => m.CompletedAt);

                entity.Property(m => m.CreatedAt)
                    .IsRequired();

                entity.Property(m => m.UpdatedAt)
                    .IsRequired();

                entity.HasIndex(m => m.RequestNumber)
                    .IsUnique();

                entity.HasIndex(m => new { m.EquipmentId, m.Status });

                entity.HasOne<Equipment>()
                    .WithMany()
                    .HasForeignKey(m => m.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<AssetValue>(entity =>
            {
                entity.HasKey(a => a.AssetValueId);

                entity.HasIndex(a => a.EquipmentId)
                    .IsUnique();

                entity.Property(a => a.AcquisitionCost)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(a => a.AcquisitionDate)
                    .IsRequired();

                entity.Property(a => a.CreatedAt)
                    .IsRequired();

                entity.HasOne<Equipment>()
                    .WithMany()
                    .HasForeignKey(a => a.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Currency>()
                    .WithMany()
                    .HasForeignKey(a => a.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<Depreciation>(entity =>
            {
                entity.HasKey(d => d.DepreciationId);

                entity.Property(d => d.DepreciationDate)
                    .IsRequired();

                entity.Property(d => d.DepreciationAmount)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(d => d.AccumulatedAmount)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(d => d.CurrencyId)
                    .IsRequired();

                entity.Property(d => d.Method)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(d => d.CreatedAt)
                    .IsRequired();

                entity.HasIndex(d => new { d.EquipmentId, d.DepreciationDate });

                entity.HasOne<Equipment>()
                    .WithMany()
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Currency>()
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<AssetValuation>(entity =>
            {
                entity.HasKey(a => a.AssetValuationId);

                entity.Property(a => a.ValuationDate)
                    .IsRequired();

                entity.Property(a => a.ValuationAmount)
                    .HasPrecision(19, 4)
                    .IsRequired();

                entity.Property(a => a.ValuationType)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(a => a.Notes);

                entity.Property(a => a.CreatedAt)
                    .IsRequired();

                entity.HasIndex(a => new { a.EquipmentId, a.ValuationDate });

                entity.HasOne<Equipment>()
                    .WithMany()
                    .HasForeignKey(a => a.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Currency>()
                    .WithMany()
                    .HasForeignKey(a => a.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        modelBuilder.Entity<InflationIndex>(entity =>
            {
                entity.HasKey(i => i.InflationIndexId);
            
                entity.Property(i => i.PeriodStart)
                    .IsRequired();
            
                entity.Property(i => i.PeriodEnd)
                    .IsRequired();
            
                entity.Property(i => i.IndexValue)
                    .HasPrecision(19, 6)
                    .IsRequired();
            
                entity.Property(i => i.Source)
                    .HasMaxLength(200);
            
                entity.Property(i => i.CreatedAt)
                    .IsRequired();
            
                entity.HasIndex(i => new { i.CountryId, i.PeriodStart, i.PeriodEnd })
                    .IsUnique();
            
                entity.HasOne<Country>()
                    .WithMany()
                    .HasForeignKey(i => i.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_InflationIndexes_PeriodRange",
                            "\"PeriodEnd\" >= \"PeriodStart\""));
            
            
            });

    }
}