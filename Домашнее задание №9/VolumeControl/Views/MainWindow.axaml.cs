using Avalonia.Controls;
using System;
using VolumeControl.Controls;
using VolumeControl.ViewModels;

namespace VolumeControl.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            var volumeControl = this.FindControl<VolumeControl.Controls.VolumeControl>("VolumeControl");
            (DataContext as MainWindowViewModel)?.SubscribeToVolumeControlEvents(volumeControl);
        }
    }
}