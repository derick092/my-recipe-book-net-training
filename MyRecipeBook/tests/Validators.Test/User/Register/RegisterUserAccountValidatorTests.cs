using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exception;
using Shouldly;

namespace Validators.Test.User.Register;

public class RegisterUserAccountValidatorTests
{
    [Fact]
    public void Validator_ShouldBeValid() 
    {
        //arrange
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        var validator = new RegisterUserAccountValidator();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNameIsEmpty()
    {
        //arrange
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Name = string.Empty;

        var validator = new RegisterUserAccountValidator();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(RessourceMessagesException.VALIDATION_NAME_REQUIRED));
        });
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenEmailIsEmpty()
    {
        //arrange
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Email = string.Empty;

        var validator = new RegisterUserAccountValidator();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(RessourceMessagesException.VALIDATION_EMAIL_REQUIRED));
        });
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenPasswordIsEmpty()
    {
        //arrange
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Password = string.Empty;

        var validator = new RegisterUserAccountValidator();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(RessourceMessagesException.VALIDATION_PASSWORD_REQUIRED));
        });
    }
    [Fact]
    public void Validator_ShouldHaveError_WhenEmailIsInvalid()
    {
        //arrange
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Email = "InvalidEmail";

        var validator = new RegisterUserAccountValidator();

        //act
        var result = validator.Validate(request);

        //assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(RessourceMessagesException.VALIDATION_EMAIL_INVALID));
        });
    }
}
