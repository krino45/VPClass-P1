using System;
using Avalonia.Controls;
using System.Reactive.Subjects;
using Avalonia.LogicalTree;
using System.Collections.Generic;

namespace TwoWindows
{
    internal class ObservableFactory
    {
        public IObservable<(string, string)> MakeObservable(ref TwoWindows.SecondaryWindow window1, ref List<TextBlock> textBlock)
        {
            var observer = new Observer(textBlock);
            observer.Observe(window1);
            return observer.Observable;
        }
    }
    internal class Observer : IObserver<(string, string)>
    {
        private readonly Subject<(string, string)> _subject = new Subject<(string, string)>();
        public IObservable<(string, string)> Observable => _subject;
        private readonly Dictionary<string, TextBlock> _textBlocks;

        public Observer(List<TextBlock> textBlocks)
        {
            _textBlocks = new Dictionary<string, TextBlock>();

            // Populate the dictionary with TextBlock controls and their names
            foreach (var textBlock in textBlocks)
            {
                _textBlocks[textBlock.Name] = textBlock;
                textBlock.Text = ":)";
            }
        }
        public void Observe(Window window)
        {
            window.PointerEntered += (sender, args) => _subject.OnNext(("textblockPE1", $"Pointer entered at ({args.GetPosition(window)})"));
            window.PointerMoved += (sender, args) => _subject.OnNext(("textblockPE2", $"Pointer moved at ({args.GetPosition(window)})"));
            window.PointerExited += (sender, args) => _subject.OnNext(("textblockPE3", $"Pointer exited at ({args.GetPosition(window)})"));
            window.PointerPressed += (sender, args) => _subject.OnNext(("textblockPE4", $"Pointer pressed at ({args.GetPosition(window)})"));
            window.SizeChanged += (sender, args) => _subject.OnNext(("textblockPE5", $"New window size: ({args.NewSize})"));
            window.PositionChanged += (sender, args) => _subject.OnNext(("textblockPE6", $"New window position at ({args.Point.X}, {args.Point.Y})"));

            var button = window.GetControl<Button>("Button");
            if (button != null)
            {
                button.Tapped += (sender, args) => _subject.OnNext(("textblockBE1", $"Button tapped! {DateTimeOffset.Now}"));
                button.DoubleTapped += (sender, args) => _subject.OnNext(("textblockBE2", $"Button doubletapped! {DateTimeOffset.Now}"));
            }

            var textbox = window.GetControl<TextBox>("TextBox");
            if (textbox != null)
            {
                textbox.GotFocus += (sender, args) => _subject.OnNext(("textblockTE1", "TextBox focused!"));
                textbox.LostFocus += (sender, args) => _subject.OnNext(("textblockTE1", "TextBox lost focus!!"));
                textbox.TextChanged += (sender, args) => _subject.OnNext(("textblockTE2", $"TextBox text changed to: {textbox.Text}"));
            }
            var slider = window.GetControl<Slider>("Slider");
            if (slider != null)
            {
                slider.ValueChanged += (sender, args) => _subject.OnNext(("textblockSE", $"Slider value changed to {slider.Value}"));
            }
            var toggleSwitch = window.GetControl<ToggleSwitch>("ToggleSwitch");
            if (toggleSwitch != null)
            {
                toggleSwitch.IsCheckedChanged += (sender, args) => _subject.OnNext(("textblockToggleBE", $"toggleSwitch val is {toggleSwitch.IsChecked}!"));
            }
            var checkBox = window.GetControl<CheckBox>("CheckBox");
            if (checkBox != null)
            {
                checkBox.IsCheckedChanged += (sender, args) => _subject.OnNext(("textblockCE", $"checkBox val is {checkBox.IsChecked}!"));
            }
            var radioButtons = window.GetControl<StackPanel>("RadioButton");
            if (radioButtons != null)
            {
                foreach (RadioButton rb in radioButtons.GetLogicalChildren())
                    rb.IsCheckedChanged += (sender, args) => _subject.OnNext(($"textblockRE_{rb.Name}", $"radioButton '{rb.Name}' val is {rb.IsChecked}!"));
            }
            var listBox = window.GetControl<ListBox>("ListBox");
            if (listBox != null)
            {
                listBox.SelectionChanged += (sender, args) => _subject.OnNext(("textblockLE", $"ListBox selection changed to item #{listBox.SelectedIndex + 1}"));
            }
        }

        public void OnCompleted()
        {
            Console.WriteLine("gg");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("err :( (" + error.Message + ")");
        }

        public void OnNext((string, string) value)
        {
            if (_textBlocks.TryGetValue(value.Item1, out var textBlock))
            {
                textBlock.Text = value.Item2;
            }
        }
    }
}
