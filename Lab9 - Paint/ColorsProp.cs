using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab9
{
    public class ColorsProp
    {
        public Color Main { get; set; }
        public Color Fill { get; set; }
        public double OutlineWeight { get; set; }
        public ColorsProp()
        {
            Main = new Color();
            Fill = new Color();
            OutlineWeight = 1;
        }
        public ColorsProp(Color main, Color fill, double weight)
        {
            Main = main;
            Fill = fill;
            OutlineWeight = weight;
        }
    }
}
