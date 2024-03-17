using Avalonia.Controls;
using Avalonia.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TwoWindows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private SecondaryWindow _window2;

        private async void Init(object? sender, TappedEventArgs? e)
        {
            await SecondaryWindow();
        }
        private async Task SecondaryWindow()
        {
            _window2 = new SecondaryWindow();
            _window2.Show();
            var factory = new ObservableFactory();
            var collection = new List<TextBlock> {  textblockPE1, textblockPE2, textblockPE3, textblockPE4, textblockPE5,
                                                    textblockPE6, textblockBE1, textblockBE2, textblockTE1, textblockTE2, 
                                                    textblockSE, textblockToggleBE, textblockCE, textblockRE_rb1, 
                                                    textblockRE_rb1, textblockRE_rb2, textblockRE_rb3, textblockLE };
            var observable = factory.MakeObservable(ref _window2, ref collection);
            var observer = new Observer(collection);
            observable.Subscribe(observer);
        }
    }
}