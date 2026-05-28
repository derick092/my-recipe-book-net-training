using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserWritesOnlyRepository
{
    Task Add(Entities.User user);
}
