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
            entity.Property(e => e.OrganizerId).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Venue).IsRequired().HasMaxLength(50);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.HasOne(e => e.Organizer);
            entity.HasMany(e => e.Sessions);
            entity.HasMany(e => e.Feedbacks);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Sessions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.PresenterId).IsRequired();
            entity.Property(e => e.ConferenceId).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Topic).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PresentationType).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Presenter);

            entity.HasOne(e => e.Conference)
                .WithMany()
                .HasForeignKey(e => e.ConferenceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Paper);
        });

        modelBuilder.Entity<UserConference>(entity =>
        {
            entity.ToTable("UserConferences");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.AttendeeId).IsRequired();
            entity.Property(e => e.ConferenceId).IsRequired();
            entity.HasOne(e => e.Attendee);

            entity.HasOne(e => e.Conference)
              .WithMany()
              .HasForeignKey(e => e.ConferenceId)
              .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedbacks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.AttendeeId).IsRequired();
            entity.Property(e => e.ConferenceId).IsRequired();
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.HasOne(e => e.Attendee);

            entity.HasOne(e => e.Conference)
              .WithMany()
              .HasForeignKey(e => e.ConferenceId)
              .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.ToTable("Papers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.AuthorId).IsRequired();
            entity.Property(e => e.SessionId).IsRequired();
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Abstract).HasMaxLength(500);
            entity.HasOne(e => e.Author);

            entity.HasOne(e => e.Session)
              .WithMany()
              .HasForeignKey(e => e.SessionId)
              .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.ReviewerId).IsRequired();
            entity.Property(e => e.PaperId).IsRequired();
            entity.Property(e => e.Score).IsRequired();
            entity.Property(e => e.Recommendation).IsRequired();
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.HasOne(e => e.Reviewer);

            entity.HasOne(e => e.Paper)
              .WithMany()
              .HasForeignKey(e => e.PaperId)
              .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
