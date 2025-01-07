using backend.Models;

namespace backend.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetList();
    bool Add(Product product);
    bool Delete(int product_id);
    bool Edit(Product product);
    Product GetById(int product_id);
    IEnumerable<AutocompleteModel> GetProductList();
}