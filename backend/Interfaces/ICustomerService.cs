using backend.Models;
namespace backend.Interfaces;

public interface ICustomerService
{
    IEnumerable<Customer> GetList();
     bool Add(Customer customer);
    bool Delete(int customer_id);
     bool Edit(Customer customer);
     IEnumerable<AutocompleteModel> GetCustomerList();
}