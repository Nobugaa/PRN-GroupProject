using BusinessObjects;
using DataAccessLayer.DTO;

namespace Services.Interface
{
    public interface ICustomerService
    {
        Customer? GetCustomerById(int id);
        Customer? GetCustomerByEmail(string email);
        Customer? CheckLogin(string email, string password);
        bool UpdateProfile(Customer customer);
        void AddCustomer(CustomerDTO customer);
        void DeleteCustomer(int id);
        void UpdateCustomer(CustomerDTO customer);
        List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate);
        int CountCustomers();
    }
}
