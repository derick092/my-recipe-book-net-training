using Moq;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;

public class IUserWriteOnlyRepositoryBuilder
{
    public static IUserWritesOnlyRepository Build()
    {
        var moq = new Mock<IUserWritesOnlyRepository>();
        return moq.Object;
    }
}
