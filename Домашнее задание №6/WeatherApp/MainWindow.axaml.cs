using Avalonia.Controls;
using Avalonia.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.Threading;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loop();
            upDown2.Minimum = Screens.Primary.WorkingArea.Width / 4;
            upDown1.Minimum = Screens.Primary.WorkingArea.Height / 4;
            upDown2.Maximum = Screens.Primary.WorkingArea.Width;
            upDown1.Maximum = Screens.Primary.WorkingArea.Height;
        }


        private async void Loop()
        {
            if (!File.Exists(@".\files\geocode.json"))
            {
                this.WindowState = WindowState.Minimized;
                CallDialogWindow();

            }
            await Task.Run(async () =>
            {
                await Task.Delay(8000);
                while (true)
                {

                    while (MakeRequest.workflag || DataContextForWeatherApp.workflag) { Console.WriteLine("loop blocked"); await Task.Delay(5000); }
                    Console.WriteLine("try update");
                    if (File.Exists(@".\files\geocode.json"))
                    {
                        await MakeRequest.Make_a_Request_Geocode();
                        Console.WriteLine("made a request wia inf loop");
                    }
                    Console.WriteLine("waiting 10...");
                    await Task.Delay(10000);
                }
            });
            
        }

        private void CallDialogWindow()
        {
            DialogWindow(null, null);
        }
  
        private void SidePanelOpener(object? sender, TappedEventArgs? e)
        {
            if (SidePanel.IsPaneOpen)
            {
                SidePanel.IsPaneOpen = false;
            }
            else
            {
                SidePanel.IsPaneOpen = true;
            }
        }

        private void ApplySettings(object? sender, RoutedEventArgs e)
        {
            if (rad_b_1_1.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTheme((int)DataContextForWeatherApp.ThemeSetting.AUTO);
            }
            else if (rad_b_1_2.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTheme((int)DataContextForWeatherApp.ThemeSetting.NIGHT);
            }
            else if (rad_b_1_3.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTheme((int)DataContextForWeatherApp.ThemeSetting.DAY);
            }
            else if (rad_b_1_4.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTheme((int)DataContextForWeatherApp.ThemeSetting.SUNRISE);
                
            }

            if (rad_b_2_1.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTemp((int)DataContextForWeatherApp.Temp_Settings.CELSIUS);
                
            }
            else if (rad_b_2_2.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTemp((int)DataContextForWeatherApp.Temp_Settings.FAHRENHEIT);
                
            }
            else if (rad_b_2_3.IsChecked == true)
            {
                (DataContext as DataContextForWeatherApp)?.SetTemp((int)DataContextForWeatherApp.Temp_Settings.KELVIN);
                
            }
            
            if (upDown1.Value != null || upDown1.Value > 100)
            {
                Height = (int)upDown1.Value;
            }
            else Height = 850;
            if (upDown2.Value != null || upDown2.Value > 100)
            {
                Width = (int)upDown2.Value;
            }
            else Width = 500;
        }

        private async void DialogWindow(object? sender, TappedEventArgs? e)
        {

            var dialog = new DialogWindow();
            string? result = await dialog.ShowAsync();
            if (result != null)
            {
                await MakeRequest.Make_a_Request(result);
            }
            this.WindowState = WindowState.Normal;
        }
    }
}