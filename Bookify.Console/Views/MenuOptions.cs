using Bookify.Application.Users.User;

namespace Bookify.Console.Views
{
    public class MenuOptions
    {
        private readonly Dictionary<string, (string Description, Func<Task<bool>> Action)> _options;

        public MenuOptions()
        {
            _options = [];
        }

        public MenuOptions AddOption(string key, string description, Func<Task<bool>> action)
        {
            _options.Add(key, (description, action));
            return this;
        }

        public Dictionary<string, (string Description, Func<Task<bool>> Action)> Build()
        {
            return _options;
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateMainMenuOptions(MenuManager menuManager)
        {
            return new MenuOptions()
                .AddOption("1", "Select User", async () => { await menuManager.HandleSelectUserAsync(); System.Console.Clear(); return false; })
                .AddOption("2", "Exit", async () => { System.Console.WriteLine("Exiting application..."); return true; })
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateUserMenuOptions(MenuManager menuManager, GetUserResponse user)
        {
            return new MenuOptions()
                .AddOption("1", "List User Books", async () => 
                { 
                    await menuManager.ShowUserBooksAsync(user.Id, user.Name ?? "Unknown User"); 
                    System.Console.Clear();
                    return false; 
                })
                .AddOption("2", "Go Back to Main Menu", async () => true)
                .Build();
        }
    }
}
