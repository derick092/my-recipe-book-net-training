using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services) //.Net >= 10
    {
        public void AddInfrastructure(IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

            services.AddDbContext<MyRecipeBookDbContext>(config => 
            {
                config.UseMySQL(configuration.GetConnectionString("DbConnection")!);
            });

            services.AddFluentMigratorCore().ConfigureRunner(config => 
            {
                config
                .AddMySql5()
                .WithGlobalConnectionString(_ => 
                {
                    return configuration.GetConnectionString("DbConnection")!;
                })
                .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure"))
                .For
                .All();
            });

            services.AddScoped<IUnityOfWork, UnitOfWork>();

            services.AddScoped<IUserWritesOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
