using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Threading;

namespace ClassTypeThing
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // custom stack size (10 MB)
            const int stackSize = 10 * 1024 * 1024; // 10 MB
            var thread = new Thread(() =>
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }, stackSize);
            thread.Start();
            thread.Join();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
    }
}
