using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HW8_UserOutput.ViewModels;

namespace HW8_UserOutput.Views;

public partial class TreeView : UserControl
{
    public TreeView()
    {
        InitializeComponent();
        DataContext = new TreeViewViewModel();
    }
}