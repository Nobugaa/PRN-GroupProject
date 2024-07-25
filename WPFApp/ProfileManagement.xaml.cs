using BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interface;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for ProfileManagement.xaml
    /// </summary>
    public partial class ProfileManagement : UserControl
    {
        private readonly ICustomerService _customerService;
        public Customer Customer;
        public ProfileManagement()
        {
            InitializeComponent();
            _customerService = ((App)Application.Current).ServiceProvider.GetRequiredService<ICustomerService>() ?? throw new ArgumentNullException(nameof(CustomerService));
            Loaded += LoadData;
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = Customer.CustomerFullName;
            txtEmail.Text = Customer.EmailAddress;
            txtTelephone.Text = Customer.Telephone;
            pwdPassword.Password = Customer.Password;
            dpBirthday.SelectedDate = Customer.CustomerBirthday;
            if (Customer.CCCDFaceFront != null)
            {
                imgCCCDMatTruoc.Source = ByteArrayToImageSource(Customer.CCCDFaceFront);
            }
            if (Customer.CCCDBackSide != null)
            {
                imgCCCDMatSau.Source = ByteArrayToImageSource(Customer.CCCDBackSide);
            }
        }

        private  void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer.CustomerFullName = txtFullName.Text;
            Customer.Telephone = txtTelephone.Text;
            Customer.CustomerBirthday = dpBirthday.SelectedDate;
            Customer.Password = pwdPassword.Password;
            if (imgCCCDMatTruoc.Source != null)
            {
                BitmapSource bitmapSource = imgCCCDMatTruoc.Source as BitmapSource;
                Customer.CCCDFaceFront = ImageToByteArray(bitmapSource);

            }
            if (imgCCCDMatSau.Source != null)
            {
                BitmapSource bitmapSource = imgCCCDMatSau.Source as BitmapSource;
                Customer.CCCDBackSide = ImageToByteArray(bitmapSource);
            }

            bool success =  _customerService.UpdateProfile(Customer);

            if (success)
            {
                System.Windows.MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to update profile.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            txtFullName.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            pwdPassword.Password = string.Empty;
            dpBirthday.SelectedDate = null;
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
        /// <summary>
        ///  chuyển ảnh từ byte[] sang BitmapImage để gắn lên control image
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        private BitmapImage ByteArrayToImageSource(byte[] imageData)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            pwdPassword.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Visible;
            txtPassword.Text = pwdPassword.Password;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            pwdPassword.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
            pwdPassword.Password = txtPassword.Text;
        }
        /// <summary>
        /// Chuyển đổi anh từ Bitmap sang byte[] để lưu vào csdl
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <returns></returns>
        private byte[] ImageToByteArray(BitmapSource bitmapSource)
        {
            byte[] imageData;

            // Use a BitmapEncoder to encode the BitmapSource to a byte array
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                imageData = memoryStream.ToArray();
            }

            return imageData;
        }
        private void btnOpenCCCDMatTruoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Open a file dialog to select an image file
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Load the image into the Image control
                    string filePath = openFileDialog.FileName;
                    BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                    imgCCCDMatTruoc.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void btnOpenCCCDMatSau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open a file dialog to select an image file
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Load the image into the Image control
                    string filePath = openFileDialog.FileName;
                    BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                    imgCCCDMatSau.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
