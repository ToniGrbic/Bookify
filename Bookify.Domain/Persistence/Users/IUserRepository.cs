using Bookify.Domain.Entities.Books;
using Bookify.Domain.Entities.Users;
using Bookify.Domain.Persistence.Common;

namespace Bookify.Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetById(int id);
        Task<IEnumerable<Book>> GetUserBooks(int userId);
    }
}
