using BusinessObjects;
using DataAccessLayer.DTO;
using Repositories;
using Repositories.Interface;
using Services.Interface;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService()
        {
            _repo = new CustomerRepository();
        }

        public void AddCustomer(CustomerDTO customer) =>  _repo.AddCustomer(customer);

        public Customer? CheckLogin(string email, string password)
        {
            Customer? customer =  GetCustomerByEmail(email);

            if (customer == null || !customer.Password.Equals(password))
            {
                return null;
            }

            return customer;
        }

        public void DeleteCustomer(int id) =>  _repo.DeleteCustomer(id);

        public Customer? GetCustomerByEmail(string email) =>  _repo.GetCustomerByEmail(email);

        public Customer? GetCustomerById(int id) =>  _repo.GetCustomerById(id);

        public List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate) => _repo.GetCustomers(predicate);

        public void UpdateCustomer(CustomerDTO customer) =>  _repo.UpdateCustomer(customer);

        public bool UpdateProfile(Customer customer) =>  _repo.UpdateCustomer(customer);

        public int CountCustomers() => _repo.CountCustomers();
    }
}
