using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System;
using System.Linq;
using System.Transactions;
using System.Windows.Input;
using Avalonia.Utilities;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Input;
using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using Avalonia.Controls.Converters;
using System.Runtime.CompilerServices;


namespace AvaloniaApplication1.Controls
{

    public class Pallete : TemplatedControl
    {
        private int _counter;

        public static readonly StyledProperty<ICommand> AddColorProperty =
    AvaloniaProperty.Register<Pallete, ICommand>(nameof(AddColor));

        public ICommand AddColor
        {
            get => GetValue(AddColorProperty);
            set => SetValue(AddColorProperty, value);
        }

        public static readonly StyledProperty<string> RGB_RedProperty =
    AvaloniaProperty.Register<Pallete, string>(nameof(RGB_Red));

        public string RGB_Red
        {
            get => GetValue(RGB_RedProperty);
            set
            {
                SetValue(RGB_RedProperty, value);
            }
        }


        public static readonly StyledProperty<string> RGB_GreenProperty =
AvaloniaProperty.Register<Pallete, string>(nameof(RGB_Green));

        public string RGB_Green
        {
            get => GetValue(RGB_GreenProperty);
            set
            {
                SetValue(RGB_GreenProperty, value);
            }
        }

        public static readonly StyledProperty<string> RGB_BlueProperty =
AvaloniaProperty.Register<Pallete, string>(nameof(RGB_Blue));

        public string RGB_Blue
        {
            get => GetValue(RGB_BlueProperty);
            set
            {
                SetValue(RGB_BlueProperty, value);
            }
        }

        public static readonly StyledProperty<string> HueProperty =
AvaloniaProperty.Register<Pallete, string>(nameof(Hue));

        public string Hue
        {
            get => GetValue(HueProperty);
            set
            {
                SetValue(HueProperty, value);
            }
        }

        public static readonly StyledProperty<string> SaturationProperty =
AvaloniaProperty.Register<Pallete, string>(nameof(Saturation));

        public string Saturation
        {
            get => GetValue(SaturationProperty);
            set
            {
                SetValue(SaturationProperty, value);
            }
        }

        public static readonly StyledProperty<string> LuminocityProperty =
AvaloniaProperty.Register<Pallete, string>(nameof(LuminocityProperty));

        public string Luminocity
        {
            get => GetValue(LuminocityProperty);
            set
            {
                SetValue(LuminocityProperty, value);
            }
        }

        public static readonly StyledProperty<Color> ColorSpectrumValueProperty =
            AvaloniaProperty.Register<Pallete, Color>(nameof(ColorSpectrumValue));

        public Color ColorSpectrumValue
        {
            get => GetValue(ColorSpectrumValueProperty);
            set
            {
                SetValue(ColorSpectrumValueProperty, value);
            }
        }

        public static readonly StyledProperty<Color> ColorValProperty =
            AvaloniaProperty.Register<Pallete, Color>(nameof(ColorVal));

        public Color ColorVal
        {
            get => GetValue(ColorValProperty);
            set
            {
                SetValue(ColorValProperty, value);
            }
        }

        public static readonly StyledProperty<IBrush> ColorBrushProperty =
            AvaloniaProperty.Register<Pallete, IBrush>(nameof(ColorBrush));

        public IBrush ColorBrush
        {
            get => GetValue(ColorBrushProperty);
            set
            {
                SetValue(ColorBrushProperty, value);
            }
        }



        public static readonly StyledProperty<double> BrightnessProperty =
            AvaloniaProperty.Register<Pallete, double>(nameof(Brightness));

        public double Brightness
        {
            get => GetValue(BrightnessProperty);
            set
            {
                SetValue(BrightnessProperty, value);
            }
        }

        private bool _isUpdatingColor = false;

