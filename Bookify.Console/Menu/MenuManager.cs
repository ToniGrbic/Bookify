using Bookify.Console.Services;
using Bookify.Domain.Entities.Users;

namespace Bookify.Console.Menu
{
    public class MenuManager
    {
        private readonly UserService _userService;

        public MenuManager(UserService userService)
        {
            _userService = userService;
        }

        public async Task RunAsync()
        {
            bool exitRequested = false;

            var mainMenuOptions = MenuOptions.CreateMainMenuOptions(this);

            while (!exitRequested)
            {
                var choice = DisplayMenu("BOOKIFY - MAIN MENU", mainMenuOptions);
                
                if (mainMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await mainMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        private string DisplayMenu(string title, Dictionary<string, (string Description, Func<Task<bool>> Action)> options)
        {
            System.Console.WriteLine($"\n=== {title} ===");
            
            foreach (var option in options)
            {
                System.Console.WriteLine($"{option.Key}. {option.Value.Description}");
            }
            
            System.Console.Write("Select an option: ");
            return System.Console.ReadLine() ?? "";
        }

        public async Task HandleSelectUserAsync()
        {
            System.Console.Clear();
            System.Console.WriteLine("\n=== AVAILABLE USERS ===\n");

            var users = await _userService.GetAllUsersAsync();
            var userList = users.ToList();

            if (!userList.Any())
            {
                System.Console.WriteLine("No users found in the database.");
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
                return;
            }

            foreach (var user in userList)
            {
                System.Console.WriteLine($"ID: {user.Id} | Name: {user.Name}");
            }

            System.Console.Write("\nEnter User ID: ");
            var input = System.Console.ReadLine();

            if (int.TryParse(input, out int userId))
            {
                var selectedUser = await _userService.GetUserByIdAsync(userId);
                if (selectedUser != null)
                {
                    await ShowUserMenuAsync(selectedUser);
                }
                else
                {
                    System.Console.WriteLine($"User with ID {userId} not found.");
                    System.Console.WriteLine("Press any key to continue...");
                    System.Console.ReadKey();
                }
            }
            else
            {
                System.Console.WriteLine("Invalid User ID.");
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }

        private async Task ShowUserMenuAsync(User user)
        {
            bool goBack = false;

            var userMenuOptions = MenuOptions.CreateUserMenuOptions(this, user);

            while (!goBack)
            {
                System.Console.Clear();
                var choice = DisplayMenu($"USER MENU: {user.Name} (ID: {user.Id})", userMenuOptions);
                
                if (userMenuOptions.ContainsKey(choice))
                {
                    goBack = await userMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        public async Task ShowUserBooksAsync(int userId, string userName)
        {
            System.Console.Clear();
            System.Console.WriteLine($"\n=== BOOKS FOR {userName.ToUpper()} ===\n");

            var books = await _userService.GetUserBooksAsync(userId);
            var bookList = books.ToList();

            if (!bookList.Any())
            {
                System.Console.WriteLine("No books found for this user.");
            }
            else
            {
                for (int i = 0; i < bookList.Count; i++)
                {
                    var book = bookList[i];
                    System.Console.WriteLine($"{i + 1}. Title: {book.Title}\n" +
                        $"   Author: {book.Author ?? "N/A"}\n" +
                        $"   ISBN: {book.ISBN ?? "N/A"}\n" +
                        $"   Published: {(book.PublishedDate.HasValue ? book.PublishedDate.Value.ToString("yyyy-MM-dd") : "N/A")}\n"
                    );
                }
            }

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
