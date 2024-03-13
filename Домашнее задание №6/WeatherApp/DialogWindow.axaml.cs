using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System.Threading.Tasks;
using Avalonia.Controls.Templates;
using Avalonia;
using Avalonia.Platform;
using System;

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
            this.Width = 300;
            this.Height = 180;
            Console.WriteLine((Screens.Primary.WorkingArea.Width).ToString() + (Screens.Primary.WorkingArea.Height).ToString());
            Position = new PixelPoint((Screens.Primary.WorkingArea.Width/2) - 300, (Screens.Primary.WorkingArea.Height / 2) - 180);
            return _taskCompletionSource.Task;
        }
    }
}
