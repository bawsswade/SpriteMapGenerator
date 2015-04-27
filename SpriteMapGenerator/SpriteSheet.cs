using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace SpriteMapGenerator
{
    class SpriteSheet
    {
        //BitmapImage AtlasSheet();
        List<SpriteImage> spriteList = new List<SpriteImage>();

        Image SpriteImage;
        
        SpriteSheet()
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(512, 512);
            SpriteImage.Source = writeableBmp;
            writeableBmp.GetBitmapContext();

            // Load an image from the calling Assembly's resources only by passing the relative path
            writeableBmp = BitmapFactory.New(1, 1).FromResource("./GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/images/smile.png");

        }

        public void SaveImage(string filename)
        {
            //AtlasSheet.
        }
    }
}
