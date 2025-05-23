using System.Windows;

using ModernMessageBoxWPF.Managers;
using ModernMessageBoxWPF.Enums;

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
            var cancelButtonStyle =FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Success
                , ModernMessageBoxInputTypeEnum.InputText
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle()
                {
                    //CancelButtonStyle = cancelButtonStyle
                });
            var temp = 2;
            //if (result)
            //{
            //    Application.Current.Shutdown();
            //}
        }
    }
}