using ModernMessageBoxWPF.Enums;
using System.Windows.Input;

namespace ModernMessageBoxWPF.ViewModels
{
    public class ModernMessageBoxViewModel : ObservableObject
    {
        private string _title = "";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message = "";
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private MessageBoxStateType _type;
        public MessageBoxStateType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private ModernMessageBoxInputTypeEnum _inputType;
        public ModernMessageBoxInputTypeEnum InputType
        {
            get { return _inputType; }
            set { _inputType = value; }
        }

        private object _value;
        public object Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }


        public Action CloseAction { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private readonly TaskCompletionSource<object> _tcs = new();

        public ModernMessageBoxViewModel(string title, string message
            , MessageBoxStateType type
            , ModernMessageBoxInputTypeEnum inputType)
        {
            Title = title;
            Message = message;
            Type = type;
            InputType = inputType;  

            ConfirmCommand = new RelayCommand(OnConfirmCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
        }

        private void OnConfirmCommand()
        {
            var result = InputType == ModernMessageBoxInputTypeEnum.Normal 
                ? true : Value;
            _tcs.TrySetResult(result);
            CloseAction?.Invoke();
        }

        private void OnCancelCommand() 
        {
            object result = InputType == ModernMessageBoxInputTypeEnum.Normal
                ? false : null;
            _tcs.TrySetResult(result);
            CloseAction?.Invoke();
        }


        public Task<object> WaitForResultAsync() => _tcs.Task;
    }
}
