using DataAccessLayer.DTO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : UserControl
    {
        private readonly ICustomerService _service;

        public CustomerManagement()
        {
            InitializeComponent();
            _service = ((App)Application.Current).ServiceProvider.GetRequiredService<ICustomerService>() ?? throw new ArgumentNullException(nameof(CustomerService));
            LoadData();
        }
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        private void LoadData()
        {
            dgCustomers.ItemsSource = null;
            var customers = _service.GetCustomers(c => c.CustomerFullName.Contains(txtSearch.Text));
            dgCustomers.ItemsSource = customers;
        }
        /// <summary>
        ///  tìm kiếm khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                try
                {
                    var customers = _service.GetCustomers(c => c.CustomerFullName.Contains(txtSearch.Text));
                    dgCustomers.ItemsSource = null;
                    dgCustomers.ItemsSource = customers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                LoadData();
            }
        }
        /// <summary>
        /// thêm khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditCustomerDialog = new AddEditCustomerDialog();
            if (addEditCustomerDialog.ShowDialog() == true)
            {
                var newCustomer = addEditCustomerDialog.Customer;
                 _service.AddCustomer(newCustomer);
                LoadData();
            }
        }
        /// <summary>
        /// sửa khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is CustomerDTO selectedCustomer)
            {
                var addEditCustomerDialog = new AddEditCustomerDialog(selectedCustomer);
                if (addEditCustomerDialog.ShowDialog() == true)
                {
                    var updatedCustomer = addEditCustomerDialog.Customer;
                     _service.UpdateCustomer(updatedCustomer);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "Edit Customer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// xóa khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is CustomerDTO selectedCustomer)
            {
                if (MessageBox.Show($"Are you sure you want to delete Customer {selectedCustomer.CustomerFullName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    selectedCustomer.CustomerStatus = 0;
                     _service.DeleteCustomer(selectedCustomer.CustomerId);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Delete Customer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
