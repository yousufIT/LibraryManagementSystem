using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.infrastructure
{
    public class LibraryDbContext : DbContext
    {

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        protected LibraryDbContext()
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB ; Initial Catalog=LibraryTest");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Book and Author
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookID, ba.AuthorID });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookID);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorID);

            // Configure relationship between Book and MainCategory
            modelBuilder.Entity<Book>()
                .HasOne(b => b.MainCategory)
                .WithMany()
                .HasForeignKey(b => b.MainCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            //// Configure relationship between Book and SubCategory
            //modelBuilder.Entity<Book>()
            //    .HasOne(b => b.SubCategory)
            //    .WithMany()
            //    .HasForeignKey(b => b.SubCategoryID)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship between Category and its SubCategories
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryID);
            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryID = 1, Name = "Fiction", ParentCategoryID = null },
            new Category { CategoryID = 2, Name = "Non-Fiction", ParentCategoryID = null },
            new Category { CategoryID = 3, Name = "Science Fiction", ParentCategoryID = 1 },
            new Category { CategoryID = 4, Name = "Biographies", ParentCategoryID = 2 }
);

            // إضافة بيانات أولية للكتّاب (Authors)
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorID = 1, Name = "Isaac Asimov", DateOfBirth = new DateTime(1920, 1, 2), Nationality = "American",
                    Biography = "Famous science fiction author.", Email = "isaac.asimov@example.com" },
                new Author { AuthorID = 2, Name = "Agatha Christie", DateOfBirth = new DateTime(1890, 9, 15), Nationality = "British",
                    Biography = "Famous mystery author.", Email = "agatha.christie@example.com" }
            );

            // إضافة بيانات أولية للكتب (Books)
            modelBuilder.Entity<Book>().HasData(
                new Book { BookID = 1, Title = "Foundation", PublishedDate = new DateTime(1951, 6, 1), Description = "A science fiction novel.",
                    MainCategoryID = 3, SubCategoryID = null, Publisher = "Gnome Press", Pages = 255, Language = "English", Price = 15.99m, Edition = "1st",
                    Format = "Hardcover", Location = "Shelf A1" },
                new Book { BookID = 2, Title = "Murder on the Orient Express", PublishedDate = new DateTime(1934, 1, 1), Description = "A mystery novel.", 
                    MainCategoryID = 1, SubCategoryID = null, Publisher = "Collins Crime Club", Pages = 256, Language = "English", Price = 12.99m, Edition = "1st",
                    Format = "Paperback", Location = "Shelf B2" }
            );

            // إضافة بيانات أولية لعلاقة الكتاب والكاتب (BookAuthor)
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { BookID = 1, AuthorID = 1 },
                new BookAuthor { BookID = 2, AuthorID = 2 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
