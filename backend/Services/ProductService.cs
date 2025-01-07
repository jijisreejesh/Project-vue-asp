using backend.Interfaces;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services;

public class ProductService : IProductService
{
    public bool Add(Product product)
    {
        try
            {
                using var connection = DBContext.GetConnection();
                var sql = "INSERT INTO Product (name,category,price,quantity_in_stock,description)" +
                "VALUES(@name,@category,@price,@quantityInStock,@description)";
                var result = connection.Execute(sql, new
                {                   
                    name = product.Name,
                    category = product.Category,
                    description = product.Description,
                    price = product.Price,
                    quantityInStock = product.Quantity_In_Stock
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

    public bool Delete(int product_id)
    {
       try{
        using var connection=DBContext.GetConnection();
          var salesDeleteSql="DELETE FROM Sales WHERE product_id=@id";
        connection.Execute(salesDeleteSql, new{id=product_id});
            var sql="DELETE FROM Product WHERE id=@id";
            var result=connection.Execute(sql,new {id=product_id});
            if(result>0)
            {
                return true;
            }
            return false;
       }
       catch(Exception ex){
         return false;
       }
    }

    public IEnumerable<Product> GetList()
    {
        try
        {
            using var connection = DBContext.GetConnection();
            var sql = "SELECT * FROM Product ORDER BY id";
            return connection.Query<Product>(sql);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<Product>();
        }
    }
    public IEnumerable<AutocompleteModel> GetProductList()
    {
         try{
            using var connection=DBContext.GetConnection();
            var sql=$"SELECT id, name as Label FROM Product ORDER BY id";
            return connection.Query<AutocompleteModel>(sql);
         
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<AutocompleteModel>();
            }
    }

    public bool Edit(Product product)
    {
        try
            {
                using var connection = DBContext.GetConnection();
                var sql = "UPDATE Product SET name=@name,category=@category,description=@description,price=@price,quantity_in_stock=@quantityInStock  where id=@id";
                var result = connection.Execute(sql, new
                {
                    id = product.Id,
                    name = product.Name,
                    category = product.Category,
                    description = product.Description,
                    price = product.Price,
                    quantityInStock = product.Quantity_In_Stock
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

    public Product GetById(int product_id){
    try{
        using var connection=DBContext.GetConnection();
        var sql="SELECT * FROM product where id=@id";
        return connection.QuerySingleOrDefault<Product>(sql,new{id=product_id});
    }
    catch(Exception ex){
        return null;
    }
    }

}