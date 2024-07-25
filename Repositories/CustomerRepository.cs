using DataAccessLayer;
using BusinessObjects;
using Repositories.Interface;
using DataAccessLayer.DTO;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(CustomerDTO customer) =>  CustomerDAO.AddCustomer(customer);

        public void DeleteCustomer(int id) =>  CustomerDAO.DeleteCustomer(id);

        public Customer? GetCustomerByEmail(string email) =>  CustomerDAO.GetCustomerByEmail(email);

        public Customer? GetCustomerById(int id) =>  CustomerDAO.GetCustomerById(id);

        public List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate) => CustomerDAO.GetCustomers(predicate);

        public bool UpdateCustomer(Customer customer) =>  CustomerDAO.UpdateCustomer(customer);

        public void UpdateCustomer(CustomerDTO customer) =>  CustomerDAO.UpdateCustomer(customer);

        public int CountCustomers() => CustomerDAO.CountCustomers();
    }
}
