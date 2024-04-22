using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace LogicGates.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Models.Standard _std;
        public Models.Standard ViewSTD
        {
            get => _std;
            set
            {
                this.RaiseAndSetIfChanged(ref _std, value);
            }
        }
#pragma warning disable CA1822 // Mark members as static
        private string _outputforand;
        public string OutputforAND
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforand;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforand, value);
            }
        }

        private string _outputfornand;
        public string OutputforNAND
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputfornand;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputfornand, value);
            }
        }
        private string _outputforbuf;
        public string OutputforBUF
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforbuf;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforbuf, value);
            }
        }
        private string _outputforinv;
        public string OutputforINV
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforinv;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforinv, value);
            }
        }

        private string _outputforor;
        public string OutputforOR
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforor;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforor, value);
            }
        }
        private string _outputfornor;
        public string OutputforNOR
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputfornor;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputfornor, value);
            }
        }

        private string _outputforxor;
        public string OutputforXOR
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforxor;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforxor, value);
            }
        }

        private string _outputforxnor;
        public string OutputforXNOR
        {
            get
            {
                // Console.WriteLine("Getting OfA");
                return _outputforxnor;
            }
            set
            {
                // Console.WriteLine("Setting OfA");
                this.RaiseAndSetIfChanged(ref _outputforxnor, value);
            }
        }

        private ObservableCollection<bool> _inputValues1;
        public ObservableCollection<bool> InputValues1
        {
            get => _inputValues1;
            set => this.RaiseAndSetIfChanged(ref _inputValues1, value);
        }

        private ObservableCollection<bool> _inputValues2;
        public ObservableCollection<bool> InputValues2
        {
            get => _inputValues2;
            set => this.RaiseAndSetIfChanged(ref _inputValues2, value);
        }

        private ObservableCollection<bool> _inputValues3;
        public ObservableCollection<bool> InputValues3
        {
            get => _inputValues3;
            set => this.RaiseAndSetIfChanged(ref _inputValues3, value);
        }

        private ObservableCollection<bool> _inputValues4;
        public ObservableCollection<bool> InputValues4
        {
            get => _inputValues4;
            set => this.RaiseAndSetIfChanged(ref _inputValues4, value);
        }

        private ObservableCollection<bool> _inputValues5;
        public ObservableCollection<bool> InputValues5
        {
            get => _inputValues5;
            set => this.RaiseAndSetIfChanged(ref _inputValues5, value);
        }

        private ObservableCollection<bool> _inputValues6;
        public ObservableCollection<bool> InputValues6
        {
            get => _inputValues6;
            set => this.RaiseAndSetIfChanged(ref _inputValues6, value);
        }

        private ObservableCollection<bool> _inputValues7;
        public ObservableCollection<bool> InputValues7
        {
            get => _inputValues7;
            set => this.RaiseAndSetIfChanged(ref _inputValues7, value);
        }

        private ObservableCollection<bool> _inputValues8;
        public ObservableCollection<bool> InputValues8
        {
            get => _inputValues8;
            set => this.RaiseAndSetIfChanged(ref _inputValues8, value);
        }


        public void setOutputAND(bool value)
        {
            OutputforAND = value.ToString();
        }
        public void setOutputNAND(bool value)
        {
            OutputforNAND = value.ToString();
        }
        public void setOutputBUF(bool value)
        {
            OutputforBUF = value.ToString();
        }
        public void setOutputINV(bool value)
        {
            OutputforINV = value.ToString();
        }
        public void setOutputOR(bool value)
        {
            OutputforOR = value.ToString();
        }
        public void setOutputNOR(bool value)
        {
            OutputforNOR = value.ToString();
        }
        public void setOutputXOR(bool value)
        {
            OutputforXOR = value.ToString();
        }
        public void setOutputXNOR(bool value)
        {
            OutputforXNOR = value.ToString();
        }
        public MainWindowViewModel()
        {

            ViewSTD = Models.Standard.GOST;
            InputValues1 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues2 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues3 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues4 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues5 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues6 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues7 = new ObservableCollection<bool>([true, true, false, false, false]);
            InputValues8 = new ObservableCollection<bool>([true, true, false, false, false]);
        }
#pragma warning restore CA1822 // Mark members as static
    }
}
