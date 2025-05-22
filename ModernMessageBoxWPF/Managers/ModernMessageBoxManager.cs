using System.Windows;

using ModernMessageBoxWPF.ViewModels;
using ModernMessageBoxWPF.Enums;
using ModernMessageBoxWPF.UI;

namespace ModernMessageBoxWPF.Managers
{
    public static class ModernMessageBoxManager
    {
        private static ModernMessageBox? dialogHolder;
        public static async Task<bool> ShowDialogAsync(Window? owner, string title, string message, DialogType type = DialogType.Info)
        {
            var vm = new ModernMessageBoxViewModel(title, message, type);
            var dialog = dialogHolder ?? new ModernMessageBox();

            dialog.Owner = owner;
            dialog.DataContext = vm;
            
            vm.CloseAction = () => dialog.Close();
            dialog.ShowDialog(); // blocking nhưng ta đợi task
            var result = await vm.WaitForResultAsync();
            return result;
        }
    }

}
