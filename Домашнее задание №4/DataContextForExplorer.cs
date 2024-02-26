using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace ExplorerAtHome
{   
    public class FileItem
    {
        private string _filePath;
        private Bitmap _imagePath;

        public FileItem(string filePath, string imagePath)
        {
            _filePath = filePath;
            _imagePath = new Bitmap(imagePath);
        }

        public string FilePath { get => _filePath;}
        public Bitmap ImagePath { get => _imagePath;}

    }

    internal class DataContextForExplorer : INotifyPropertyChanged
    {
        private string _currDir;
        private string _regularFolderPNGPath = @".\resources\regular_folder.png";
        private string _filePNGPath = @".\resources\file.png";
        private string _parentFolderPNGPath = @".\resources\arrow-up-bold.png";
        private string _discPNGPath = @".\resources\drive.png";
        private ListBox _listBox;
        private ObservableCollection<FileItem> _fileCollection;

        public ObservableCollection<FileItem> FileCollection
        {
            get => _fileCollection;
            set => SetField(ref _fileCollection, value);
        }


        public DataContextForExplorer()
        { 
            _currDir = Directory.GetCurrentDirectory();
            _fileCollection = new ObservableCollection<FileItem>();
            _listBox = new ListBox();
            FileCollection = GetDirsAndFiles(_currDir); 
        }
        public void AttachListBox(ListBox listBox)
        {
            _listBox = listBox;
            _listBox.DoubleTapped += ListBox_DoubleTapped;
        }
        public void ListBox_DoubleTapped(object? sender, RoutedEventArgs args)
        {   
            if (_listBox.SelectedItem is FileItem selectedItem)
            {
                if (selectedItem.FilePath == "..")
                {
                    try
                    {
                        DirectoryInfo? x = Directory.GetParent(_currDir);
                        // Console.WriteLine("x is " + x);
                        if (x == null)
                        {
                            _currDir = "";
                            ObservableCollection<string> temp = new ObservableCollection<string>(DriveInfo.GetDrives().Select(d => d.Name).ToList());
                            ObservableCollection<FileItem> temp2 = new ObservableCollection<FileItem>();
                            for(int i = 0; i < temp.Count; i++)
                            {
                                temp2.Add(new FileItem(temp[i], _discPNGPath));
                            }
                            FileCollection = temp2;
                            

                        }
                        else
                        {
                            _currDir = x.FullName;
                            // Console.WriteLine("in .. " + _currDir);
                            FileCollection = GetDirsAndFiles(_currDir);
                            // Console.WriteLine(_fileCollection[1].FilePath + " " + _fileCollection[1].ImagePath);
                        }
                    }
                    catch (Exception ex)
                    {   
                        Console.WriteLine(ex.Message);
                        _currDir = "C:\\";
                    }
                }
                else if (_currDir != "" && Directory.Exists(_currDir + @"\" + selectedItem.FilePath))
                {
                    _currDir = _currDir + @"\" + selectedItem.FilePath;
                    // Console.WriteLine("in directory checkout: " + _currDir);
                    FileCollection = GetDirsAndFiles(_currDir);
                }
                else if (_currDir == "")
                {
                    _currDir = selectedItem.FilePath;
                    // Console.WriteLine("in directory checkout top tevel: " + _currDir);
                    FileCollection = GetDirsAndFiles(_currDir);
                }
                // Console.WriteLine("after " + _currDir);
                // Console.WriteLine($"Double Tapped on: {selectedItem.FilePath}");
            }

            OnPropertyChanged(nameof(FileCollection));
            _listBox.InvalidateVisual();

        }


        private ObservableCollection<FileItem> GetDirsAndFiles(string dir)
        {
            try
            {   
                // Console.WriteLine("GetDirsAndFiles");
                string[] returndir = [".."];
                string[] dirs = Directory.GetDirectories(dir);
                string[] files = Directory.GetFiles(dir);
                string[] everything = returndir.Concat(dirs.Concat(files).ToArray()).ToArray();
                FileItem[] result = new FileItem[everything.Length];
                result[0] = new FileItem("..", _parentFolderPNGPath);
                for (int i = 1; i < everything.Length; i++)
                {
                    if (Directory.Exists(everything[i]))
                    {
                        result[i] = new FileItem(Path.GetRelativePath(_currDir, everything[i]), _regularFolderPNGPath);
                    }
                    else if (File.Exists(everything[i]))
                    {
                        result[i] = new FileItem(Path.GetRelativePath(_currDir, everything[i]), _filePNGPath);
                    }
                    // Console.WriteLine("in getdirsandfiles: " + result[i].FilePath + " and " + result[i].ImagePath);
                }
                return new ObservableCollection<FileItem>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ObservableCollection<FileItem>();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            // Console.WriteLine("not false....");
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}