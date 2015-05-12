using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;


namespace SpriteMapGenerator 
{
    public class SpriteImage
    {
        public BitmapImage bmImg {get;set;}
        public String filename { get; set; }
        //public Point point;

        //BitmapImage
        public  string filePath {get;set;}
        public int id { get; set; }

        public double width { get; set; }
        public double height { get; set; }

        
    }
}
