using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Repositories;

public interface IUnityOfWork
{
    Task Commit();
}
