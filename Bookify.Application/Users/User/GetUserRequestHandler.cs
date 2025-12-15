using Bookify.Application.Common.Model;
using Bookify.Domain.Persistence.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Users.User
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class GetUserRequestHandler : RequestHandler<GetByIdRequest, GetUserResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Common.Model.Result<GetUserResponse>> HandleRequest(
            GetByIdRequest request,
            Common.Model.Result<GetUserResponse> result)
        {
            var sqlResult = await _unitOfWork.Repository.GetById(request.Id);

            if (sqlResult == null)
                return result;

            result.SetResult(new GetUserResponse
            {
                Id = sqlResult.Id,
                Name = sqlResult.Name
            });

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
