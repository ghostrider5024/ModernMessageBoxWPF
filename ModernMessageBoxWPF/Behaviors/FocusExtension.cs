using System.Windows.Threading;
using System.Windows.Input;
using System.Windows;

namespace ModernMessageBoxWPF.Behaviors
{
    public static class FocusExtension
    {
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused",
                typeof(bool),
                typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        public static bool GetIsFocused(DependencyObject obj) => (bool)obj.GetValue(IsFocusedProperty);
        public static void SetIsFocused(DependencyObject obj, bool value) => obj.SetValue(IsFocusedProperty, value);

        public static readonly DependencyProperty FocusWhenEnabledProperty =
            DependencyProperty.RegisterAttached(
                "FocusWhenEnabled",
                typeof(bool),
                typeof(FocusExtension),
                new UIPropertyMetadata(false));

        public static bool GetFocusWhenEnabled(DependencyObject obj) => (bool)obj.GetValue(FocusWhenEnabledProperty);
        public static void SetFocusWhenEnabled(DependencyObject obj, bool value) => obj.SetValue(FocusWhenEnabledProperty, value);

        public static readonly DependencyProperty DelayFocusProperty =
            DependencyProperty.RegisterAttached(
                "DelayFocus",
                typeof(TimeSpan),
                typeof(FocusExtension),
                new UIPropertyMetadata(TimeSpan.Zero)); // mặc định 0 giây

        public static TimeSpan GetDelayFocus(DependencyObject obj) => (TimeSpan)obj.GetValue(DelayFocusProperty);
        public static void SetDelayFocus(DependencyObject obj, TimeSpan value) => obj.SetValue(DelayFocusProperty, value);

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement uie)
                return;

            if ((bool)e.NewValue)
            {
                if (GetFocusWhenEnabled(uie))
                {
                    uie.IsEnabledChanged += Uie_IsEnabledChanged;
                    return;
                }

                FocusElement(uie);
            }
        }

        private static void Uie_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not UIElement uie)
                return;

            if (uie.IsEnabled)
            {
                if (GetIsFocused(uie))
                {
                    FocusElement(uie);
                }
            }
        }

        private static void FocusElement(UIElement uie)
        {
            TimeSpan delay = GetDelayFocus(uie);

            uie.Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(async () =>
                {
                    if (delay > TimeSpan.Zero)
                        await Task.Delay(delay);

                    uie.Focus();
                    Keyboard.Focus(uie);
                }));
        }
    }
}