        private async void EvenLesserColorChange_rgb()
        {
            if (!_isUpdatingColor)
            {
                _isUpdatingColor = true;

                Color clr;
                try
                {
                    int r = Convert.ToByte(RGB_Red);
                    int g = Convert.ToByte(RGB_Green);
                    int b = Convert.ToByte(RGB_Blue);
                    clr = new Color(255, (byte)r, (byte)g, (byte)b);
                }
                catch (Exception)
                {
                    Console.WriteLine("EXCEPTIONNN");
                    _isUpdatingColor = false;
                    return;
                }
                Hue = (Math.Truncate(clr.ToHsl().H * 100) / 100).ToString();
                Saturation = (Math.Truncate(clr.ToHsl().S * 100) / 100).ToString();
                Luminocity = (Math.Truncate(clr.ToHsl().L * 100) / 100).ToString();
                ColorVal = clr;
                ColorSpectrumValue = clr;
                //ColorBrush = new SolidColorBrush(new Color(255, 255, 0, 255));
                ColorBrush = new SolidColorBrush(clr);
                Brightness = clr.ToHsl().L / 100;
                await Task.Delay(50);
                _isUpdatingColor = false;
            }
        }
        private async void LesserColorChange_rgb(object? sender, TextChangedEventArgs e)
        {
            if (sender is TextBox && !_isUpdatingColor)
            {
                _isUpdatingColor = true;

                Color clr;
                try
                {
                    int r = Convert.ToByte(RGB_Red);
                    int g = Convert.ToByte(RGB_Green);
                    int b = Convert.ToByte(RGB_Blue);
                    clr = new Color(255, (byte)r, (byte)g, (byte)b);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTIONNN");
                    _isUpdatingColor = false;
                    return;
                }
                Brightness = clr.ToHsl().L / 100;
                ColorVal = clr;
                ColorSpectrumValue = clr;
                ColorBrush = new SolidColorBrush(ColorVal);
                Hue = (Math.Truncate(ColorVal.ToHsl().H)).ToString();
                Saturation = (Math.Truncate(ColorVal.ToHsl().S * 100) / 100).ToString();
                Luminocity = (Math.Truncate(ColorVal.ToHsl().L * 100) / 100).ToString();
                await Task.Delay(50);
                _isUpdatingColor = false;
            }
        }
        private async void LesserColorChange_hsl(object? sender, TextChangedEventArgs e)
        {
            if (sender is TextBox && !_isUpdatingColor)
            {
                _isUpdatingColor = true;

                HslColor clr;
                try
                {
                    double h = Math.Truncate(double.Parse(Hue));
                    double s = Math.Truncate(double.Parse(Saturation) * 100) / 100;
                    double l = Math.Truncate(double.Parse(Luminocity) * 100) / 100;
                    clr = new HslColor(1.0, h, s, l);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTIONNN");
                    _isUpdatingColor = false;
                    return;
                }

                Brightness = Convert.ToDouble(Luminocity);
                ColorVal = clr.ToRgb();
                ColorSpectrumValue = clr.ToRgb();
                ColorBrush = new SolidColorBrush(ColorVal);
                RGB_Red = ColorVal.R.ToString();
                RGB_Green = ColorVal.G.ToString();
                RGB_Blue = ColorVal.B.ToString();
                await Task.Delay(50);
                _isUpdatingColor = false;
            }
        }

        private async void ColorChange(object sender, ColorChangedEventArgs e)
        {
            if (sender is ColorSpectrum && !_isUpdatingColor)
            {
                _isUpdatingColor = true;

                ColorSpectrumValue = e.NewColor;
                var clr = new HslColor(ColorSpectrumValue.ToHsl().A, ColorSpectrumValue.ToHsl().H, ColorSpectrumValue.ToHsl().S, ColorSpectrumValue.ToHsl().L);
                ColorVal = clr.ToRgb();
                ColorBrush = new SolidColorBrush(ColorVal);
                RGB_Red = ColorVal.R.ToString();
                RGB_Green = ColorVal.G.ToString();
                RGB_Blue = ColorVal.B.ToString();
                Hue = (Math.Truncate(ColorVal.ToHsl().H)).ToString();
                Saturation = (Math.Truncate(ColorVal.ToHsl().S * 100) / 100).ToString();
                Brightness = (Math.Truncate(ColorVal.ToHsl().L * 100) / 100);
                Luminocity = Brightness.ToString();

                await Task.Delay(5);
                _isUpdatingColor = false;
            }
        }

