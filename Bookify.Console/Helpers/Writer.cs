using Bookify.Application.Users.User;

namespace Bookify.Console.Helpers
{
    public static class Writer
    {
        public static void WriteUsers(IEnumerable<GetUserResponse> users)
        {
            var userList = users.ToList();
            
            if (!userList.Any())
            {
                System.Console.WriteLine("No users found.");
                return;
            }

            foreach (var user in userList)
            {
                System.Console.WriteLine($"ID: {user.Id} | Name: {user.Name}");
            }
        }

        public static void WriteBooks(IEnumerable<BookResponse> books, string userName = "")
        {
            var bookList = books.ToList();

            if (!bookList.Any())
            {
                System.Console.WriteLine($"No books found{(string.IsNullOrEmpty(userName) ? "." : $" for {userName}.")}");
                return;
            }

            for (int i = 0; i < bookList.Count; i++)
            {
                var book = bookList[i];
                System.Console.WriteLine($"{i + 1}. Title: {book.Title}\n" +
                    $"   Author: {book.Author ?? "N/A"}\n" +
                    $"   ISBN: {book.ISBN ?? "N/A"}\n" +
                    $"   Published: {(book.PublishedDate.HasValue ? book.PublishedDate.Value.ToString("yyyy-MM-dd") : "N/A")}\n");
            }
        }

        public static void DisplayMenu(string title, Dictionary<string, (string Description, Func<Task<bool>> Action)> options)
        {
            System.Console.WriteLine($"\n=== {title} ===");
            
            foreach (var option in options)
            {
                System.Console.WriteLine($"{option.Key}. {option.Value.Description}");
            }
        }

        public static void WriteMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public static void WaitForKey()
        {
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
