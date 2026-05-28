using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure.Migrations;

public class DatabaseMigration
{
    public static async void ExecuteMigrations(IServiceProvider serviceProvider) 
    {
        //used if you want to execute migrations with EF
        //var dbContext = serviceProvider.GetRequiredService<MyRecipeBookDbContext>();
        //await dbContext.Database.MigrateAsync();

        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}
