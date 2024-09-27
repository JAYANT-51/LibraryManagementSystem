using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IssuedBook> IssuedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssuedBook>()
                .HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookID);

            modelBuilder.Entity<IssuedBook>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserID);
        }
    }
}
