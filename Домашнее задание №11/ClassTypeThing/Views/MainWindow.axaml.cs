using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using ClassTypeThing.Controls;
using ClassTypeThing.Models;
using ClassTypeThing.ViewModels;

namespace ClassTypeThing.Views
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            reflectionThing.DataContext = new MainWindowViewModel();
            this.DataContext = reflectionThing.DataContext;
        }

    }
}