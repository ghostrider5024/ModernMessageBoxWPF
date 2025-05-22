using ModernMessageBoxWPF.Enums;
using System.Windows.Input;

namespace ModernMessageBoxWPF.ViewModels
{
    public class ModernMessageBoxViewModel : ObservableObject
    {
        private string _title = "";
        private string _message = "";
        private DialogType _type;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DialogType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public Action CloseAction { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private readonly TaskCompletionSource<bool> _tcs = new();

        public ModernMessageBoxViewModel(string title, string message, DialogType type)
        {
            Title = title;
            Message = message;
            Type = type;

            ConfirmCommand = new RelayCommand(OnConfirmCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
        }

        private void OnConfirmCommand()
        {
            _tcs.TrySetResult(true);
            CloseAction?.Invoke();
        }

        private void OnCancelCommand() 
        {
            _tcs.TrySetResult(false);
            CloseAction?.Invoke();
        }


        public Task<bool> WaitForResultAsync() => _tcs.Task;
    }
}
