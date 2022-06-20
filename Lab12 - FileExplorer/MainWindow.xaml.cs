using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool maxBool;
        public MainWindow()
        {
            InitializeComponent();

           
        }

        // Runs when the window is loaded
        // Gets drives
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in Directory.GetLogicalDrives())
            {
                var drive = new TreeViewItem();
                drive.Header = item;
                drive.Tag = item;

                // Get Subdirectories
                drive.Items.Add(null);

                // Listener
                drive.Expanded += Drive_Expanded;

                VolumeDrives.Items.Add(drive);
            }
        }

        // When a drive is expanded
        private void Drive_Expanded(object sender, RoutedEventArgs e)
        {
            var drive = (TreeViewItem)sender;

            if (drive.Items.Count != 1 || drive.Items[0] != null)
            {
                return;
            }

            drive.Items.Clear();

            var volumePath = (string)drive.Tag;

            var directories = new List<string>();
            try
            {
                var IsDirectories = Directory.GetDirectories(volumePath).Length > 0;

                if (IsDirectories)
                {
                    directories.AddRange(Directory.GetDirectories(volumePath));
                }
            }
            catch
            {
            }

            directories.ForEach(path =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(path),
                    Tag = path
                };

                subItem.Items.Add(null);

                subItem.Expanded += Drive_Expanded;

                drive.Items.Add(subItem);
            });

            // Get Files
            var files = new List<string>();
            try
            {
                var IsFs = Directory.GetFiles(volumePath).Length > 0;

                if (IsFs)
                {
                    files.AddRange(Directory.GetFiles(volumePath));
                }
            }
            catch
            {
            }

            files.ForEach(path =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(path),
                    Tag = path
                };

                drive.Items.Add(subItem);
            });
        }

        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            var NormalizeIt = path.Replace('/', '\\');
            var LastIndex = NormalizeIt.LastIndexOf('\\');

            if (LastIndex <= 0)
            {
                return path;
            }

            return path.Substring(LastIndex + 1);
        }

        private void minWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maxWindow_Click(object sender, RoutedEventArgs e)
        {
            if (maxBool)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }

            maxBool = !maxBool;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void topBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left) DragMove();
            }
            catch (Exception)
            {
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}