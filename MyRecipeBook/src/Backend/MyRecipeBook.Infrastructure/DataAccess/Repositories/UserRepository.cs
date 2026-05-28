using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

internal sealed class UserRepository(MyRecipeBookDbContext dbContext) : IUserWritesOnlyRepository, IUserReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithEmail(string email) => 
        await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(email));
}
