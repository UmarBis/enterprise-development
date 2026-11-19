using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessClub.Infrastructure.SqlServer.Migrations;

[DbContext(typeof(FitnessClubDbContext))]
public sealed class FitnessClubDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        modelBuilder.Entity<Person>().UseTpcMappingStrategy();

        modelBuilder.Entity<Client>(b =>
        {
            b.ToTable("Clients");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Id).HasColumnType("uniqueidentifier");
            b.Property(x => x.PassportNumber).HasMaxLength(20).IsRequired();
            b.HasIndex(x => x.PassportNumber).IsUnique();
            b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Phone).HasMaxLength(30).IsRequired();
            b.Property(x => x.Gender).HasConversion<int>();
            b.Property(x => x.BirthDate).HasColumnType("date");
            b.Property(x => x.MembershipStartDate).HasColumnType("date");
            b.Property(x => x.MembershipEndDate).HasColumnType("date");
            b.Ignore(x => x.FullName);
        });

        modelBuilder.Entity<Trainer>(b =>
        {
            b.ToTable("Trainers", t => t.HasCheckConstraint("CK_Trainers_ExperienceYears", "[ExperienceYears] >= 0"));
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Id).HasColumnType("uniqueidentifier");
            b.Property(x => x.PassportNumber).HasMaxLength(20).IsRequired();
            b.HasIndex(x => x.PassportNumber).IsUnique();
            b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Gender).HasConversion<int>();
            b.Property(x => x.BirthDate).HasColumnType("date");
            b.Ignore(x => x.FullName);
            b.HasOne<Specialization>().WithMany().HasForeignKey(x => x.SpecializationId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Specialization>(b =>
        {
            b.ToTable("Specializations");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Id).HasColumnType("uniqueidentifier");
            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Booking>(b =>
        {
            b.ToTable("Bookings");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(x => x.Id).HasColumnType("uniqueidentifier");
            b.Property(x => x.HallName).HasMaxLength(150).IsRequired();
            b.Property(x => x.ScheduledAt).HasColumnType("datetime2");
            b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<Trainer>().WithMany().HasForeignKey(x => x.TrainerId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => x.ScheduledAt);
            b.HasIndex(x => new { x.HallName, x.ScheduledAt });
        });
    }
}
