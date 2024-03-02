using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ExplorerAtHome
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as DataContextForExplorer)?.AttachListBox(listbox);
        }

    }
}
