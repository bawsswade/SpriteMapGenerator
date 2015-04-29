using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows;

namespace SpriteMapGenerator
{
    class SpriteImage
    {
        public BitmapImage bmImg;
        public Point point;

        //BitmapImage
        public  string filePath {get;set;}
        public int id { get; set; }

        public double width { get; set; }
        public double height { get; set; }

        
    }
}
