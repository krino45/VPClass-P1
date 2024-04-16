using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Core;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ClassTypeThing.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ClassTypeThing.Controls
{

    public class ReflectionThing : TemplatedControl
    {
        public ReflectionThing()
        {
            ContentChanged += ReflectionThing_ContentChanged;
            Console.WriteLine("init");
            /* var _user = new Users
            {
                address = new Address { City = "moscow" },
                name = "Name",
                phone = "1234567890",
                company = new Company { Name = "starcucks" }
            };*/
            Stack_Panel = new StackPanel();

            ContentOfControlProperty.Changed.AddClassHandler<ReflectionThing>((sender, e) =>
            {
                // Handle property change here
                ContentChanged.Invoke(this, e.NewValue);
                Console.WriteLine($"Content changed: {e.NewValue}");
            });

        }

        public static readonly StyledProperty<object?> ContentProperty =
            AvaloniaProperty.Register<ReflectionThing, object?>(nameof(Content));
        public object? Content
        {
            get
            {
                Console.WriteLine($"got visual Content");
                return GetValue(ContentProperty);
            }
            set
            {
                Console.WriteLine($"visual Content {value}");
                SetValue(ContentProperty, value);
                Console.WriteLine($"visual Content {value}");
            }
        }

        private static readonly StyledProperty<StackPanel> Stack_PanelProperty =
            AvaloniaProperty.Register<ReflectionThing, StackPanel>(nameof(Stack_Panel));
        private StackPanel Stack_Panel
        {
            get => GetValue(Stack_PanelProperty);
            set => SetValue(Stack_PanelProperty, value);
        }

        public static readonly StyledProperty<object?> ContentOfControlProperty =
            AvaloniaProperty.Register<ReflectionThing, object?>(nameof(ContentOfControl));
        public object? ContentOfControl
        {
            get => GetValue(ContentOfControlProperty);
            set => SetValue(ContentOfControlProperty, value);
        }

        public event EventHandler<object?> ContentChanged;
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            Console.WriteLine("apply");
        }

        private void ReflectionThing_ContentChanged(object? sender, object? e)
        {
            Console.WriteLine("is e null?");
            if (e != null)
            {
                Console.WriteLine("no!");
                Type type = e.GetType();
                Console.WriteLine($"the type is {type}");
                HashSet<Type> types = new HashSet<Type>();
                var exp = TypeFieldInfoReflection(type, e, types);
                Content = exp;
            }
        }

        private Expander? TypeFieldInfoReflection(Type type, object? obj, HashSet<Type> visitedTypes)
        {
            if (visitedTypes.Contains(type))
            {
                return null;
            }
            visitedTypes.Add(type);
            Console.WriteLine($"checking typeinfo for type: {type} ");
            var fields = type.GetProperties(BindingFlags.Instance | BindingFlags.GetField
                | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);

            Expander exp = new Expander();
            exp.Padding = new Thickness(0);
            exp.Header = type.Name;
            exp.Background = new SolidColorBrush(Colors.White);
            exp.Foreground = new SolidColorBrush(Colors.Black);
            exp.Width = 500;

            StackPanel stackPanel = new StackPanel();
            

            foreach (PropertyInfo fieldinfo in fields)
            {
                Type fieldType = fieldinfo.PropertyType;
                if (fieldType.Namespace?.StartsWith("System") ?? false)
                {
                    Console.WriteLine($"base class fieldinfo: {fieldinfo}");

                    TextBlock tb1 = new TextBlock();
                    tb1.Text = fieldinfo.Name;
                    tb1.TextWrapping = TextWrapping.WrapWithOverflow;
                    tb1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    tb1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                    tb1.Margin = new Thickness(2);
                    object? value;
                    try
                    {
                        value = fieldinfo.GetValue(obj);
                    }
                    catch (Exception ex)
                    {
                        value = "Exception: " +  ex.Message;
                    }
                    
                    TextBlock tb2 = new TextBlock();
                    tb2.Text = value?.ToString() ?? "null";
                    tb2.TextWrapping = TextWrapping.WrapWithOverflow;
                    tb2.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    tb2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                    tb2.Margin = new Thickness(2);

                    Grid grid = new Grid();
                    grid.MinHeight = 25;
                    grid.ColumnDefinitions = new ColumnDefinitions("*,*");

                    Border br1 = new Border();
                    br1.Child = tb1;
                    br1.BorderBrush = new SolidColorBrush(Colors.Black);
                    br1.BorderThickness = new Thickness(1);
                    br1.CornerRadius = new CornerRadius(0);

                    Border br2 = new Border();
                    br2.Child = tb2;
                    br2.BorderBrush = new SolidColorBrush(Colors.Black);
                    br2.BorderThickness = new Thickness(1);
                    br2.CornerRadius = new CornerRadius(0);
                    grid.Children.Add(br1);
                    grid.Children.Add(br2);
                    Grid.SetColumn(br1, 0);
                    Grid.SetColumn(br2, 1);

                    stackPanel.Children.Add(grid);
                }
                else
                {
                    Console.WriteLine($"nonbaseclassfieldinfo: {fieldinfo}");
                    object? nestedObj = fieldinfo.GetValue(obj);
                    if (nestedObj != null)
                    {
                        var nestedExpander = TypeFieldInfoReflection(fieldType, nestedObj, visitedTypes);
                        if (nestedExpander != null)
                            stackPanel.Children.Add(nestedExpander);
                    }
                }
            }
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = stackPanel;
            exp.Content = scrollViewer;

            Console.WriteLine($"finished checking typeinfo for type: {type} ");
            return exp;
        }

    }

}