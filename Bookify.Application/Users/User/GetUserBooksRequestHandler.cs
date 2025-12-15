using Bookify.Application.Common.Model;
using Bookify.Domain.Common.Model;
using Bookify.Domain.Persistence.Users;
namespace Bookify.Application.Users.User
{
    public class GetUserBooksRequest
    {
        public int UserId { get; init; }

        public GetUserBooksRequest(int userId)
        {
            UserId = userId;
        }

        public GetUserBooksRequest()
        {
        }
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public DateTime? PublishedDate { get; set; }
    }

    public class GetUserBooksRequestHandler : RequestHandler<GetUserBooksRequest, GetAllResponse<BookResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetUserBooksRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Common.Model.Result<GetAllResponse<BookResponse>>> HandleRequest(
            GetUserBooksRequest request,
            Common.Model.Result<GetAllResponse<BookResponse>> result)
        {
            var books = await _unitOfWork.Repository.GetUserBooks(request.UserId);

            var bookResponses = books.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate
            });

            result.SetResult(new GetAllResponse<BookResponse>(bookResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
