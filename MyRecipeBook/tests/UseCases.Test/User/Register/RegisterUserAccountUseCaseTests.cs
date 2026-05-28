using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register;

public class RegisterUserAccountUseCaseTests
{
    [Fact]
    public async Task UseCode_ShouldExecuteWithSuccess()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Tokens.AccessToken.ShouldBeNullOrEmpty();
        result.Tokens.RefreshToken.ShouldBeNullOrEmpty();
    }

    [Fact]
    public async Task UseCode_ShouldThrowException_WhenNameIsEmpty() 
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages.ShouldSatisfyAllConditions(errorMessages => 
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(RessourceMessagesException.VALIDATION_NAME_REQUIRED);
        });
    }

    [Fact]
    public async Task UseCode_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages.ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(RessourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS);
        });
    }

    private  RegisterUserAccountUseCase CreateUseCase(string? emailThatAlreadyExists = null) 
    {
        var unitOfWork = IUnityOfWorkBuilder.Build();
        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new IUserReadOnlyRepositoryBuilder();
        var passwordHasher = new IPasswordHasherBuilder().Build();

        if (emailThatAlreadyExists.IsNotEmpty()) 
        {
            userReadOnlyRepository.ExistsActiveUserWithEmail(emailThatAlreadyExists);
        }

        return new RegisterUserAccountUseCase(passwordHasher, userWriteOnlyRepository, userReadOnlyRepository.Build(), unitOfWork);
    }
}
