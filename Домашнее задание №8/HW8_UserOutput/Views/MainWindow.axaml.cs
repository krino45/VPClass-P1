using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using HW8_UserOutput.ViewModels;

namespace HW8_UserOutput.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ListBoxItem_Tapped(object sender, TappedEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            if (item != null)
            {
                if (item.Name == "DataGrid")
                {
                    var viewmodelbase = (DataContext as MainWindowViewModel)?.viewModelBases[0];
                    (DataContext as MainWindowViewModel)?.ChangeView(viewModel: viewmodelbase);
                }
                else if (item.Name == "TreeView")
                {
                    var viewmodelbase = (DataContext as MainWindowViewModel)?.viewModelBases[1];
                    (DataContext as MainWindowViewModel)?.ChangeView(viewModel: viewmodelbase);
                }
            }

        }

    }
}