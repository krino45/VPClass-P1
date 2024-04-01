using ReactiveUI;
using System;
using System.Reactive;

namespace VolumeControl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _textArea;

        public MainWindowViewModel()
        {
            TextArea = "Empty";
        }

        public string TextArea
        {
            get => _textArea;
            set => this.RaiseAndSetIfChanged(ref _textArea, value);
        }

        public void SubscribeToVolumeControlEvents(VolumeControl.Controls.VolumeControl volumeControl)
        {
            volumeControl.FoldStateChanged += (sender, isFolded) =>
            {
                TextArea = isFolded ? "Control folded" : "Control unfolded";
            };

            volumeControl.SliderValueChanged += (sender, sliderValue) =>
            {
                TextArea = $"Slider value: {sliderValue}";
            };
        }
    }
}
