using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using LogicGates.Controls;
using LogicGates.ViewModels;

namespace LogicGates.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Find<AND_gate>("and").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputAND(value));
            this.Find<NAND_gate>("nand").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputNAND(value));
            this.Find<BUFFER_gate>("buf").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputBUF(value));
            this.Find<INVERTOR_gate>("inv").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputINV(value));
            this.Find<OR_gate>("or").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputOR(value));
            this.Find<NOR_gate>("nor").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputNOR(value));
            this.Find<XOR_gate>("xor").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputXOR(value));
            this.Find<XNOR_gate>("xnor").OutputValueChanged += ((sender, value) => (DataContext as LogicGates.ViewModels.MainWindowViewModel)?.setOutputXNOR(value));
        }
    }
}