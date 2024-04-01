using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace HW8_UserOutput.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        private object? _content;
        private ObservableCollection<ViewModelBase> _viewModelBases;
        public object? Content
        {
            get => _content;
            set
            {
                this.RaiseAndSetIfChanged(ref _content, value);
            }
        }
        public MainWindowViewModel()
        {
            _viewModelBases = new ObservableCollection<ViewModelBase>();
            _viewModelBases.Add(new DataGridViewModel());
            _viewModelBases.Add(new TreeViewViewModel());
            _content = "Welcome to Avalonia!";
        }
        public void ChangeView(ViewModelBase viewModel)
        {
            Content = viewModel;
        }

        public ObservableCollection<ViewModelBase> viewModelBases
        {
            get => _viewModelBases;
            set
            {
                this.RaiseAndSetIfChanged(ref _viewModelBases, value);
            }
        }
    }
}
