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
using System.Threading.Tasks;

namespace ExplorerAtHome
{   
    public class FileItem
    {
        private string _filePath;
        private Bitmap _iconPath;
        private string? _fullfilePath;

        public FileItem(string filePath, string iconPath, string? fullfilePath = "")
        {
            _filePath = filePath;
            _iconPath = new Bitmap(iconPath);
            _fullfilePath = fullfilePath;
        }

        public string FilePath { get => _filePath;}
        public Bitmap IconPath { get => _iconPath;}
        public string? FullfilePath { get => _fullfilePath;}

    }

    internal class DataContextForExplorer : INotifyPropertyChanged
    {
        private string _currFile = @"D:\3Semester\Visual Programming I\Домашнее задание №5\resources\placeholder.jpg";
        private string _currDir;
        private string _regularFolderPNGPath = @".\resources\regular_folder.png";
        private string _filePNGPath = @".\resources\file.png";
        private string _parentFolderPNGPath = @".\resources\arrow-up-bold.png";
        private string _discPNGPath = @".\resources\drive.png";
        private Bitmap? _image = new Bitmap(@".\resources\placeholder.jpg");
        private ListBox _listBox;
        private ObservableCollection<FileItem> _fileCollection;
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png", ".tiff" };

        private bool ExtentionCheck(string file)
        {
                if (ImageExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
                {
                    return true;
                }
            return false;
        }

        public ObservableCollection<FileItem> FileCollection
        {
            get => _fileCollection;
            set => SetField(ref _fileCollection, value);
        }
        public Bitmap? Image
        {
            get => _image;
            set => SetField(ref _image, value);
        }
        public string CurrentFile
        {
            get => _currFile;
            set => SetField(ref _currFile, value);
        }

        public DataContextForExplorer()
        { 
            _currDir = Directory.GetCurrentDirectory();
            _fileCollection = new ObservableCollection<FileItem>();
            _listBox = new ListBox();
            FileCollectionItit();
        }
        private async void FileCollectionItit()
        {
            FileCollection = await GetDirsAndFiles(_currDir);
        }
        public void AttachListBox(ListBox listBox)
        {
            _listBox = listBox;
            _listBox.DoubleTapped += ListBox_DoubleTapped;
            _listBox.Tapped += ListBox_Tapped;
        }
        

        public async void ListBox_Tapped(object? sender, RoutedEventArgs args)
        {

            if (_listBox.SelectedItem is FileItem selectedItem)
            {
                if (ExtentionCheck(selectedItem.FilePath) && selectedItem.FullfilePath != null)
                {
                    try
                    {
                        Image = await Task.Run(() => new Bitmap(selectedItem.FullfilePath));
                        CurrentFile = selectedItem.FullfilePath;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }    
        

        public async void ListBox_DoubleTapped(object? sender, RoutedEventArgs args)
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
                                temp2.Add(new FileItem(temp[i], _discPNGPath, temp[i]));
                            }
                            FileCollection = temp2;
                            

                        }
                        else
                        {
                            _currDir = x.FullName;
                            // Console.WriteLine("in .. " + _currDir);
                            FileCollection = await GetDirsAndFiles(_currDir);
                            // Console.WriteLine(_fileCollection[1].FilePath + " " + _fileCollection[1].IconPath);
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
                    FileCollection = await GetDirsAndFiles(_currDir);
                }
                else if (_currDir == "")
                {
                    _currDir = selectedItem.FilePath;
                    // Console.WriteLine("in directory checkout top tevel: " + _currDir);
                    FileCollection = await GetDirsAndFiles(_currDir);
                }
                // Console.WriteLine("after " + _currDir);
                // Console.WriteLine($"Double Tapped on: {selectedItem.FilePath}");
            }

            OnPropertyChanged(nameof(FileCollection));
            _listBox.InvalidateVisual();

        }


        private async Task<ObservableCollection<FileItem>> GetDirsAndFiles(string dir)
        {
            try
            {   
                // Console.WriteLine("GetDirsAndFiles");
                string[] returndir = [".."];
                string[] dirs = await Task.Run(() => Directory.GetDirectories(dir));
                List<string> files = await Task.Run(() => Directory.GetFiles(dir).ToList());
                foreach (string file in files.ToList())
                {
                    if (!ExtentionCheck(file))
                    {
                        files.Remove(file);
                    }
                }
                files.ToArray();
                string[] everything = returndir.Concat(dirs.Concat(files).ToArray()).ToArray();
                FileItem[] result = new FileItem[everything.Length];
                DirectoryInfo? highdir = Directory.GetParent(_currDir);
                if (highdir != null)
                {
                    result[0] = new FileItem("..", _parentFolderPNGPath, highdir.ToString());
                }
                else
                {
                    result[0] = new FileItem("..", _parentFolderPNGPath, "Discs");
                }
                
                for (int i = 1; i < everything.Length; i++)
                {
                    if (Directory.Exists(everything[i]))
                    {
                        result[i] = new FileItem(Path.GetRelativePath(_currDir, everything[i]), _regularFolderPNGPath, everything[i]);
                    }
                    else if (File.Exists(everything[i]))
                    {
                        result[i] = new FileItem(Path.GetRelativePath(_currDir, everything[i]), _filePNGPath, everything[i]);
                    }
                    // Console.WriteLine("in getdirsandfiles: " + result[i].FilePath + " and " + result[i].IconPath);
                }
                return new ObservableCollection<FileItem>(result);
            }
            catch (Exception ex)
            {
                CurrentFile = ($"Error: {ex.Message}");
                Image = null;
                return new ObservableCollection<FileItem>(new FileItem[] { new FileItem("..", _parentFolderPNGPath, "Back") });
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