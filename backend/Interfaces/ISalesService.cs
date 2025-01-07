using backend.Models;
namespace backend.Interfaces;
public interface ISalesService
{
    IEnumerable<SalesViewModel>GetList();
    public bool Add(Sales sales);
    public bool Edit(Sales sales);
    public bool Delete(int sales_id);
}