using Bookify.Application.Common.Model;
using Bookify.Domain.Common.Model;
using Bookify.Domain.Persistence.Users;

namespace Bookify.Application.Users.User
{
    public class GetAllUsersRequestHandler : RequestHandler<GetAllRequest, GetAllResponse<GetUserResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetAllUsersRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected async override Task<Common.Model.Result<GetAllResponse<GetUserResponse>>> HandleRequest(GetAllRequest request, Common.Model.Result<GetAllResponse<GetUserResponse>> result)
        {
            var sqlResult = await _unitOfWork.Repository.Get();
            
            var userResponses = sqlResult.Values.Select(u => new GetUserResponse
            {
                Id = u.Id,
                Name = u.Name
            });

            result.SetResult(new GetAllResponse<GetUserResponse>(userResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
