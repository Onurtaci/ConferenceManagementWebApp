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
            entity.HasOne(e => e.Organizer).WithMany().IsRequired();
            entity.HasMany(e => e.Sessions).WithOne().IsRequired();
            entity.HasMany(e => e.Feedbacks).WithOne().IsRequired();
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
            entity.HasOne(e => e.Presenter).WithMany().IsRequired();
            entity.HasOne(e => e.Conference).WithMany().IsRequired();
            entity.HasMany(e => e.Paper).WithOne();
        });

        modelBuilder.Entity<UserConference>(entity =>
        {
            entity.ToTable("UserConferences");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.AttendeeId).IsRequired();
            entity.Property(e => e.ConferenceId).IsRequired();
            entity.HasOne(e => e.Attendee).WithMany().IsRequired();
            entity.HasOne(e => e.Conference).WithMany().IsRequired();
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedbacks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.ConferenceId).IsRequired();
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.HasOne(e => e.Attendee).WithMany().IsRequired();
            entity.HasOne(e => e.Conference).WithMany().IsRequired();
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
            entity.HasOne(e => e.Author).WithMany().IsRequired();
            entity.HasOne(e => e.Session).WithMany().IsRequired();
        });
    }
}
