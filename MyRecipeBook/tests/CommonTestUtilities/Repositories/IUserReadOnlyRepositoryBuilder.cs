using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class IUserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _moq;

    public IUserReadOnlyRepositoryBuilder() => _moq = new Mock<IUserReadOnlyRepository>();

    public void ExistsActiveUserWithEmail(string email) => _moq.Setup(repository => repository.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);

    public IUserReadOnlyRepository Build() => _moq.Object;
}
