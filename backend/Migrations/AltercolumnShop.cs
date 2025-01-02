using FluentMigrator;
//using FluentMigrator.Postgres;
namespace backend{

    [Migration(20250101010)]
    public class AltercolumnShop: Migration
    {
        public override void Up()
        {
            Alter.Table("product")
             .AddColumn("description")
             .AsString(255)
              .NotNullable();
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }

    }

}
