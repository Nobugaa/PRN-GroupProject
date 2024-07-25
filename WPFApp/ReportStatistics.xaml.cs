using BusinessObjects;
using DataAccessLayer.DTO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for ProfileManagement.xaml
    /// </summary>
    public partial class ReportStatistics : UserControl
    {
        private readonly IRoomService _serviceR;
        private readonly ICustomerService _serviceC;
        private readonly IBookingHistoryService _serviceB;
        public ObservableCollection<ReportStatisticDTO> Statistics { get; set; }

        public ReportStatistics()
        {
            InitializeComponent();
            _serviceR = ((App)Application.Current).ServiceProvider.GetRequiredService<IRoomService>() ?? throw new ArgumentNullException(nameof(RoomService));
            _serviceC = ((App)Application.Current).ServiceProvider.GetRequiredService<ICustomerService>() ?? throw new ArgumentNullException(nameof(CustomerService));
            _serviceB = ((App)Application.Current).ServiceProvider.GetRequiredService<IBookingHistoryService>() ?? throw new ArgumentNullException(nameof(BookingHistoryService));
            LoadStatistics();
            dgReportStatistics.ItemsSource = Statistics;
        }
        /// <summary>
        /// Lấy báo cáo 
        /// </summary>
        private void LoadStatistics()
        {
            Statistics = new ObservableCollection<ReportStatisticDTO>
            {
                new ReportStatisticDTO { Statistic = "Total Bookings", Value = _serviceB.CountBookings().ToString() },
                new ReportStatisticDTO { Statistic = "Active Customers", Value = _serviceC.CountCustomers().ToString() },
                new ReportStatisticDTO { Statistic = "Rooms Available", Value = _serviceR.CountRooms().ToString() },
                new ReportStatisticDTO { Statistic = "Revenue This Month", Value = "$" + _serviceB.CalcRevenue().ToString() }
            };
        }
    }
}
