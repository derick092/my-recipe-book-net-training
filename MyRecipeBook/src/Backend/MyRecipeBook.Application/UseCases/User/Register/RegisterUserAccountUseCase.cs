using FluentValidation.Results;
using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserAccountUseCase(
    IPasswordHasher passwordHasher, 
    IUserWritesOnlyRepository userWritesOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnityOfWork unityOfWork) : IRegisterUserAccountUseCase
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserWritesOnlyRepository _userWritesOnlyRepository = userWritesOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserAccountJson request)
    {
        await ValidateAndThrowOnFailures(request);

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordHasher.HashPassword(request.Password);

        await _userWritesOnlyRepository.Add(user);

        await _unityOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }

    private async Task ValidateAndThrowOnFailures(RequestRegisterUserAccountJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        if (await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email)) 
        {
            result.Errors.Add(new ValidationFailure(string.Empty, RessourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid is false) 
        {
            throw new ErrorOnValidationException([.. result.Errors.Select(error => error.ErrorMessage)]);
        }
    }
}
