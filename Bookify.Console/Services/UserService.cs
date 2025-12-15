using Bookify.Application.Users.User;
using Bookify.Domain.Persistence.Users;

namespace Bookify.Console.Services
{
    public class UserService
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public UserService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var result = await _unitOfWork.Repository.Get();

            return result.Values.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name
            });
        }

        public async Task<UserResponse?> GetUserByIdAsync(int userId)
        {
            var user = await _unitOfWork.Repository.GetById(userId);

            if (user == null)
                return null;

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<IEnumerable<BookResponse>> GetUserBooksAsync(int userId)
        {
            var books = await _unitOfWork.Repository.GetUserBooks(userId);

            return books.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate
            });
        }
    }
}
