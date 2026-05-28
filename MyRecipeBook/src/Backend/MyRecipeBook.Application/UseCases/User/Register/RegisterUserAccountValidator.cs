using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserAccountValidator : AbstractValidator<RequestRegisterUserAccountJson>
{
    public RegisterUserAccountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RessourceMessagesException.VALIDATION_NAME_REQUIRED);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(RessourceMessagesException.VALIDATION_EMAIL_REQUIRED);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(RessourceMessagesException.VALIDATION_PASSWORD_REQUIRED);
        When(user => user.Email.IsNotEmpty(), () => 
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(RessourceMessagesException.VALIDATION_EMAIL_INVALID);
        });
    }
}
