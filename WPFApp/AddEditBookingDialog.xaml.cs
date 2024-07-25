using BusinessObjects;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddEditBookingDialog.xaml
    /// </summary>
    public partial class AddEditBookingDialog : Window
    {
        private readonly IBookingHistoryService _bookingService;
        private readonly IRoomService _roomService;
        public BookingDTO Booking { get; private set; }
        public Customer Customer { get; set; }
        private RoomDTO room { get; set; }
        private List<BookingDetailsDTO> bookingDetailsDTOs { get; set; }

        public AddEditBookingDialog()
        {
            InitializeComponent();
            _bookingService = ((App)Application.Current).ServiceProvider.GetRequiredService<IBookingHistoryService>() ?? throw new ArgumentNullException(nameof(BookingHistoryService));
            _roomService = ((App)Application.Current).ServiceProvider.GetRequiredService<IRoomService>() ?? throw new ArgumentNullException(nameof(RoomService));
            if (Booking is null)
            {
                Booking = new BookingDTO();
                bookingDetailsDTOs = new List<BookingDetailsDTO>();
            }
            else
            {
                bookingDetailsDTOs = Booking.BookingDetails;
            }
            Loaded += LoadRooms;
            Loaded += LoadBookingDetails;
        }
        /// <summary>
        /// Load danh sách phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadRooms(object sender, RoutedEventArgs e)
        {
            var data = _roomService.GetRooms(r => r.RoomStatus == 1);
            dgAvailableRooms.ItemsSource = data;
        }
        /// <summary>
        /// Load danh sách đặt phòng chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadBookingDetails(object sender, RoutedEventArgs e)
        {
            dgBookingDetails.ItemsSource = null;
            dgBookingDetails.ItemsSource = bookingDetailsDTOs;

            txtTotalPrice.Text = CalculateTotalPrice().ToString();
        }
        /// <summary>
        /// Ấn nút hủy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        /// <summary>
        /// Ấn nút thêm phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgAvailableRooms.SelectedItems.Count > 0)
            {
                DateTime? startDate = dpStartDate.SelectedDate;
                DateTime? endDate = dpEndDate.SelectedDate;

                if (!startDate.HasValue || !endDate.HasValue)
                {
                    MessageBox.Show("Please select both start and end dates.");
                    return;
                }

                if (startDate > endDate)
                {
                    MessageBox.Show("Invalid date range. Start date cannot be after end date.");
                    return;
                }

                room = (RoomDTO)dgAvailableRooms.SelectedItem;

                int daysDifference = (endDate.Value - startDate.Value).Days;
                var TotalPrice = (daysDifference * room.RoomPricePerDay);

                var bookingDetail = new BookingDetailsDTO()
                {
                    Room = room,
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    ActualPrice = TotalPrice,
                };
                bookingDetailsDTOs.Add(bookingDetail);
                LoadBookingDetails(sender, e);
            }
        }
        /// <summary>
        /// Tính tổng tiền phòng
        /// </summary>
        /// <returns></returns>
        private decimal? CalculateTotalPrice()
        {
            decimal? total = 0;

            foreach (var item in bookingDetailsDTOs)
            {
                total += item.ActualPrice;
            }

            return total;
        }
        /// <summary>
        /// sự kiện khi chọn phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgAvailableRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAvailableRooms.SelectedItems.Count > 0)
            {
                room = (RoomDTO)dgAvailableRooms.SelectedItem;
                txtRoomId.Text = room.RoomId.ToString();
                txtRoomNumber.Text = room.RoomNumber;
                txtNumPerson.Text = room.RoomMaxCapacity.ToString();
                txtPrice.Text = room.RoomPricePerDay.ToString();
            }
        }
        /// <summary>
        /// Sự kiện chọn ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpEndDate.SelectedDate.Value;

                int daysDifference = (endDate - startDate).Days;
                var TotalPrice = (daysDifference * room.RoomPricePerDay);
                txtTotalPrice.Text = TotalPrice.ToString();

                if (startDate < DateTime.Today || endDate < DateTime.Today)
                {
                    txtTotalPrice.Text = "0";
                }
            }
        }
        /// <summary>
        /// Xóa thông tin chi tiết đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingDetails.SelectedItems.Count > 0)
            {
                var bookingdetail = (BookingDetailsDTO)dgBookingDetails.SelectedItem;
                bookingDetailsDTOs.Remove(bookingdetail);
                LoadBookingDetails(sender, e);
            }
        }
        /// <summary>
        /// chọn thông tin chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBookingDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookingDetails.SelectedItems.Count > 0)
            {
                var bookingdetail = (BookingDetailsDTO)dgBookingDetails.SelectedItem;
                txtRoomIdDB.Text = bookingdetail.Room.RoomId.ToString();
                txtRoomNumDB.Text = bookingdetail.Room.RoomNumber;
                dpStartDateDB.SelectedDate = bookingdetail.StartDate;
                dpEndDateDB.SelectedDate = bookingdetail.EndDate;
                txtActualPrice.Text = bookingdetail.ActualPrice.ToString();
            }
        }
        /// <summary>
        /// lưu thông tin đặt phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Booking.BookingDate = DateTime.Today;
            Booking.TotalPrice = System.Convert.ToDecimal(txtTotalPrice.Text);
            Booking.CustomerId = Customer.CustomerId;
            Booking.BookingStatus = 1;
            Booking.BookingDetails = bookingDetailsDTOs;
            Booking.CalculateTotalPrice();

            _bookingService.CreateBooking(Booking);

            DialogResult = true;
            Close();
        }
    }
}
