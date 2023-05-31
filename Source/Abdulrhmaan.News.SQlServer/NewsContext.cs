using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abdulrhmaan.News.SQlServer;

public class NewsContext : IdentityDbContext<User>
{
    public virtual DbSet<User> Authors { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Category> Categorys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
    {
        if (OptionsBuilder.IsConfigured) return;
        OptionsBuilder.UseSqlServer("Server=DESKTOP-41ISMFG;Database=NewsDb;Integrated Security=True;TrustServerCertificate=True;");
        OptionsBuilder.UseLazyLoadingProxies()
              .EnableDetailedErrors()
              .EnableSensitiveDataLogging();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.Property(e => e.Id).ValueGeneratedNever();

        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });


        modelBuilder.Entity<Item>().HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Item>().HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Category>().HasOne(e => e.USer).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

    }


}
