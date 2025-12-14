using Bookify.Domain.Entities.Books;
using Bookify.Domain.Entities.Users;
using Bookify.Domain.Persistence.Users;
using Bookify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Book>> GetUserBooks(int userId)
        {
            return await _dbContext.Books
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
