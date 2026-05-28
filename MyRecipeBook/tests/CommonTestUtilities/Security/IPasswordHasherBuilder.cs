using Moq;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;

namespace CommonTestUtilities.Security;

public class IPasswordHasherBuilder
{
    private readonly Mock<IPasswordHasher> _moq;

    public IPasswordHasherBuilder()
    {
        _moq = new Mock<IPasswordHasher>();
        _moq.Setup(passwordHasher => passwordHasher.HashPassword(It.IsAny<string>())).Returns("hashed-password");
    }

    public void VerifyPassword(string password) => _moq.Setup(passwordHasher => passwordHasher.VerifyPassword(password, It.IsAny<string>())).Returns(true);

    public IPasswordHasher Build() => _moq.Object;
}
