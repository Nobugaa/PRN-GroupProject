using DataAccessLayer.DTO;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : UserControl
    {
        private readonly IRoomService _service;
        public RoomManagement()
        {
            InitializeComponent();
            _service = ((App)Application.Current).ServiceProvider.GetRequiredService<IRoomService>() ?? throw new ArgumentNullException(nameof(RoomService));
            LoadData();
        }
        /// <summary>
        /// Lấy danh sách phòng
        /// </summary>
        private void LoadData()
        {
            dgRooms.ItemsSource = null;
            var rooms = _service.GetRooms(r => r.RoomNumber.Contains(txtSearch.Text));
            dgRooms.ItemsSource = rooms;
        }
        /// <summary>
        /// Tìm kiếm phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                try
                {
                    List<RoomDTO> rooms = _service.GetRooms(r => r.RoomNumber.Contains(txtSearch.Text));
                    // Ensure UI update happens on the main thread
                    Dispatcher.Invoke(() =>
                    {
                        dgRooms.ItemsSource = null;
                        dgRooms.ItemsSource = rooms;
                    });
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void dgRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// thêm phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditRoomDialog = new AddEditRoomDialog();
            if (addEditRoomDialog.ShowDialog() == true)
            {
                var newRoom = addEditRoomDialog.Room;
                _service.AddRoom(newRoom);
                LoadData();
            }
        }
        /// <summary>
        /// sửa phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomDTO selectedRoom)
            {
                var addEditRoomDialog = new AddEditRoomDialog(selectedRoom);
                if (addEditRoomDialog.ShowDialog() == true)
                {
                    var updatedRoom = addEditRoomDialog.Room;
                     _service.UpdateRoom(updatedRoom);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to edit.", "Edit Room", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// xóa phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomDTO selectedRoom)
            {
                if (MessageBox.Show($"Are you sure you want to delete Room {selectedRoom.RoomNumber}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    selectedRoom.RoomStatus = 0;
                     _service.UpdateRoom(selectedRoom);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Delete Room", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
