using BusinessObjects;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Customer customer;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //DataContext = customer;
            if (profileManagement != null)
            {
                profileManagement.Customer = customer;
            }

            if (bookingHistory != null)
            {
                bookingHistory.Customer = customer;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}