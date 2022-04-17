using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab9
{
    /// <summary>
    /// Interaction logic for DialogColors.xaml
    /// </summary>
    public partial class DialogColors : Window
    {
        public ColorsProp changedcolors { get; set; }
        public DialogColors(ColorsProp colors)
        {
            InitializeComponent();
            changedcolors = new ColorsProp(colors.Main,colors.Fill, colors.OutlineWeight);
            ColorPickerMain.SelectedColor = colors.Main;
            ColorPickerFill.SelectedColor = colors.Fill;
            SliderOutline.Value = colors.OutlineWeight;
            
        }
        

        private void ColorPickerMain_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            
            changedcolors.Main = (Color)ColorPickerMain.SelectedColor;
        }

        private void ColorPickerFill_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            changedcolors.Fill = (Color)ColorPickerFill.SelectedColor;
        }

        private void SliderOutline_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            changedcolors.OutlineWeight = SliderOutline.Value;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
