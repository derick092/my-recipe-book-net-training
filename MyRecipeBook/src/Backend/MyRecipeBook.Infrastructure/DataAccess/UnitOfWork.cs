using MyRecipeBook.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Infrastructure.DataAccess;

internal sealed class UnitOfWork(MyRecipeBookDbContext dbContext) : IUnityOfWork
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
