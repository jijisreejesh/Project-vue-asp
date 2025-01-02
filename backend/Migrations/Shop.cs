using FluentMigrator;
//using FluentMigrator.Postgres;
namespace backend{

    [Migration(2025010104)]
    public class Shop: Migration
    {
        public override void Up()
        {

            Create.Table("product")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("category").AsString(255).NotNullable()
            .WithColumn("price").AsInt32().NotNullable()
            .WithColumn("quantity_in_stock").AsInt32().NotNullable();
       
            Create.Table("customer")
              .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("phone").AsString(255).Nullable()
             .WithColumn("email").AsString(255).NotNullable()
            .WithColumn("city").AsString(255).NotNullable();
           

            Create.Table("sales")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("customer_id").AsInt32().NotNullable()
            .WithColumn("product_id").AsInt32().NotNullable()
            .WithColumn("quantity").AsInt32().NotNullable()
            .WithColumn("total_price").AsInt32().NotNullable()
            .WithColumn("sales_date").AsDate().NotNullable()
            .WithColumn("payment_method").AsString(255).NotNullable()
            .WithColumn("status").AsString(255).NotNullable();

            Create.ForeignKey("fk_sales_customer_id_customer_id")
			.FromTable("sales").ForeignColumn("customer_id")
			.ToTable("customer").PrimaryColumn("id");
       
             Create.ForeignKey("fk_sales_product_id_product_id")
			.FromTable("sales").ForeignColumn("product_id")
			.ToTable("product").PrimaryColumn("id");
       

        }
        public override void Down()
        {
            // Delete.Table("Student");
        }
     
    }
}

