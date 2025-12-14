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

            var mainMenuOptions = new Dictionary<string, (string Description, Func<Task<bool>> Action)>
            {
                { "1", ("Select User", async () => { await HandleSelectUserAsync(); System.Console.Clear(); return false; }) },
                { "2", ("Exit", async () => { System.Console.WriteLine("Exiting application..."); return true; }) }
            };

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
            System.Console.WriteLine();
            System.Console.WriteLine($"=== {title} ===");
            
            foreach (var option in options)
            {
                System.Console.WriteLine($"{option.Key}. {option.Value.Description}");
            }
            
            System.Console.Write("Select an option: ");
            return System.Console.ReadLine() ?? "";
        }

        private async Task HandleSelectUserAsync()
        {
            System.Console.Clear();
            System.Console.WriteLine();
            System.Console.WriteLine("=== AVAILABLE USERS ===");

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

            System.Console.WriteLine();
            System.Console.Write("Enter User ID: ");
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

            var userMenuOptions = new Dictionary<string, (string Description, Func<Task<bool>> Action)>
            {
                { "1", ("List User Books", async () => { await ShowUserBooksAsync(user.Id, user.Name); System.Console.Clear(); return false; }) },
                { "2", ("Go Back to Main Menu", async () => { return true; }) }
            };

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

        private async Task ShowUserBooksAsync(int userId, string userName)
        {
            System.Console.Clear();
            System.Console.WriteLine();
            System.Console.WriteLine($"=== BOOKS FOR {userName.ToUpper()} ===");
            System.Console.WriteLine();

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
                    System.Console.WriteLine($"{i + 1}. Title: {book.Title}");
                    System.Console.WriteLine($"   Author: {book.Author ?? "N/A"}");
                    System.Console.WriteLine($"   ISBN: {book.ISBN ?? "N/A"}");
                    System.Console.WriteLine($"   Published: {(book.PublishedDate.HasValue ? book.PublishedDate.Value.ToString("yyyy-MM-dd") : "N/A")}");
                    System.Console.WriteLine();
                }
            }

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
