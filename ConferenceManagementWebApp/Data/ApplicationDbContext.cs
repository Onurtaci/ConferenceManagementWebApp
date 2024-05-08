using ConferenceManagementWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagementWebApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conference>(entity =>
        {

            entity.ToTable("Conferences");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Venue).IsRequired().HasMaxLength(50);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.HasOne(e => e.Organizer);

            entity.HasMany(e => e.Sessions);
            entity.HasMany(e => e.Feedbacks);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Sessions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Topic).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PresentationType).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Presenter);

            entity.HasOne(e => e.Conference).WithMany(e => e.Sessions).OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(e => e.Papers);
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.ToTable("Papers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Abstract).HasMaxLength(50);
            entity.Property(e => e.Keywords).HasMaxLength(50);
            entity.Property(e => e.FileBytes).IsRequired();
            entity.HasOne(e => e.Author);
            entity.HasOne(e => e.Session).WithMany(e => e.Papers).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Review);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedbacks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.HasOne(e => e.Attendee);
            entity.HasOne(e => e.Conference).WithMany(e => e.Feedbacks).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Score);
            entity.Property(e => e.Recommendation).HasMaxLength(50);
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.HasOne(e => e.Reviewer);
            entity.HasOne(e => e.Paper).WithOne(e => e.Review).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.CreationDate).IsRequired();

        });

        modelBuilder.Entity<ConferenceAttendee>(entity =>
        {
            entity.ToTable("ConferenceAttendees");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.HasOne(e => e.Conference).WithMany(e => e.ConferenceAttendees).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Attendee);
        });

        modelBuilder.Entity<ConferenceReviewer>(entity =>
        {
            entity.ToTable("ConferenceReviewers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.HasOne(e => e.Conference).WithMany(e => e.ConferenceReviewers).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Reviewer);
        });
    }

    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Paper> Papers { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ConferenceAttendee> ConferenceAttendees { get; set; }
    public DbSet<ConferenceReviewer> ConferenceReviewers { get; set; }
}
