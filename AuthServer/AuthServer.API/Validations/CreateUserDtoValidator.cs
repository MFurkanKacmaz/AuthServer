using AuthServer.Core.DTOs;
using FluentValidation;

namespace AuthServer.API.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Zorunlu alan");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Zorunlu alan").EmailAddress().WithMessage("Email formatı yanlış");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı zorunlu");
        }
    }
}
