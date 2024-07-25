using DataAccessLayer.DTO;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AddEditCustomerDialog.xaml
    /// </summary>
    public partial class AddEditCustomerDialog : Window
    {
        public CustomerDTO Customer { get; private set; }

        public AddEditCustomerDialog(CustomerDTO customer = null)
        {
            InitializeComponent();
            if (customer != null)
            {
                Customer = customer;
                txtFullName.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtEmail.Text = customer.EmailAddress;
                dpBirthday.SelectedDate = customer.CustomerBirthday;
                txtPassword.Text = customer.Password;
                if (customer.CCCDFaceFront != null)
                {
                    imgCCCDMatTruoc.Source = ByteArrayToImageSource(customer.CCCDFaceFront);
                }
                if (customer.CCCDBackSide != null)
                {
                    imgCCCDMatSau.Source = ByteArrayToImageSource(customer.CCCDBackSide);
                }
            }
            else
            {
                Customer = new CustomerDTO();
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
        /// <summary>
        /// Lưu khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text) &&
                !string.IsNullOrEmpty(txtTelephone.Text) &&
                !string.IsNullOrEmpty(txtEmail.Text) &&
                dpBirthday.SelectedDate != null &&
                !string.IsNullOrEmpty(txtPassword.Text))
            {
                // All fields are filled, proceed with assigning values
                Customer.CustomerFullName = txtFullName.Text;
                Customer.Telephone = txtTelephone.Text;
                Customer.EmailAddress = txtEmail.Text;
                Customer.CustomerBirthday = dpBirthday.SelectedDate;
                Customer.CustomerStatus = 1;
                Customer.Password = txtPassword.Text;
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
                DialogResult = true;
                Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Thông tin không được để trông.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        /// <summary>
        /// đóng màn hình cập nhật khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOpenCCCDMatTruoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Open a file dialog to select an image file
                OpenFileDialog openFileDialog = new OpenFileDialog();
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
                OpenFileDialog openFileDialog = new OpenFileDialog();
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
