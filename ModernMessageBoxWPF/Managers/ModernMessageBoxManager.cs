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
        public static async Task<bool> ShowDialogAsync(Window? owner
            , string title, string message, DialogType type = DialogType.Info
            , ModernMessageBoxStyle? style = null)
        {
            var vm = new ModernMessageBoxViewModel(title, message, type);
            var dialog = dialogHolder ?? new ModernMessageBox();

            dialog.SetStyle(style);

            dialog.Owner = owner;
            dialog.DataContext = vm;
            
            vm.CloseAction = () => dialog.Close();
            dialog.ShowDialog(); // blocking nhưng ta đợi task
            var result = await vm.WaitForResultAsync();
            return result;
        }
    }

}
