using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class IUnityOfWorkBuilder
{
    public static IUnityOfWork Build() 
    {
        var moq = new Mock<IUnityOfWork>();
        return moq.Object;
    }
}
