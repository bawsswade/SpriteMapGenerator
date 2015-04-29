using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.IO;

namespace SpriteMapGenerator
{
    class SpriteSheet
    {
        const int SHEET_WIDTH = 2000;
        const int SHEET_HEIGHT = 2000;
        public List<SpriteImage> spriteList = new List<SpriteImage>();
        public Canvas SpriteCanvas = new Canvas();

        // atlas's image
        Image SpriteImage = new Image();

        public SpriteSheet() 
        {
            SpriteCanvas.Width = SHEET_WIDTH;
            SpriteCanvas.Height = SHEET_HEIGHT;
        }



        public void CreateAtlas()
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(SHEET_WIDTH, SHEET_HEIGHT);
            
            //SpriteImage.Source = writeableBmp;
            //writeableBmp.GetBitmapContext();

            // add sprites
            foreach (SpriteImage img in spriteList)
            {
                System.Windows.Rect spriteRec = new System.Windows.Rect(0, 0, img.width, img.height);
                WriteableBitmap wBmp = new WriteableBitmap(BitmapFactory.ConvertToPbgra32Format(img.bmImg));
                // width and height of individual image
                writeableBmp.Blit(spriteRec, wBmp, new System.Windows.Rect(0, 0, img.width, img.height));
            }

            //saving in png format
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(writeableBmp));

            //save the file
            FileStream fStream = null;
            string file = "SpriteSheet.png";
            //ParseFilePath(filePath, out  path, out file);
            try
            {
                fStream = new FileStream(file, FileMode.Create, FileAccess.Write);
                encoder.Save(fStream);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fStream != null)
                    fStream.Close();
            }
        }

    }
}
