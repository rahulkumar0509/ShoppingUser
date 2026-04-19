using FluentValidation;
using ShoppingUser.Services;

namespace ShoppingUser.Domain
{
    public class UserDto
    {
        [CustomPasswordValidation]
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
    }

    // Fluent Validation
    public class UserValidator: AbstractValidator<UserDto>
    {
        public UserValidator(UserService userService)
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress().WithMessage("email address format is not correct").MustAsync(userService.IsEmailIdUnique).WithMessage("A valid Email is required");
            // RuleFor(x=>x.Email).NotEmpty().EmailAddress().MustAsync((email, ct) => Task.FromResult(userService.IsEmailIdUnique(email, ct))).WithMessage("A valid Email is required");
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(100).WithMessage("Name should not exceeds 100 chars.");
            RuleForEach(x=>x.Password).NotEmpty().WithMessage("should not be empty");
        }
    }
}
