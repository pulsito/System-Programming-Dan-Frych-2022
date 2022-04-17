using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Lab9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ColorsProp colors = new ColorsProp(Colors.Black,Colors.Transparent, 1);
        public  Point startP = new Point(); 
        public Point endP = new Point();
        Rectangle rectangle;
        Ellipse ellipse;
        Line line;
        Thickness thickness;
        string fileName = "";

        public static string Extension { get; set; } = "jpg";

        public bool FileIsOpen = false;
        public bool FileIsSave = false;
        public bool FileIsPrint = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Save_File()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = $"{Extension} files (*.{Extension})|*.{Extension}";
            
            if (sfd.ShowDialog().Value)
            {
                fileName = sfd.FileName;
                FileName.Text = "File Name: " + sfd.FileName;
                int marg = int.Parse(inkCanvas.Margin.Left.ToString());
                RenderTargetBitmap rtb =
                        new RenderTargetBitmap((int)this.Frame.ActualWidth,
                                (int)this.Frame.ActualHeight, 0, 0,
                            PixelFormats.Default);
                rtb.Render(inkCanvas);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fileStream = new FileStream(sfd.FileName, FileMode.Create);
                encoder.Save(fileStream);
                fileStream.Close();
                
            }
        }

        



        private void InkCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startP = e.GetPosition(inkCanvas);
            thickness = new Thickness(startP.X, startP.Y, 0, 0);
            if (btnRectangle.IsChecked == true)
            {
                rectangle = new Rectangle();
                rectangle.Margin = thickness;
                rectangle.Fill = new SolidColorBrush(colors.Fill);
                rectangle.Stroke = new SolidColorBrush(inkCanvas.DefaultDrawingAttributes.Color);
                rectangle.StrokeThickness = colors.OutlineWeight;
                inkCanvas.Children.Add(rectangle);

            }
            else if (btnEllipse.IsChecked == true)
            {
                ellipse = new Ellipse();
                ellipse.Margin = thickness;
                ellipse.Fill = new SolidColorBrush(colors.Fill);
                ellipse.Stroke = new SolidColorBrush(inkCanvas.DefaultDrawingAttributes.Color);
                ellipse.StrokeThickness = colors.OutlineWeight;
                inkCanvas.Children.Add(ellipse);
            }
            else if (btnLine.IsChecked == true)
            {
                line = new Line();
                line.X1 = startP.X;
                line.Y1 = startP.Y;
                line.Stroke = new SolidColorBrush(inkCanvas.DefaultDrawingAttributes.Color);
                line.StrokeThickness = colors.OutlineWeight;
                inkCanvas.Children.Add(line);

            }

        }

        private void inkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                endP = e.GetPosition(inkCanvas);
                Cursor.Text = $"X: {endP.X} | Y: {endP.Y}";
                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    if (btnRectangle.IsChecked == true)
                    {
                        if (endP.X >= startP.X)
                        {
                            rectangle.Width = endP.X - startP.X;
                        }
                        else
                        {
                            rectangle.Width = startP.X - endP.X;
                            thickness.Left = startP.X - (startP.X - endP.X);
                            rectangle.Margin = thickness;
                        }
                        if (endP.Y >= startP.Y)
                        {
                            rectangle.Height = endP.Y - startP.Y;
                        }
                        else
                        {
                            rectangle.Height = startP.Y - endP.Y;
                            thickness.Top = startP.Y - (startP.Y - endP.Y);
                            rectangle.Margin = thickness;
                        }
                    }
                    else if (btnEllipse.IsChecked == true)
                    {
                        if (endP.X >= startP.X)
                        {
                            ellipse.Width = endP.X - startP.X;
                        }
                        else
                        {
                            ellipse.Width = startP.X - endP.X;
                            thickness.Left = startP.X - (startP.X - endP.X);
                            ellipse.Margin = thickness;
                        }
                        if (endP.Y >= startP.Y)
                        {
                            ellipse.Height = endP.Y - startP.Y;
                        }
                        else
                        {
                            ellipse.Height = startP.Y - endP.Y;
                            thickness.Top = startP.Y - (startP.Y - endP.Y);
                            ellipse.Margin = thickness;
                        }
                    }
                    else if (btnLine.IsChecked == true)
                    {
                        line.X2 = endP.X;
                        line.Y2 = endP.Y;
                    }
                }
            }
            catch
            {
            }
        }

        private void InkCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (btnRectangle.IsChecked == true)
            {
                rectangle = null;
            }
            else if (btnEllipse.IsChecked == true)
            {
                ellipse = null;
            }
            else if (btnLine.IsChecked == true)
            {
                line = null;
            }
        }

       
     

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {
            if (btnRectangle.IsChecked == true)
            {
                
                inkCanvas.EditingMode = InkCanvasEditingMode.None;
                btnEllipse.IsChecked = false;
                btnLine.IsChecked = false;
                btnPen.IsChecked = false;
                btnSelect.IsChecked = false;
            }
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            if (btnEllipse.IsChecked == true)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.None;
                btnRectangle.IsChecked= false;
                btnLine.IsChecked = false;
                btnPen.IsChecked = false;
                btnSelect.IsChecked = false;
            }
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            if (btnLine.IsChecked == true)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.None;
                btnRectangle.IsChecked = false;
                btnEllipse.IsChecked = false;
                btnPen.IsChecked = false;
                btnSelect.IsChecked = false;
                


            }
        }

       

        private void btnPen_Click(object sender, RoutedEventArgs e)
        {
            if (btnPen.IsChecked == true)
            {
                
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                btnRectangle.IsChecked = false;
                btnEllipse.IsChecked = false;
                btnLine.IsChecked = false;
                btnSelect.IsChecked = false;
            }
            
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (btnSelect.IsChecked == true)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Select;
                btnRectangle.IsChecked = false;
                btnEllipse.IsChecked = false;
                btnLine.IsChecked = false;
                btnPen.IsChecked = false;
            }
        }
                    

        private void btnColors_Click(object sender, RoutedEventArgs e)
        {
            DialogColors dialog = new DialogColors(colors);
            dialog.Owner = this;
            bool? diaRes = dialog.ShowDialog();
           colors = dialog.changedcolors;
           inkCanvas.DefaultDrawingAttributes.Color = colors.Main;
            inkCanvas.DefaultDrawingAttributes.Width = colors.OutlineWeight;
            inkCanvas.DefaultDrawingAttributes.Height = colors.OutlineWeight;
            

        }

        private void CommandBinding_Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (fileName != "")
            {
                int marg = int.Parse(inkCanvas.Margin.Left.ToString());
                RenderTargetBitmap rtb =
                        new RenderTargetBitmap((int)this.Frame.ActualWidth - marg,
                                (int)this.Frame.ActualHeight - marg, 0, 0,
                            PixelFormats.Default);
                rtb.Render(this.inkCanvas);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            else
                Save_File();
        }
        private void CommandBinding_SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            Save_File();
            
        }
        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG)|*.bmp;*.jpg;*.gif; *.tif; *.png";
            if (openFileDialog.ShowDialog().Value)
            {
                fileName = openFileDialog.FileName;
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(openFileDialog.FileName);
                bmi.EndInit();
                MainImg.Source = bmi;
                FileName.Text = "File Name: " + openFileDialog.FileName;
              
            }
        }
        private void CommandBinding_Undo(object sender, ExecutedRoutedEventArgs e)
        {
            if (btnPen.IsChecked == true)
            {
                int lengthStrokes = inkCanvas.Strokes.Count - 1;
                if (lengthStrokes >= 0)
                {
                    inkCanvas.Strokes.RemoveAt(lengthStrokes);
                }

            }
            else
            {
                int lengthShapes = inkCanvas.Children.Count - 1;
                if (lengthShapes >= 0)
                {
                    inkCanvas.Children.RemoveAt(lengthShapes);
                }

            }
        }
    
    
    }

}
