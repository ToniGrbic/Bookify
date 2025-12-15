using Bookify.Application.Users.User;
using Bookify.Application.Common.Model;

namespace Bookify.Console.Services
{
    public class UserService
    {
        private readonly GetAllUsersRequestHandler _getAllUsersHandler;
        private readonly GetUserRequestHandler _getUserHandler;
        private readonly GetUserBooksRequestHandler _getUserBooksHandler;

        public UserService(
            GetAllUsersRequestHandler getAllUsersHandler,
            GetUserRequestHandler getUserHandler,
            GetUserBooksRequestHandler getUserBooksHandler)
        {
            _getAllUsersHandler = getAllUsersHandler;
            _getUserHandler = getUserHandler;
            _getUserBooksHandler = getUserBooksHandler;
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllUsersAsync()
        {
            var result = await _getAllUsersHandler.ProcessAuthorizedRequestAsync(new GetAllRequest());

            return result.Value?.Values ?? [];
        }

        public async Task<GetUserResponse?> GetUserByIdAsync(int userId)
        {
            var result = await _getUserHandler.ProcessAuthorizedRequestAsync(new GetByIdRequest(userId));

            return result.Value;
        }

        public async Task<IEnumerable<BookResponse>> GetUserBooksAsync(int userId)
        {
            var result = await _getUserBooksHandler.ProcessAuthorizedRequestAsync(new GetUserBooksRequest(userId));

            return result.Value?.Values ?? [];
        }
    }
}
