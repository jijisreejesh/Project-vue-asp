using FluentMigrator;
//using FluentMigrator.Postgres;
namespace backend{

[Migration(2025010102)]
public class Users : Migration
{
    public override void Up()
    {
        Create.Table("Student2")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("Name").AsString()
        .WithColumn("Class").AsInt16()
        .WithColumn("Place").AsString();
    }
    public override void Down()
    {
        // Delete.Table("Student");
    }
}}