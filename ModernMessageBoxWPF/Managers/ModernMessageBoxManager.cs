using System.Windows;

using ModernMessageBoxWPF.ViewModels;
using ModernMessageBoxWPF.Enums;
using ModernMessageBoxWPF.UI;
using ModernMessageBoxWPF.Models;

namespace ModernMessageBoxWPF.Managers
{
    public static class ModernMessageBoxManager
    {
        private static ModernMessageBox? dialogHolder;
        public static async Task<object> ShowDialogAsync(Window? owner
            , string title, string message
            , MessageBoxStateType type = MessageBoxStateType.Info
            , ModernMessageBoxInputTypeEnum inputType = ModernMessageBoxInputTypeEnum.Normal
            , ModernMessageBoxStyle? style = null
            , Func<MessageBoxStateType, string>? BuildThemeURL = null)
        {
            var vm = new ModernMessageBoxViewModel(title, message
                , type, inputType);
            var dialog = new ModernMessageBox(type);

            dialog.SetStyle(style);
            dialog.MessageBoxStateType = type;
            dialog.BuildThemeURL = BuildThemeURL;

            dialog.Owner = owner;
            dialog.DataContext = vm;
            
            vm.CloseAction = () => dialog.Close();
            dialog.ShowDialog(); // blocking nhưng ta đợi task
            var result = await vm.WaitForResultAsync();
            return result;
        }
    }

}
