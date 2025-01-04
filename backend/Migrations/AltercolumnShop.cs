// using FluentMigrator;
// //using FluentMigrator.Postgres;
// namespace backend{

//     [Migration(20250101016)]
//     public class AltercolumnShop: Migration
//     {
//         public override void Up()
//         {
//             // Alter.Table("product")
//             //  .AddColumn("description")
//             //  .AsString(255)
//             //   .NotNullable();
//               Alter.Table("Sales")
//                  .AlterColumn("Sales_Date")
//                     .AsDate()
//                     .NotNullable();
//         }
//         public override void Down()
//         {
//             throw new NotImplementedException();
//         }

//     }
// }

