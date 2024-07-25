using BusinessObjects;
using DataAccessLayer.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class CustomerDAO
{
    /// <summary>
    /// lấy khách hàng theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Customer? GetCustomerById(int id)
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers.FirstOrDefault(c => c.CustomerId.Equals(id));
    }
    /// <summary>
    ///  Lấy thông tin khách hàng theo email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static Customer? GetCustomerByEmail(string email)
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers.FirstOrDefault(c => c.EmailAddress.Equals(email));
    }
    /// <summary>
    /// Lấy danh sách khách hàng
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static List<CustomerDTO> GetCustomers(Func<Customer, bool> predicate)
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers
            .Where(predicate)
            .Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                CustomerFullName = c.CustomerFullName,
                Telephone = c.Telephone,
                EmailAddress = c.EmailAddress,
                CustomerBirthday = c.CustomerBirthday,
                CustomerStatus = c.CustomerStatus,
                Password = c.Password,
                CCCDFaceFront = c.CCCDFaceFront,
                CCCDBackSide = c.CCCDBackSide
            })
            .ToList();
    }
    /// <summary>
    /// Lấy số lượng khách hàng
    /// </summary>
    /// <returns></returns>
    public static int CountCustomers()
    {
        using var db = new FuminiHotelManagementContext();
        return db.Customers
            .Where(c => c.CustomerStatus == 1)
            .Count();
    }
    /// <summary>
    /// Cập nhật thông tin khách hàng
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public static bool UpdateCustomer(Customer customer)
    {
        using var db = new FuminiHotelManagementContext();
        db.Customers.Update(customer);
        var success = db.SaveChanges();
        return success == 1 ? true : false;
    }
    /// <summary>
    /// Cập nhật thông tin khách hàng theo DTO
    /// </summary>
    /// <param name="customer"></param>
    public static void UpdateCustomer(CustomerDTO customer)
    {
        using var db = new FuminiHotelManagementContext();
        var existingCustomer = db.Customers.Find(customer.CustomerId);
        if (existingCustomer != null)
        {
            existingCustomer.CustomerFullName = customer.CustomerFullName;
            existingCustomer.Telephone = customer.Telephone;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.CustomerBirthday = customer.CustomerBirthday;
            existingCustomer.CustomerStatus = customer.CustomerStatus;
            existingCustomer.Password = customer.Password;
            existingCustomer.CCCDFaceFront = customer.CCCDFaceFront;
            existingCustomer.CCCDBackSide = customer.CCCDBackSide;
            db.Customers.Update(existingCustomer);
            db.SaveChanges();
        }
    }
    /// <summary>
    /// Xóa khách hàng theo id 
    /// </summary>
    /// <param name="customerId"></param>
    public static void DeleteCustomer(int customerId)
    {
        using var db = new FuminiHotelManagementContext();
        var customer = db.Customers.Find(customerId);
        if (customer != null)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
    /// <summary>
    /// Thêm khách hàng
    /// </summary>
    /// <param name="customer"></param>
    public static void AddCustomer(CustomerDTO customer)
    {
        using var db = new FuminiHotelManagementContext();
        var newCustomer = new Customer
        {
            CustomerFullName = customer.CustomerFullName,
            Telephone = customer.Telephone,
            EmailAddress = customer.EmailAddress,
            CustomerBirthday = customer.CustomerBirthday,
            CustomerStatus = customer.CustomerStatus,
            Password = customer.Password,
            CCCDFaceFront = customer.CCCDFaceFront,
            CCCDBackSide = customer.CCCDBackSide
        };

        db.Customers.Add(newCustomer);
        db.SaveChanges();
    }
}
