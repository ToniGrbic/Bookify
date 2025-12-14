using Bookify.Application.Common.Model;
using Bookify.Domain.Enumerations.Users;
using Bookify.Domain.Persistence.Users;

namespace Bookify.Application.Users.User
{
    public class CreateUserRequest
    {
        public required string Name { get; init; }
        public DateOnly? DateOfBirth { get; init; }
    }

    public class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public CreateUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateUserRequest request, Result<SuccessPostResponse> result)
        {
            var user = new Domain.Entities.Users.User
            {
                Name = request.Name,
                DateOfBirth = request?.DateOfBirth,
            };

            var validationResult = await user.Create(_unitOfWork.Repository);
            result.SetValidationResult(validationResult.ValidationResult);

            if (result.HasError)
                return result;

            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessPostResponse(user.Id));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
