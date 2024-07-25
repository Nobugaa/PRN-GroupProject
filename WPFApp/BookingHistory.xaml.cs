using BusinessObjects;
using DataAccessLayer.DTO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for BookingHistory.xaml
    /// </summary>
    public partial class BookingHistory : UserControl
    {
        private readonly IBookingHistoryService _service;
        public Customer Customer;
        public BookingHistory()
        {
            InitializeComponent();
            _service = ((App)Application.Current).ServiceProvider.GetRequiredService<IBookingHistoryService>() ?? throw new ArgumentNullException(nameof(BookingHistoryService));
            Loaded += LoadData;
        }
        /// <summary>
        /// Lấy danh sách lịch sử đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void LoadData(object sender, RoutedEventArgs e)
        {
            List<BookingHistoryDTO> bookingDetails =  _service.GetBookingByCusId(Customer.CustomerId);

            dgBookingHistory.ItemsSource = bookingDetails;
        }
        /// <summary>
        ///  thêm đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookingDialog addEditBookingDialog = new AddEditBookingDialog();
            addEditBookingDialog.Customer = Customer;
            if (addEditBookingDialog.ShowDialog() == true)
            {
                LoadData(sender, e);
            }
        }
        /// <summary>
        /// sửa đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditBookingDialog addEditBookingDialog = new AddEditBookingDialog();
            addEditBookingDialog.Customer = Customer;
            //addEditBookingDialog.Booking = 
            if (addEditBookingDialog.ShowDialog() == true)
            {
                LoadData(sender, e);
            }
        }
        /// <summary>
        /// xóa đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingHistory.SelectedItem is BookingHistoryDTO selectedBooking)
            {
                if (MessageBox.Show($"Are you sure you want to delete Customer {selectedBooking.BookingReservationId}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                     _service.UpdateBooking(selectedBooking);
                    LoadData(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.", "Delete Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
    