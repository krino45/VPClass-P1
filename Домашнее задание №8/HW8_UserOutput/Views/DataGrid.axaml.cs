using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW8_UserOutput.ViewModels;

namespace HW8_UserOutput.Views
{
    public partial class DataGrid : UserControl
    {
        public DataGrid()
        {
            InitializeComponent();
            DataContext = new DataGridViewModel();
        }
    }
}
