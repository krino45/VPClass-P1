using Avalonia;
using Avalonia.Controls;
using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class TextInputPromptDialog : Window
    {
        private TextBox _textBox;
        private Button _okButton;
        private Button _cancelButton;
        private TaskCompletionSource<string?> _taskCompletionSource;

        public TextInputPromptDialog()
        {
            InitializeComponents();
            _taskCompletionSource = new TaskCompletionSource<string?>();
        }

        private void InitializeComponents()
        {
            Title = "Input Prompt";
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _textBox = new TextBox();
            _okButton = new Button { Content = "OK" };
            _cancelButton = new Button { Content = "Cancel" };

            _okButton.Click += OkButton_Click;
            _cancelButton.Click += CancelButton_Click;

            var stackPanel = new StackPanel();
            stackPanel.Children.Add(new TextBlock { Text = "Enter text:" });
            stackPanel.Children.Add(_textBox);
            stackPanel.Children.Add(_okButton);
            stackPanel.Children.Add(_cancelButton);

            Content = stackPanel;
        }

        private void OkButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _taskCompletionSource.SetResult(_textBox.Text);
            Close();
        }

        private void CancelButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _taskCompletionSource.SetResult(null);
            Close();
        }

        public Task<string?> ShowAsync()
        {
            Show();
            return _taskCompletionSource.Task;
        }
    }
}
