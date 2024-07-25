using BusinessObjects;
using DataAccessLayer.DTO;

namespace Repositories.Interface
{
    public interface ICustomerRepository
    {
        Customer? GetCustomerById(int id);
        Customer? GetCustomerByEmail(string email);
        bool UpdateCustomer(Customer customer);
        void AddCustomer(CustomerDTO customer);
        void DeleteCustomer(int id);
        void UpdateCustomer(CustomerDTO customer);
        List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate);
        int CountCustomers();
    }
}
