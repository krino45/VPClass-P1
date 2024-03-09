using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System.Threading.Tasks;

namespace WeatherApp
{
    public partial class DialogWindow : Window
    {
        private TaskCompletionSource<string?> _taskCompletionSource;
        public DialogWindow()
        {
            InitializeComponent();
            _taskCompletionSource = new TaskCompletionSource<string?>();
        }

        private void ConfirmDialogButtonTapped(object sender, TappedEventArgs e)
        {
            _taskCompletionSource.SetResult(_textBox.Text);
            Close();
        }
        private void CloseDialogButtonTapped(object? sender, TappedEventArgs e)
        {
            this.Close("Close");
        }

        public Task<string?> ShowAsync()
        {
            Show();
            return _taskCompletionSource.Task;
        }
    }
}
