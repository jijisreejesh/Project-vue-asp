
using backend.Interfaces;
using backend.Models;
using Dapper;
namespace backend.Services;

public class CustomerService : ICustomerService
{
    public IEnumerable<Customer> GetList()
    {
        try
        {
            using var connection = DBContext.GetConnection();
            var sql = "SELECT * FROM Customer ORDER BY id";
            return connection.Query<Customer>(sql);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<Customer>();
        }
    }
    public bool Add(Customer customer)
    {
        try
        {
            using var connection = DBContext.GetConnection();
            var sql = "INSERT INTO Customer(name,phone,email,city)values(@Name,@Phone,@Email,@City)";
            var result = connection.Execute(sql, new
            {
                name = customer.Name,
                phone = customer.Phone,
                email = customer.Email,
                city = customer.City
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
    public bool Delete(int customer_id)
    {
        try
        {
            using var connection=DBContext.GetConnection();
             var salesDeleteSql="DELETE FROM Sales WHERE customer_id=@id";
        connection.Execute(salesDeleteSql, new{id=customer_id});
           var sql="DELETE FROM customer where id=@id";
           var result=connection.Execute(sql,new {Id=customer_id});
           if(result>0)
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
    public bool Edit(Customer customer)
    {
          try{
            using var connection=DBContext.GetConnection();
            var sql="UPDATE Customer SET name=@name,phone=@phone,email=@email,city=@city WHERE id=@id";
            var result=connection.Execute(sql,new{
                    id=customer.Id,
                    name=customer.Name,
                    phone=customer.Phone,
                    email=customer.Email,
                    city=customer.City
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


    public IEnumerable<AutocompleteModel> GetCustomerList()
    {
         try{
            using var connection=DBContext.GetConnection();
            var sql=$"SELECT id, name as Label FROM Customer ORDER BY id";
            return connection.Query<AutocompleteModel>(sql);
         
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<AutocompleteModel>();
            }
    }


}