using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

using ModernMessageBoxWPF.Enums;
using ModernMessageBoxWPF.Models;

namespace ModernMessageBoxWPF.UI
{
    /// <summary>
    /// Interaction logic for ModernMessageBox.xaml
    /// </summary>
    public partial class ModernMessageBox : Window
    {
        #region DP
        public ModernMessageBoxAnimationTypeEnum AnimationType
        {
            get { return (ModernMessageBoxAnimationTypeEnum)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register(nameof(AnimationType)
                , typeof(ModernMessageBoxAnimationTypeEnum)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(ModernMessageBoxAnimationTypeEnum.SlideFromTop
                    , OnAnimationTypeChanged));
        private static void OnAnimationTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var component = d as ModernMessageBox;
            if (component != null)
            {
                component.SetInitialAnimationState();
            }
        }

        public TimeSpan AnimationInDuration
        {
            get { return (TimeSpan)GetValue(AnimationInDurationProperty); }
            set { SetValue(AnimationInDurationProperty, value); }
        }
        public static readonly DependencyProperty AnimationInDurationProperty =
            DependencyProperty.Register(nameof(AnimationInDuration)
                , typeof(TimeSpan)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(TimeSpan.FromMilliseconds(250)));

        public TimeSpan AnimationOutDuration
        {
            get { return (TimeSpan)GetValue(AnimationOutDurationProperty); }
            set { SetValue(AnimationOutDurationProperty, value); }
        }
        public static readonly DependencyProperty AnimationOutDurationProperty =
            DependencyProperty.Register(nameof(AnimationOutDuration)
                , typeof(TimeSpan)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(TimeSpan.FromMilliseconds(250)));

        public Style? RootBorderStyle
        {
            get { return (Style)GetValue(RootBorderStyleProperty); }
            set { SetValue(RootBorderStyleProperty, value); }
        }
        public static readonly DependencyProperty RootBorderStyleProperty =
            DependencyProperty.Register(nameof(RootBorderStyle)
                , typeof(Style)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(default));

        public Style? CancelButtonStyle
        {
            get { return (Style)GetValue(CancelButtonStyleProperty); }
            set { SetValue(CancelButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty CancelButtonStyleProperty =
            DependencyProperty.Register(nameof(CancelButtonStyle)
                , typeof(Style)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(default));

        public Style? ConfirmButtonStyle
        {
            get { return (Style)GetValue(ConfirmButtonStyleProperty); }
            set { SetValue(ConfirmButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty ConfirmButtonStyleProperty =
            DependencyProperty.Register(nameof(ConfirmButtonStyle)
                , typeof(Style)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(default));


        public MessageBoxStateType MessageBoxStateType
        {
            get { return (MessageBoxStateType)GetValue(MessageBoxStateTypeProperty); }
            set { SetValue(MessageBoxStateTypeProperty, value); }
        }
        public static readonly DependencyProperty MessageBoxStateTypeProperty =
            DependencyProperty.Register(nameof(MessageBoxStateType)
                , typeof(MessageBoxStateType)
                , typeof(ModernMessageBox)
                , new PropertyMetadata(MessageBoxStateType.Info, OnMessageBoxStateTypeChanged));

        private static void OnMessageBoxStateTypeChanged(DependencyObject d
            , DependencyPropertyChangedEventArgs e)
        {
            var component = d as ModernMessageBox;
            if (component != null)
            {
                var type = (MessageBoxStateType) e.NewValue;
                component.Resources.MergedDictionaries.Clear();
                component.Resources.MergedDictionaries.Add(component.GetThemeFromType(type));
            }
        }

        public Func<MessageBoxStateType, string>? BuildThemeURL { get; set; }

        #endregion DP
        private void InitTheme()
        {
            Resources.MergedDictionaries.Add(GetThemeFromType(MessageBoxStateType));
        }

        private ResourceDictionary GetThemeFromType(MessageBoxStateType type)
        {
            var themeURL = BuildThemeURL != null 
                ? BuildThemeURL(type) 
                : BuildDefaultThemeURL(type);
           
            var dict = new ResourceDictionary
            {
                Source = new Uri(themeURL, UriKind.Absolute)
            };
            return dict;
        }

        private string BuildDefaultThemeURL(MessageBoxStateType type)
        {
            string file = type.ToString() + "Theme.xaml";
            return $"pack://application:,,,/ModernMessageBoxWPF;component/Themes/{file}";
        }

        private bool _allowClose = false;

        private Dictionary<string, Storyboard> _cachedStoryboards = new();

        public ModernMessageBox()
        {
            InitTheme();
            InitializeComponent();
            Scale.ScaleX = 0.8;
            Scale.ScaleY = 0.8; 
        }

        public ModernMessageBox(MessageBoxStateType messageBoxStateType)
        {
            MessageBoxStateType = messageBoxStateType;
            InitTheme();
            InitializeComponent();
            Scale.ScaleX = 0.8;
            Scale.ScaleY = 0.8;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetInitialAnimationState();
            if (this.Owner != null)
            {
                SetSizeWindow();
                SetPositionWindow();
            }
            RunAnimationIn();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_allowClose) return;
            e.Cancel = true;

            RunAnimationOut();
        }

        #region Hàm gán giá trị mặc định trước khi chạy hiệu ứng
        
        public void SetStyle(ModernMessageBoxStyle? style)
        {
            RootBorderStyle = style?.RootBorderStyle
                ?? FindResource("DefaultRootStyle") as Style;
            CancelButtonStyle = style?.CancelButtonStyle 
                ?? FindResource("DefaultCancelButtonStyle") as Style;
            ConfirmButtonStyle = style?.ConfirmButtonStyle
                ?? FindResource("DefaultConfirmButtonStyle") as Style;
        }

        public void SetInitialAnimationState()
        {
            switch (AnimationType)
            {
                case ModernMessageBoxAnimationTypeEnum.Fade:
                    Root.Opacity = 0;
                    break;

                case ModernMessageBoxAnimationTypeEnum.Zoom:
                    Root.Opacity = 0;
                    Scale.ScaleX = 0.8;
                    Scale.ScaleY = 0.8;
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromBottom:
                    Root.Opacity = 0;
                    Root.RenderTransform = new TranslateTransform(0, 100);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromTop:
                    Root.Opacity = 0;
                    Root.RenderTransform = new TranslateTransform(0, -100);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromRight:
                    Root.Opacity = 0;
                    Root.RenderTransform = new TranslateTransform(100, 0);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromLeft:
                    Root.Opacity = 0;
                    Root.RenderTransform = new TranslateTransform(-100, 0);
                    break;
               

                case ModernMessageBoxAnimationTypeEnum.Flip:
                    Root.Opacity = 0;
                    Root.RenderTransformOrigin = new Point(0.5, 0.5);
                    Root.RenderTransform = new RotateTransform(90, Root.ActualWidth / 2, Root.ActualHeight / 2);
                    break;
              
            }
        }


        #endregion Hàm gán giá trị mặc định trước khi chạy hiệu ứng


        #region Hiệu ứng show

        private void RunAnimationIn()
        {
            if (Root == null) return;

            var sb = CreateStoryboardIn();
            sb.Begin(Root, true);
        }

        private Storyboard CreateStoryboardIn()
        {
            string key = $"In_{AnimationType}";
            if (_cachedStoryboards.TryGetValue(key, out var cached))
                return cached;

            var sb = new Storyboard { Duration = AnimationInDuration };

            switch (AnimationType)
            {
                case ModernMessageBoxAnimationTypeEnum.Fade:
                    AddOpacityAnimation(sb, 0, 1);
                    break;

                case ModernMessageBoxAnimationTypeEnum.Zoom:
                    AddOpacityAnimation(sb, 0, 1);
                    AddScaleAnimation(sb, 0.8, 1);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromBottom:
                case ModernMessageBoxAnimationTypeEnum.SlideFromTop:
                    double fromY = AnimationType == ModernMessageBoxAnimationTypeEnum.SlideFromBottom ? 100 : -100;
                    var verticalSlideAnim = new DoubleAnimation(fromY, 0, sb.Duration) 
                    { 
                        EasingFunction = new BackEase() 
                        { 
                            EasingMode = EasingMode.EaseOut,
                            Amplitude = 0.2
                        } 
                    };
                    Storyboard.SetTargetProperty(verticalSlideAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                    AddOpacityAnimation(sb, 0, 1);
                    sb.Children.Add(verticalSlideAnim);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromLeft:
                case ModernMessageBoxAnimationTypeEnum.SlideFromRight:
                    {
                        double fromX = AnimationType == ModernMessageBoxAnimationTypeEnum.SlideFromRight ? 100 : -100;
                        var horizontalSlideAnim = new DoubleAnimation(fromX, 0, sb.Duration)
                        {
                            EasingFunction = new BackEase()
                            {
                                EasingMode = EasingMode.EaseOut,
                                Amplitude = 0.2
                            }
                        };
                        Storyboard.SetTargetProperty(horizontalSlideAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                        AddOpacityAnimation(sb, 0, 1);
                        sb.Children.Add(horizontalSlideAnim);
                        break;
                    }

                case ModernMessageBoxAnimationTypeEnum.Flip:
                    var flip = new DoubleAnimation(90, 0, sb.Duration) { EasingFunction = new CircleEase() };
                    Storyboard.SetTargetProperty(flip, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                    sb.Children.Add(flip);
                    AddOpacityAnimation(sb, 0, 1);
                    break;

            }

            _cachedStoryboards[key] = sb;
            return sb;
        }

        #endregion Hiệu ứng show

        private void RunAnimationOut()
        {
            if (Root == null) return;

            var sb = CreateStoryboardOut();
            sb.Completed += (_, _) => { _allowClose = true; Close(); };
            sb.Begin(Root, true);
        }
        private Storyboard CreateStoryboardOut()
        {
            string key = $"Out_{AnimationType}";
            if (_cachedStoryboards.TryGetValue(key, out var cached))
                return cached;

            var sb = new Storyboard { Duration = AnimationOutDuration };

            switch (AnimationType)
            {
                case ModernMessageBoxAnimationTypeEnum.Fade:
                    AddOpacityAnimation(sb, 1, 0);
                    break;

                case ModernMessageBoxAnimationTypeEnum.Zoom:
                    AddOpacityAnimation(sb, 1, 0);
                    AddScaleAnimation(sb, 1, 0.8);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromBottom:
                case ModernMessageBoxAnimationTypeEnum.SlideFromTop:
                    double toY = AnimationType == ModernMessageBoxAnimationTypeEnum.SlideFromBottom ? 100 : -100;
                    var verticalSlideAnim = new DoubleAnimation(0, toY, sb.Duration) 
                    { 
                        EasingFunction = new BackEase()
                        {
                            Amplitude = 0.2,
                            EasingMode = EasingMode.EaseIn
                        }
                    };
                    Storyboard.SetTargetProperty(verticalSlideAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                    sb.Children.Add(verticalSlideAnim);
                    AddOpacityAnimation(sb, 1, 0);
                    break;

                case ModernMessageBoxAnimationTypeEnum.SlideFromRight:
                case ModernMessageBoxAnimationTypeEnum.SlideFromLeft:
                    double toX = AnimationType == ModernMessageBoxAnimationTypeEnum.SlideFromRight ? 100 : -100;
                    var horizontalSlideAmin = new DoubleAnimation(0, toX, sb.Duration)
                    {
                        EasingFunction = new BackEase()
                        {
                            Amplitude = 0.2,
                            EasingMode = EasingMode.EaseIn
                        }
                    };
                    Storyboard.SetTargetProperty(horizontalSlideAmin, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                    sb.Children.Add(horizontalSlideAmin);
                    AddOpacityAnimation(sb, 1, 0);
                    break;

                case ModernMessageBoxAnimationTypeEnum.Flip:
                    var flip = new DoubleAnimation(0, -180, sb.Duration) 
                    { 
                        EasingFunction = new CircleEase()
                        {
                            EasingMode = EasingMode.EaseIn
                        }
                    };
                    Storyboard.SetTargetProperty(flip, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                    sb.Children.Add(flip);
                    AddOpacityAnimation(sb, 1, 0);
                    break;
            }

            _cachedStoryboards[key] = sb;
            return sb;
        }

        private void AddOpacityAnimation(Storyboard sb, double from, double to)
        {
            var opacityAnim = new DoubleAnimation(from, to, sb.Duration)
            {
                EasingFunction = new SineEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            };
            Storyboard.SetTargetProperty(opacityAnim, new PropertyPath("Opacity"));
            sb.Children.Add(opacityAnim);
        }

        private void AddScaleAnimation(Storyboard sb, double from, double to)
        {
            var x = new DoubleAnimation(from, to, sb.Duration);
            Storyboard.SetTarget(x, Scale);
            Storyboard.SetTargetProperty(x, new PropertyPath(ScaleTransform.ScaleXProperty));
            var y = new DoubleAnimation(from, to, sb.Duration);
            Storyboard.SetTarget(y, Scale);
            Storyboard.SetTargetProperty(y, new PropertyPath(ScaleTransform.ScaleYProperty));

            sb.Children.Add(x);
            sb.Children.Add(y);
        }

        private void SetSizeWindow()
        {
            // Chỉnh size của cửa sổ bằng với cửa sổ chứa
            this.Width = this.Owner.ActualWidth;
            this.Height = this.Owner.ActualHeight;
        }

        private void SetPositionWindow()
        {
            var dpiX = 1.0;
            var dpiY = 1.0;

            var source = PresentationSource.FromVisual(this.Owner);
            if (source?.CompositionTarget != null)
            {
                dpiX = source.CompositionTarget.TransformToDevice.M11;
                dpiY = source.CompositionTarget.TransformToDevice.M22;
            }

            // Điều chỉnh lại vị trí cho trùng với cửa sổ chứa
            var screenPos = this.Owner.PointToScreen(new Point(0, 0));
            this.Left = screenPos.X / dpiX;
            this.Top = screenPos.Y / dpiY;
        }

    }
}
