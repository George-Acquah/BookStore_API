using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.DB
{
    public class BookStoreAPIDbContext(DbContextOptions<BookStoreAPIDbContext> dbContextOptions): DbContext(dbContextOptions)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Book>()
                .HasIndex(b => b.Title)
                .IsUnique();

            builder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books)
                .UsingEntity<Dictionary<string, object>>(
                "BookCategory",
                b => b.HasOne<BookCategory>().WithMany().HasForeignKey("CategoryId"),
                c => c.HasOne<Book>().WithMany().HasForeignKey("BookId")
               );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
