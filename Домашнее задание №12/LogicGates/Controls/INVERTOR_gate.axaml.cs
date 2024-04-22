using Avalonia;
using LogicGates.Models;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Layout;
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
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using System.Collections.Specialized;
using DynamicData;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace LogicGates.Controls
{
    public class INVERTOR_gate : TemplatedControl
    {
        public static readonly StyledProperty<object?> ContentProperty =
            AvaloniaProperty.Register<INVERTOR_gate, object?>(nameof(Content));
        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly StyledProperty<byte> MinInputsProperty =
            AvaloniaProperty.Register<INVERTOR_gate, byte>(nameof(MinInputs), defaultValue: 1);
        public byte MinInputs
        {
            get => GetValue(MinInputsProperty);
        }

        public static readonly StyledProperty<byte> MaxInputsProperty =
            AvaloniaProperty.Register<INVERTOR_gate, byte>(nameof(MaxInputs), defaultValue: 1);
        public byte MaxInputs
        {
            get => GetValue(MaxInputsProperty);
        }

        public static readonly StyledProperty<byte> InputCountProperty =
            AvaloniaProperty.Register<INVERTOR_gate, byte>(nameof(InputCount), defaultValue: 1, validate: ValidateInputCount);
        private static bool ValidateInputCount(byte inputCount)
        {
            return inputCount == 1;
        }
        public byte InputCount
        {
            get => GetValue(InputCountProperty);
            set => SetValue(InputCountProperty, value);
        }

        private static void OnInputCountChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is byte newInputCount && e.Sender is INVERTOR_gate gate)
            {
                while (gate.InputValues.Count > newInputCount)
                {
                    gate.InputValues.RemoveAt(gate.InputValues.Count - 1);
                }
                while (gate.InputValues.Count < newInputCount)
                {
                    gate.InputValues.Add(false);
                }
                gate.DrawControl();
            }
        }
        private static void OnInputValuesChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is ObservableCollection<bool> newInputValues && e.Sender is INVERTOR_gate gate)
            {
                while (gate.InputValues.Count > gate.InputCount)
                {
                    gate.InputValues.RemoveAt(gate.InputValues.Count - 1);
                }
                while (gate.InputValues.Count < gate.InputCount)
                {
                    gate.InputValues.Add(false);
                }
            }
        }

        public static readonly StyledProperty<ObservableCollection<bool>> InputValuesProperty =
            AvaloniaProperty.Register<INVERTOR_gate, ObservableCollection<bool>>(nameof(InputValues), defaultValue: new ObservableCollection<bool>([false, false]));
        public ObservableCollection<bool> InputValues
        {
            get => GetValue(InputValuesProperty);
            set => SetValue(InputValuesProperty, value);
        }

        public static readonly StyledProperty<bool> OutputValueProperty =
            AvaloniaProperty.Register<INVERTOR_gate, bool>(nameof(OutputValue));
        public bool OutputValue
        {
            get
            {
                return GetValue(OutputValueProperty);
            }
            set
            {
                SetValue(OutputValueProperty, value);
                OutputValueChanged.Invoke(this, value);
            }
        }

        public event EventHandler<bool> OutputValueChanged;

        public static readonly StyledProperty<Standard> ViewStandardProperty =
            AvaloniaProperty.Register<INVERTOR_gate, Standard>(nameof(ViewStandard), defaultValue: Standard.GOST);
        public Standard ViewStandard
        {
            get => GetValue(ViewStandardProperty);
            set => SetValue(ViewStandardProperty, value);
        }

        private static void OnViewStandardPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is Standard std && e.Sender is INVERTOR_gate gate)
            {
                if (std == Standard.GOST)
                {
                    gate.Content = gate._GOST;
                    if (gate.Label == "INV") gate.Label = "Èםגונעמנ";
                }
                else if (std == Standard.ANSI)
                {
                    gate.Content = gate._ANSI;
                    if (gate.Label == "Èםגונעמנ") gate.Label = "INV";
                }
            }
        }

        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<AND_gate, bool>(nameof(IsSelected), defaultValue: false);
        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly StyledProperty<string?> LabelProperty =
            AvaloniaProperty.Register<INVERTOR_gate, string?>(nameof(Label), defaultValue: "Èםגונעמנ");
        public string? Label
        {
            get => GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        private void OnLabelChanged(AvaloniaPropertyChangedEventArgs e)
        {

            if (e.NewValue is string label && e.Sender is INVERTOR_gate gate)
            {
                gate.Label = label;
                gate.DrawControl();
            }
        }

        public static readonly StyledProperty<string> LabelFontProperty =
            AvaloniaProperty.Register<AND_gate, string>(nameof(LabelFont), defaultValue: "Times New Roman", validate: ValidateFont);

        private static bool ValidateFont(string fontname)
        {
            var fontFamily = new Avalonia.Media.FontFamily(fontname);
            return fontFamily.FamilyNames.Any(family => family.Equals(fontname));
        }
        public string LabelFont
        {
            get => GetValue(LabelFontProperty);
            set => SetValue(LabelFontProperty, value);
        }

        private object _GOST;
        private object _ANSI;

        public INVERTOR_gate()
        {
            DrawControl();
            InputCountProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => OnInputCountChanged(e));
            InputValuesProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => OnInputValuesChanged(e));
            InputValuesProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => LogicalOperation(e));
            ViewStandardProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => OnViewStandardPropertyChanged(e));
            LabelProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => OnLabelChanged(e));
            OutputValueProperty.Changed.AddClassHandler<INVERTOR_gate>((sender, e) => Console.WriteLine("lol"));
            Content = _GOST;
            this.Focusable = true;
            this.GotFocus += Gate_GotFocus;
            this.LostFocus += Gate_LostFocus;
        }

        private void Gate_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Console.WriteLine("lost focus");
            IsSelected = false;
            DrawControl();
        }

        private void Gate_GotFocus(object? sender, GotFocusEventArgs e)
        {
            Console.WriteLine("got focus");
            IsSelected = true;
            DrawControl();
        }

        private void LogicalOperation(AvaloniaPropertyChangedEventArgs e)
        {
            Console.WriteLine($"Commensing logical operatiton on {InputValues.Count} variables!");
            OutputValue = !InputValues[0];
            Console.WriteLine("Result: " + OutputValue);
        }

        public void DrawControl()
        {
            var GOST_Canvas = new Canvas();
            GOST_Canvas.Width = 100;
            GOST_Canvas.Height = 130;
            var GOST_in1 = new Ellipse
            {
                Fill = Brushes.Blue,
                Width = 10,
                Height = 10,
                [Canvas.TopProperty] = 40,
                [Canvas.LeftProperty] = 0,
            };
            GOST_Canvas.Children.Add(GOST_in1);
            var GOST_outputEllipse = new Ellipse
            {
                Stroke = Brushes.Orange,
                StrokeThickness = 2,
                Width = 10,
                Height = 10,
                [Canvas.TopProperty] = 40,
                [Canvas.LeftProperty] = 50,
            };
            GOST_Canvas.Children.Add(GOST_outputEllipse);
            var GOST_rectangleGeometry = Geometry.Parse("M 0 0 L 0 80 L 50 80 L 50 0 Z");
            var GOST_gateBody = new Path
            {
                Stroke = (IsSelected == false) ? Brushes.Black : Brushes.Red,
                StrokeThickness = 3,
                Fill = Brushes.Transparent,
                [Canvas.TopProperty] = 5,
                [Canvas.LeftProperty] = 5,
                Data = GOST_rectangleGeometry,
            };
            GOST_Canvas.Children.Add(GOST_gateBody);
            var textBlock1 = new TextBlock
            {
                Text = "1",
                HorizontalAlignment = HorizontalAlignment.Center,
                [Canvas.LeftProperty] = 25,
                [Canvas.TopProperty] = 25
            };
            GOST_Canvas.Children.Add(textBlock1);
            var textBlock2 = new TextBlock
            {
                FontFamily = new FontFamily(LabelFont),
                Text = Label,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Width = 100,
                [Canvas.TopProperty] = 95
            };
            GOST_Canvas.Children.Add(textBlock2);

            _GOST = GOST_Canvas;

            var ANSI_Canvas = new Canvas();
            ANSI_Canvas.Width = 100;
            ANSI_Canvas.Height = 130;
            ANSI_Canvas.HorizontalAlignment = HorizontalAlignment.Center;
            // Input ellipse
            var in1 = new Ellipse
            {
                Fill = Brushes.Blue,
                Width = 10,
                Height = 10,
                [Canvas.TopProperty] = 40,
                [Canvas.LeftProperty] = 0,
            };
            ANSI_Canvas.Children.Add(in1);
            var outputEllipse = new Ellipse
            {
                Stroke = Brushes.Orange,
                StrokeThickness = 2,
                Width = 10,
                Height = 10,
                [Canvas.TopProperty] = 40,
                [Canvas.LeftProperty] = 80,
            };
            ANSI_Canvas.Children.Add(outputEllipse);
            var triangleGeometry = Geometry.Parse("M 0 0 L 0 80 L 80 40 Z");
            var ANSI_gateBody = new Path
            {
                Stroke = (IsSelected == false) ? Brushes.Black : Brushes.Red,
                StrokeThickness = 3,
                Fill = Brushes.Transparent,
                [Canvas.TopProperty] = 5,
                [Canvas.LeftProperty] = 5,
                Data = triangleGeometry,
            };
            ANSI_Canvas.Children.Add(ANSI_gateBody);

            var ANSI_textBlock = new TextBlock
            {
                FontFamily = new FontFamily(LabelFont),
                Text = Label,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Width = 100,
                [Canvas.TopProperty] = 95
            };
            ANSI_Canvas.Children.Add(ANSI_textBlock);
            _ANSI = ANSI_Canvas;


            Content = (ViewStandard == Standard.GOST) ? _GOST : _ANSI;
        }
    }


}
