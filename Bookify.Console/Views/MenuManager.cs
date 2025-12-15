using Bookify.Application.Users.User;
using Bookify.Console.Helpers;
using Bookify.Console.Services;

namespace Bookify.Console.Views
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
                Writer.DisplayMenu("BOOKIFY - MAIN MENU", mainMenuOptions);
                var choice = Reader.ReadMenuChoice();
                
                if (mainMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await mainMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
        }

        public async Task HandleSelectUserAsync()
        {
            System.Console.Clear();
            Writer.WriteMessage("\n=== AVAILABLE USERS ===\n");

            var users = await _userService.GetAllUsersAsync();
            var userList = users.ToList();

            if (!userList.Any())
            {
                Writer.WriteMessage("No users found in the database.");
                Writer.WaitForKey();
                return;
            }

            Writer.WriteUsers(userList);

            var userId = Reader.ReadInt("\nEnter User ID: ");

            if (userId.HasValue)
            {
                var selectedUser = await _userService.GetUserByIdAsync(userId.Value);
                if (selectedUser != null)
                {
                    await ShowUserMenuAsync(selectedUser);
                }
                else
                {
                    Writer.WriteMessage($"User with ID {userId.Value} not found.");
                    Writer.WaitForKey();
                }
            }
            else
            {
                Writer.WriteMessage("Invalid User ID.");
                Writer.WaitForKey();
            }
        }

        private async Task ShowUserMenuAsync(UserResponse user)
        {
            bool goBack = false;

            var userMenuOptions = MenuOptions.CreateUserMenuOptions(this, user);

            while (!goBack)
            {
                System.Console.Clear();
                Writer.DisplayMenu($"USER MENU: {user.Name} (ID: {user.Id})", userMenuOptions);

                var choice = Reader.ReadMenuChoice();
                
                if (userMenuOptions.ContainsKey(choice))
                {
                    goBack = await userMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
        }

        public async Task ShowUserBooksAsync(int userId, string userName)
        {
            System.Console.Clear();
            Writer.WriteMessage($"\n=== BOOKS FOR {userName.ToUpper()} ===\n");

            var books = await _userService.GetUserBooksAsync(userId);
            
            Writer.WriteBooks(books, userName);
            Writer.WaitForKey();
        }
    }
}
