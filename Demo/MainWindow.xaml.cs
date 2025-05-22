using ModernMessageBoxWPF.Enums;
using ModernMessageBoxWPF.Managers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this, "Xác nhận", "Bạn muốn thoát ứng dụng?", DialogType.Warning);
            if (result)
            {
                Application.Current.Shutdown();
            }
        }
    }
}