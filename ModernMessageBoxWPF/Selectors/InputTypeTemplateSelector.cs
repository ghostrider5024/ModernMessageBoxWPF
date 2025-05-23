using System.Windows.Controls;
using System.Windows;

using ModernMessageBoxWPF.Enums;

namespace ModernMessageBoxWPF.Selectors
{
    public class InputTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate InputTextTemplate { get; set; }
        public DataTemplate InputPasswordTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ModernMessageBoxInputTypeEnum type)
            {
                return type switch
                {
                    ModernMessageBoxInputTypeEnum.Normal => NormalTemplate,
                    ModernMessageBoxInputTypeEnum.InputText => InputTextTemplate,
                    ModernMessageBoxInputTypeEnum.InputPassword => InputPasswordTemplate,
                    _ => NormalTemplate
                };
            }

            return base.SelectTemplate(item, container);
        }
    }

}
