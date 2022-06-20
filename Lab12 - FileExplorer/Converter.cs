using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileExplorer
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class Converter : IValueConverter
    {
        public static Converter Instance = new Converter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;

            if (path == null)
            {
                return null;
            }
            var name = MainWindow.GetFileFolderName(path);
            var image = "Resources/file.png";

            if (string.IsNullOrEmpty(name))
            {
                image = "Resources/hard-disk.png";
            }
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "Resources/folder.png";
            }
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}