using ModernMessageBoxWPF.Enums;
using System.Globalization;
using System.Windows.Data;

namespace ModernMessageBoxWPF.Converters
{
    public class ModernMessgeBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value switch
            {
                MessageBoxStateType.Info => "ℹ️",
                MessageBoxStateType.Warning => "⚠️",
                MessageBoxStateType.Error => "❌",
                MessageBoxStateType.Success => "✅",
                _ => ""
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
