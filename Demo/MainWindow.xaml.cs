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

            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Info
                , ModernMessageBoxInputTypeEnum.Normal
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle()
                {
                    //CancelButtonStyle = cancelButtonStyle
                });
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Success
                , ModernMessageBoxInputTypeEnum.Normal
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle()
                {
                    //CancelButtonStyle = cancelButtonStyle
                });
            
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Warning
                , ModernMessageBoxInputTypeEnum.Normal
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle(),
               null);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Warning
                , ModernMessageBoxInputTypeEnum.Normal
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle(),
                 null);
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Warning
                , ModernMessageBoxInputTypeEnum.InputText
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle(),
                null);
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var cancelButtonStyle = FindResource("CancelButtonStyle") as Style;
            var result = await ModernMessageBoxManager
                .ShowDialogAsync(this
                , "Xác nhận"
                , "Bạn muốn thoát ứng dụng?"
                , MessageBoxStateType.Warning
                , ModernMessageBoxInputTypeEnum.InputPassword
                , new ModernMessageBoxWPF.Models.ModernMessageBoxStyle(),
                null);
        }
    }
}