        private async void ValueChange(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sender is ColorSlider)
            {
                _isUpdatingColor = true;

                Brightness = MathUtilities.Clamp(Math.Truncate(e.NewValue) / 100, 0.01, 0.99);
                var clr = new HslColor(1.0, ColorVal.ToHsl().H, ColorVal.ToHsl().S, Brightness);
                ColorVal = clr.ToRgb();
                ColorSpectrumValue = clr.ToRgb();
                ColorBrush = new SolidColorBrush(ColorVal);
                RGB_Red = ColorVal.R.ToString();
                RGB_Green = ColorVal.G.ToString();
                RGB_Blue = ColorVal.B.ToString();
                Hue = (Math.Truncate(ColorVal.ToHsl().H)).ToString();
                Saturation = (Math.Truncate(ColorVal.ToHsl().S * 100) / 100).ToString();
                Brightness = (Math.Truncate(ColorVal.ToHsl().L * 100) / 100);
                Luminocity = Brightness.ToString();

                await Task.Delay(500);
                _isUpdatingColor = false;
            }
        }

        public Pallete()
        {
            Console.WriteLine("yap");
            Brightness = 0.5;
            _rectangleColors.Add(new Color(1,255,0,0));
            AddColor = new RelayCommand(AddTheColor);
        }
        private void AddTheColor()
        {
            _counter %= dynam_borders.Count;
            var rectangle = dynam_borders[_counter].Child as Avalonia.Controls.Shapes.Rectangle;
            if (rectangle != null)
            {
                Console.WriteLine("wahoo3 + " + rectangle.Fill.ToString());
                SolidColorBrush color = new SolidColorBrush(ColorVal);
                rectangle.Fill = color;
            }
            _counter++;
        }

        public IEnumerable<Color> RectangleColors
        {
            get { return _rectangleColors; }
        }
        private List<Color> _rectangleColors = new List<Color>();
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e); // do single slider, that gets found based on orientation
            var hue = e.NameScope.Find<TextBox>("hue");
            var sat = e.NameScope.Find<TextBox>("sat");
            var lum = e.NameScope.Find<TextBox>("lum");
            var red = e.NameScope.Find<TextBox>("red");
            var green = e.NameScope.Find<TextBox>("green");
            var blue = e.NameScope.Find<TextBox>("blue");
            var colorspectrum = e.NameScope.Find<ColorSpectrum>("ColorSpectrum");
            var brightnessslider = e.NameScope.Find<ColorSlider>("BrightnessSlider");

            colorspectrum.ColorChanged += (sender, args) => ColorChange(sender, args);
            brightnessslider.ValueChanged += (sender, args) => ValueChange(sender, args);
            hue.TextChanged += LesserColorChange_hsl;
            sat.TextChanged += LesserColorChange_hsl;
            lum.TextChanged += LesserColorChange_hsl;
            red.TextChanged += LesserColorChange_rgb;
            green.TextChanged += LesserColorChange_rgb;
            blue.TextChanged += LesserColorChange_rgb;
            FindBorders(this);

            foreach (var border in borders)
            {
                Console.WriteLine("wahoo");
                border.Tapped += Border_Tapped;
            }
        }

        private void FindBorders(Control control)
        {
            foreach (var child in control.GetVisualChildren())
            {
                if (child is Border border)
                {
                    borders.Add(border);
                    if (border.Name != null)
                    {
                        dynam_borders.Add(border);
                    }
                }
                if (child is Control childControl)
                {
                    FindBorders(childControl);
                }
            }
        }


        private List<Border> borders = new List<Border>();
        private List<Border> dynam_borders = new List<Border>();

        private void Border_Tapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Border b)
            {
                var rectangle = b.Child as Avalonia.Controls.Shapes.Rectangle;
                if (rectangle != null)
                {
                    Console.WriteLine("wahoo3 + " + rectangle.Fill.ToString());
                    var color = rectangle.Fill;
                    if (rectangle.Fill == null)
                    {
                        Console.WriteLine("Rectangle.Fill is null");
                    }
                    else if (color is IImmutableSolidColorBrush clr)
                    {
                        Console.WriteLine("Reached the 'wahee' line of code");
                        RGB_Red = clr.Color.R.ToString();
                        RGB_Green = clr.Color.G.ToString();
                        RGB_Blue = clr.Color.B.ToString();
                        EvenLesserColorChange_rgb();
                    }
                    else if (color is SolidColorBrush clr2)
                    {
                        Console.WriteLine("Reached the 'wahee' line of code");
                        RGB_Red = clr2.Color.R.ToString();
                        RGB_Green = clr2.Color.G.ToString();
                        RGB_Blue = clr2.Color.B.ToString();
                        EvenLesserColorChange_rgb();
                    }
                    else
                    {
                        Console.WriteLine("Color is not a SolidColorBrush");
                    }
                }
            }
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
