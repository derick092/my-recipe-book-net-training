namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> ErrorMessages) : MyRecipeBookException
{
    private readonly List<string> _errors = ErrorMessages;

    public List<string> GetErrorMessages => _errors;
}
