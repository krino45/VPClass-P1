using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System;
using System.Linq;
using System.Transactions;
using System.Windows.Input;

namespace VolumeControl.Controls
{
    public class VolumeControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> IsFoldedProperty =
            AvaloniaProperty.Register<VolumeControl, bool>(nameof(IsFolded));

        public bool IsFolded
        {
            get => GetValue(IsFoldedProperty);
            set
            {
                SetValue(IsFoldedProperty, value);
            }
        }

        private void UpdateWidthAndColumnWidths(Border border, Grid grid, bool isFolded)
        {

            Console.WriteLine("yaaa");
            grid.Width = isFolded ? 100 : 500;
            border.Width = grid.Width;
            grid.ColumnDefinitions[1].Width = isFolded ? new GridLength(0) : new GridLength(4, GridUnitType.Star);

        }

        public static readonly StyledProperty<ICommand> FolderButtonProperty =
            AvaloniaProperty.Register<VolumeControl, ICommand>(nameof(FolderButton));

        public ICommand FolderButton
        {
            get => GetValue(FolderButtonProperty);
            set => SetValue(FolderButtonProperty, value);
        }

        public static readonly StyledProperty<object> FolderButtonParameterProperty =
            AvaloniaProperty.Register<VolumeControl, object>(nameof(FolderButtonParameter));

        public object FolderButtonParameter
        {
            get => GetValue(FolderButtonParameterProperty);
            set => SetValue(FolderButtonParameterProperty, value);
        }

        public static readonly StyledProperty<Transitions> A_TransitionProperty =
            AvaloniaProperty.Register<VolumeControl, Transitions>(nameof(A_Transition));

        public Transitions A_Transition
        {
            get => GetValue(A_TransitionProperty);
            set => SetValue(A_TransitionProperty, value);
        }

        public VolumeControl()
        {
            Console.WriteLine("yap");
            FolderButton = new RelayCommand(ToggleControlFoldedState);
        }

        public event EventHandler<bool>? FoldStateChanged;

        public void ToggleControlFoldedState()
        {
            IsFolded = !IsFolded;
            FoldStateChanged?.Invoke(this, IsFolded);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e); // do single slider, that gets found based on orientation
            var transition = new Transitions
{
                new DoubleTransition
                {
                    Property = WidthProperty,
                    Duration = TimeSpan.FromSeconds(1),
                    Easing = new LinearEasing(),
                },
                new DoubleTransition
                {
                    Property = HeightProperty,
                    Duration = TimeSpan.FromSeconds(1),
                    Easing = new LinearEasing(),
                }
            };


            var slider1 = e.NameScope.Find<Slider>("PART_SliderUP");
            var slider2 = e.NameScope.Find<Slider>("PART_SliderRIGHT");
            var grid = e.NameScope.Find<Grid>("PART_Grid");
            var border = e.NameScope.Find<Border>("PART_border");
            if (slider1 != null && slider2 != null && grid != null && border != null)
            {
                grid.Transitions = transition;
                slider1.ValueChanged += (sender, args) =>
                {
                    SliderValueChanged?.Invoke(this, slider1.Value);
                };
                FoldStateChanged += (sender, args) =>
                {
                    slider1.IsEnabled = !args;
                    slider2.IsEnabled = !args;
                    slider1.IsVisible = !args;
                    slider2.IsVisible = !args;
                    UpdateWidthAndColumnWidths(border, grid, args);
                };
            }
        }

        public static readonly DirectProperty<VolumeControl, double> SliderValueProperty =
            AvaloniaProperty.RegisterDirect<VolumeControl, double>(
                nameof(SliderValue),
                o => o.SliderValue,
                (o, v) => o.SliderValue = v);

        private double _sliderValue;

        public double SliderValue
        {
            get => _sliderValue;
            set => SetAndRaise(SliderValueProperty, ref _sliderValue, value);
        }

        public event EventHandler<double>? SliderValueChanged;

        public static readonly StyledProperty<double> SliderMinProperty =
            AvaloniaProperty.Register<VolumeControl, double>(nameof(SliderMin), defaultValue: 0.0);

        public double SliderMin
        {
            get => GetValue(SliderMinProperty);
            set => SetValue(SliderMinProperty, value);
        }

        public static readonly StyledProperty<double> SliderMaxProperty =
            AvaloniaProperty.Register<VolumeControl, double>(nameof(SliderMax), defaultValue: 100.0);

        public double SliderMax
        {
            get => GetValue(SliderMaxProperty);
            set => SetValue(SliderMaxProperty, value);
        }

        public static readonly StyledProperty<Bitmap> ImageSourceProperty =
           AvaloniaProperty.Register<VolumeControl, Bitmap>(nameof(ImageSource), defaultValue: new Bitmap(@"D:\3Semester\Visual Programming I\Домашнее задание №9\VolumeControl\Assets\avalonia-logo.ico"));

        public Bitmap ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
