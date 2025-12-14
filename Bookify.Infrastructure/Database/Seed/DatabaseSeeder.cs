using Bookify.Domain.Entities.Books;
using Bookify.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Database.Seed
{
    public static class DatabaseSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John Doe",
                    DateOfBirth = new DateOnly(1990, 5, 15)
                },
                new User
                {
                    Id = 2,
                    Name = "Jane Smith",
                    DateOfBirth = new DateOnly(1985, 8, 22)
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2008, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                    UserId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt, David Thomas",
                    ISBN = "978-0135957059",
                    PublishedDate = new DateTime(2019, 9, 13, 0, 0, 0, DateTimeKind.Utc),
                    UserId = 1
                },
                new Book
                {
                    Id = 3,
                    Title = "Design Patterns",
                    Author = "Gang of Four",
                    ISBN = "978-0201633610",
                    PublishedDate = new DateTime(1994, 10, 21, 0, 0, 0, DateTimeKind.Utc),
                    UserId = 2
                },
                new Book
                {
                    Id = 4,
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    ISBN = "978-0321125215",
                    PublishedDate = new DateTime(2003, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    UserId = 2
                }
            );
        }
    }
}
