using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersion.CREATE_TABLE_USERS)]
public class CreateTableUser : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Name").AsString(250).NotNullable()
            .WithColumn("Email").AsString(250).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}
