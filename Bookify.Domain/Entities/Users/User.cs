using Bookify.Domain.Common.Model;
using Bookify.Domain.Common.Validation;
using Bookify.Domain.Common.Validation.ValidationItems;
using Bookify.Domain.Persistence.Users;

namespace Bookify.Domain.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public const int NameMaxLength = 50;
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        
        // Navigation property for books
        public ICollection<Books.Book> Books { get; set; } = [];

        public async Task<Result<int?>> Create(IUserRepository userRepository)
        {
            var validationResult = await CreateOrUpdateValidation();
            if (validationResult.HasError)
            {
                return new Result<int?>(null, validationResult);
            }

            await userRepository.InsertAsync(this);

            return new Result<int?>(Id, validationResult);
        }

        public async Task<ValidationResult> CreateOrUpdateValidation()
        {
            var validationResult = new ValidationResult();

            if (Name?.Length > NameMaxLength)
                validationResult.AddValidationItem(ValidationItems.User.NameMaxLength);

            return validationResult;
        }
    }
}
