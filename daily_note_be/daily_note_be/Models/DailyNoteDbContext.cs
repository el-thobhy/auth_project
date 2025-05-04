using Microsoft.EntityFrameworkCore;

namespace Daily_Note.Models
{
    public class DailyNoteDbContext : DbContext
    {
        public DailyNoteDbContext(DbContextOptions<DailyNoteDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<AuthorizationGroup> AuthorizationGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.Entity<Account>()
                .HasIndex(o => o.UserName)
                .IsUnique();
            modelBuilder.Entity<Account>()
                .Property(o => o.Email)
                .HasDefaultValue("auriwanyasper@gmail.com");
            modelBuilder.Entity<Account>()
                .Property(o => o.Otp)
                .HasColumnType("char(6)");

            modelBuilder.Entity<RoleGroup>()
                .HasIndex(o => o.GroupName)
                .IsUnique();
        }
    }

    public static class DailyNoteModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleGroup>()
                .HasData(
                new RoleGroup { Id = 1, GroupName = "Admin", Created_by = "admin", Created_on = DateTime.Now },
                new RoleGroup { Id = 2, GroupName = "User", Created_by = "admin", Created_on = DateTime.Now }
                );

            modelBuilder.Entity<Account>()
                .HasData(
                new Account
                {
                    Id = 1,
                    UserName = "admin",
                    // admin1234
                    Password = "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270",
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "auriwanyasper@gmail.com",
                    Is_delete = false,
                    RoleGroupId = 1,
                    Created_by = "admin",
                    Created_on = DateTime.Now
                },
                new Account
                {
                    Id = 2,
                    UserName = "user",
                    // dosen123
                    Password = "c431bffe6c2cf3b69ad2e9cbbe9806835dbced7c97b9d3f946387ee92eb17018",
                    FirstName = "Regular",
                    LastName = "User",
                    Email = "user@gmail.com",
                    Is_delete = false,
                    RoleGroupId = 2,
                    Created_by = "admin",
                    Created_on = DateTime.Now
                }
                );

            modelBuilder.Entity<AuthorizationGroup>()
                .HasData(
                new AuthorizationGroup { Id = 1, Role = "account", RoleGroupId = 1, Created_by = "admin", Created_on = DateTime.Now },
                new AuthorizationGroup { Id = 2, Role = "role", RoleGroupId = 1, Created_by = "admin", Created_on = DateTime.Now },
                new AuthorizationGroup { Id = 3, Role = "authorization", RoleGroupId = 1, Created_by = "admin", Created_on = DateTime.Now },
                new AuthorizationGroup { Id = 4, Role = "home", RoleGroupId = 2, Created_by = "admin", Created_on = DateTime.Now }
                );
        }
    }

}
