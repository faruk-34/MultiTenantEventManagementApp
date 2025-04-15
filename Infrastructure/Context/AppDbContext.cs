using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WorkContext _workContext { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options,
            WorkContext workContext, IConfiguration configuration

            )
            : base(options)
        {
            _workContext = workContext;
            _configuration = configuration;
        }

        // DbSet tanımlamaları
        public DbSet<Users> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity ayarları
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(150).IsRequired();
                entity.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
              

                // İlişki: User ile Tenant
                entity.HasOne(u => u.Tenant)
                      .WithMany(t => t.Users)
                      .HasForeignKey(u => u.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Tenant entity ayarları
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).HasMaxLength(100).IsRequired();
                entity.Property(t => t.Address).HasMaxLength(250);

                // İlişki: Tenant ile Event
                entity.HasMany(t => t.Events)
                      .WithOne(e => e.Tenant)
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Event entity ayarları
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Location).HasMaxLength(250).IsRequired();

                // İlişki: Event ile Tenant
                entity.HasOne(e => e.Tenant)
                      .WithMany(t => t.Events)
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);

                // İlişki: Event ile Registration
                entity.HasMany(e => e.Registrations)
                      .WithOne(r => r.Event)
                      .HasForeignKey(r => r.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Registration entity ayarları
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => r.Id);

                // İlişki: Registration ile Event
                entity.HasOne(r => r.Event)
                      .WithMany(e => e.Registrations)
                      .HasForeignKey(r => r.EventId)
                      .OnDelete(DeleteBehavior.Cascade);

                // İlişki: Registration ile User
                entity.HasOne(r => r.User)
                      .WithMany()
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Role entity ayarları
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).HasMaxLength(50).IsRequired();

                // Rolleri seed et
                //entity.HasData(
                //    new Role { Id = 1, Name = "Admin" },
                //    new Role { Id = 2, Name = "EventManager" },
                //    new Role { Id = 3, Name = "Attendee" }
                //);
            });

            // UserRole (many-to-many) ayarları
            modelBuilder.Entity<UserRole>(entity =>
            {
                // Composite primary key (UserId, RoleId)
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                // İlişki: UserRole ile User
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // İlişki: UserRole ile Role
                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyQueryFilter<IMultiTenant>(t => t.TenantId == _workContext.TenantId);
            modelBuilder.ApplyQueryFilter<ISoftDeletable>(t => t.IsDeleted == false);
        }


    }
}
