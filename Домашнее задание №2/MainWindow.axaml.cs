using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Домашнее_задание__2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object? sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                Rectangle rect = this.GetControl<Rectangle>("filler");
                if (button != null && button.Background != null)
                {
                    IBrush? brush = button.Background;
                    rect.Fill = brush;

                }
                    
            }
        }
    }
}
