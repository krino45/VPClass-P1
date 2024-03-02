using Avalonia.Media.Imaging;

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

        public string FilePath { get => _filePath; }
        public Bitmap IconPath { get => _iconPath; }
        public string? FullfilePath { get => _fullfilePath; }

    }
}