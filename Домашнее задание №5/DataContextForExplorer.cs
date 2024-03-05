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
using System.Threading;

namespace ExplorerAtHome
{   

    internal class DataContextForExplorer : INotifyPropertyChanged
    {
        private FileSystemWatcher? _watcher;
        private string _currFile = @"placeholder.jpg";
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
            _watcherInit();
        }
        private async void FileCollectionItit()
        {
            await GetDirsAndFiles(_currDir);
        }
        private void _watcherInit()
        {
            _watcher = new FileSystemWatcher(_currDir);
            _watcher.IncludeSubdirectories = false;
            _watcher.Changed += (sender, e) => { Watcher_Init(); };
            _watcher.Created += (sender, e) => { Watcher_Init(); };
            _watcher.Deleted += (sender, e) => { Watcher_Init(); };
            _watcher.Renamed += (sender, e) => { Watcher_Init(); };
            _watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Init()
        {
            Task.Run(async () =>
            {
                await GetDirsAndFiles(_currDir);
                OnPropertyChanged(nameof(FileCollection));
            });
        }
        private void ChangeWatcherDirectory()
        {   
            if(_watcher != null)
            {
                _watcher.Dispose();
            }
            _watcher = new FileSystemWatcher(_currDir);
            _watcher.IncludeSubdirectories = false;
            _watcher.Changed += (sender, e) => { Watcher_Init(); };
            _watcher.Created += (sender, e) => { Watcher_Init(); };
            _watcher.Deleted += (sender, e) => { Watcher_Init(); };
            _watcher.Renamed += (sender, e) => { Watcher_Init(); };
            _watcher.EnableRaisingEvents = true;
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
                    await Task.Run(() => ImageHandler(selectedItem.FullfilePath));
                }
            }
        }

        public void ImageHandler(string filepath)
        {
            try
            {
                Image = new Bitmap(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void ListBox_DoubleTapped(object? sender, RoutedEventArgs args)
        {
            string bufferDir = _currDir;
            bool safety_flag = true;
            if (_listBox.SelectedItem is FileItem selectedItem)
            {
                if (selectedItem.FilePath == "..")
                {
                    try
                    {
                        DirectoryInfo? x = Directory.GetParent(_currDir);
                        Console.WriteLine("x is " + x);
                        if (x == null)
                        {
                            _currDir = "";
                            safety_flag = false;
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
                            Console.WriteLine("in .. " + _currDir);
                            await GetDirsAndFiles(_currDir);
                            Console.WriteLine(_fileCollection[1].FilePath + " " + _fileCollection[1].IconPath);
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
                    Console.WriteLine("in directory checkout: " + _currDir);
                    await GetDirsAndFiles(_currDir);
                }
                else if (_currDir == "")
                {
                    _currDir = selectedItem.FilePath;
                    Console.WriteLine("in directory checkout top tevel: " + _currDir);
                    await GetDirsAndFiles(_currDir);
                }
                Console.WriteLine("after " + _currDir);
                Console.WriteLine($"Double Tapped on: {selectedItem.FilePath}");
            }
            if(bufferDir != _currDir && safety_flag)
            {
                ChangeWatcherDirectory();
                OnPropertyChanged(nameof(FileCollection));
            }
            
        }


        private async Task GetDirsAndFiles(string dir)
        {
            try
            {   
                Console.WriteLine("GetDirsAndFiles");
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
                        FileCollection = new ObservableCollection<FileItem>(result);
                        await Task.Delay(10);
                    }
                    else if (File.Exists(everything[i]))
                    {
                        result[i] = new FileItem(Path.GetRelativePath(_currDir, everything[i]), _filePNGPath, everything[i]);
                        FileCollection = new ObservableCollection<FileItem>(result);
                        await Task.Delay(10);
                    }
                    Console.WriteLine("in getdirsandfiles: " + result[i].FilePath + " and " + result[i].IconPath);
                }
                FileCollection = new ObservableCollection<FileItem>(result);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                CurrentFile = ($"Error: {ex.InnerException.Message}");
                Image = new Bitmap(@".\resources\placeholder.jpg");
                _currDir = Directory.GetCurrentDirectory();
                FileCollection = new ObservableCollection<FileItem>(new FileItem[] { new FileItem("..", _parentFolderPNGPath, "Back") });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CurrentFile = ($"Error: {ex.Message}");
                Image = new Bitmap(@".\resources\placeholder.jpg");
                _currDir = Directory.GetCurrentDirectory();
                FileCollection = new ObservableCollection<FileItem>(new FileItem[] { new FileItem("..", _parentFolderPNGPath, "Back") });

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
            Console.WriteLine("not false....");
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}