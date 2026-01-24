using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FormApp.Core.Entities;

namespace FormApp.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, Microsoft.AspNetCore.Identity.IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // New normalized entities
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<MeterScale> MeterScales { get; set; }
    public DbSet<SubscriptionViolation> SubscriptionViolations { get; set; }
    public DbSet<Transformer> Transformers { get; set; }
    public DbSet<SubscriptionBranch> SubscriptionBranches { get; set; }
    public DbSet<TransactionAttachment> TransactionAttachments { get; set; }
    public DbSet<TransactionRecord> TransactionRecords { get; set; }
    
    // Shared entities
    public DbSet<UploadedFile> UploadedFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        // UploadedFile configuration
        modelBuilder.Entity<UploadedFile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).HasMaxLength(255).IsRequired();
            entity.Property(e => e.OriginalFileName).HasMaxLength(255).IsRequired();
            entity.Property(e => e.FileExtension).HasMaxLength(10).IsRequired();
            entity.Property(e => e.FilePath).HasMaxLength(500).IsRequired();
            entity.Property(e => e.ContentType).HasMaxLength(100).IsRequired();
        });
        
        // TransactionRecord configuration (legacy flat table)
        modelBuilder.Entity<TransactionRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Latitude).HasPrecision(18, 8);
            entity.Property(e => e.Longitude).HasPrecision(18, 8);
            
            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // ===== New Entity Configurations =====
        
        // Transaction configuration
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Subscriber)
                .WithOne(s => s.Transaction)
                .HasForeignKey<Transaction>(e => e.SubscriberId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Subscription)
                .WithOne(s => s.Transaction)
                .HasForeignKey<Transaction>(e => e.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.MeterScale)
                .WithOne(m => m.Transaction)
                .HasForeignKey<Transaction>(e => e.MeterScaleId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Violation)
                .WithOne(v => v.Transaction)
                .HasForeignKey<Transaction>(e => e.ViolationId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Transformer)
                .WithOne(t => t.Transaction)
                .HasForeignKey<Transaction>(e => e.TransformerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasMany(e => e.Attachments)
                .WithOne(a => a.Transaction)
                .HasForeignKey(a => a.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Subscriber configuration
        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FacilityName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.SubscriptionNumber).HasMaxLength(50);
            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.Mobile1).HasMaxLength(20);
            entity.Property(e => e.Mobile2).HasMaxLength(20);
            entity.Property(e => e.NearestPoint).HasMaxLength(200);
            entity.Property(e => e.CommercialAccountName).HasMaxLength(200);
            entity.Property(e => e.FieldPersonName).HasMaxLength(100);
            entity.Property(e => e.FieldElectricCompanyCompanion).HasMaxLength(100);
            entity.Property(e => e.Latitude).HasPrecision(18, 8);
            entity.Property(e => e.Longitude).HasPrecision(18, 8);
            entity.Property(e => e.EstimatedNeighborhood).HasMaxLength(200);
            entity.Property(e => e.ActualNeighborhood).HasMaxLength(200);
            
            // Unique indexes
            entity.HasIndex(e => e.SubscriptionNumber).IsUnique().HasFilter("[SubscriptionNumber] IS NOT NULL AND [SubscriptionNumber] != ''");
            entity.HasIndex(e => e.AccountNumber).IsUnique().HasFilter("[AccountNumber] IS NOT NULL AND [AccountNumber] != ''");
        });
        
        // Subscription configuration
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SubscriberName).HasMaxLength(200);
        });
        
        // MeterScale configuration
        modelBuilder.Entity<MeterScale>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MeasurementNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ActualMeasurementNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ReadingNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.MultiplicationFactor).HasMaxLength(20).IsRequired();
        });
        
        // SubscriptionViolation configuration
        modelBuilder.Entity<SubscriptionViolation>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        
        // Transformer configuration
        modelBuilder.Entity<Transformer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TransformerCapacity).HasMaxLength(50);
            entity.Property(e => e.TransformerSerialNumber).HasMaxLength(100);
        });
        
        // SubscriptionBranch configuration
        modelBuilder.Entity<SubscriptionBranch>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Size).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(500);
            
            entity.HasOne(e => e.Transformer)
                .WithMany(t => t.Branches)
                .HasForeignKey(e => e.TransformerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // TransactionAttachment configuration
        modelBuilder.Entity<TransactionAttachment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.FileType).HasMaxLength(50);
            
            entity.HasOne(e => e.File)
                .WithMany()
                .HasForeignKey(e => e.FileId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
