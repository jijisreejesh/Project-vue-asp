using backend.Models;
using backend.Interfaces;
using Dapper;
namespace backend.Services;
public class SalesService:ISalesService
{
    public IEnumerable<SalesViewModel>GetList()
    {
        try{
             using var connection = DBContext.GetConnection();
           var sql = @"
        SELECT 
            s.*, 
            c.name AS customerName, 
            p.name AS productName
        FROM sales s
        INNER JOIN customer c ON s.customer_id = c.id
        INNER JOIN product p ON s.product_id = p.id ORDER BY id";
            return connection.Query<SalesViewModel>(sql);
        }
        catch(Exception ex)
        {
             Console.WriteLine(ex.Message);
            return Enumerable.Empty<SalesViewModel>();
        }
    }
    public bool Add(Sales sales)
    {
         try
            {
                using var connection = DBContext.GetConnection();
                var sql = "INSERT INTO Sales (customer_id,product_id,quantity,total_price,sales_date,payment_method,status)" +
                "VALUES(@Customer_Id,@Product_Id,@Quantity,@Total_Price,@Sales_Date,@Payment_Method,@Status)";
                var result = connection.Execute(sql, new
                {
                    customer_id=sales.Customer_Id,
                    product_id = sales.Product_Id,
                    quantity= sales.Quantity,
                    total_price=sales.Total_Price,
                    sales_date=sales.Sales_Date,
                    payment_method=sales.Payment_Method,
                    status=sales.Status
                });
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
    }
    public bool Edit(Sales sales)
    {
         try{
                using var connection=DBContext.GetConnection();
                var sql="UPDATE SALES SET customer_id=@Customer_Id,product_id=@Product_Id,quantity=@Quantity,total_price=@Total_Price,sales_date=@Sales_Date,payment_method=@Payment_Method,status=@Status where id=@id";
                var result=connection.Execute(sql,new{
                     id=sales.Id,
                    product_id=sales.Product_Id,
                    customer_id=sales.Customer_Id,
                    quantity=sales.Quantity,
                    total_price=sales.Total_Price,
                    sales_date=sales.Sales_Date,
                    payment_method=sales.Payment_Method,
                    status=sales.Status
                });
                if(result>0)
                {
                    return true;
                }
                 return false;
            }
            
            catch(Exception ex)
            {
                return false;
            }
    }
    public bool Delete(int sales_id)
    {
        try{
             using var connection=DBContext.GetConnection();
            var sql="DELETE FROM Sales WHERE id=@id";
            var result=connection.Execute(sql,new {Id=sales_id});
            if(result>0)
            {
               return true;
            }
           return false;
        }
        catch(Exception ex)
        {
             return false;
        }
    }
}