using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteMapGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Imaging;

namespace SpriteMapGenerator.Tests
{
    [TestClass()]
    public class MainWindowTests
    {

        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        [TestMethod()]
        public void MainWindowTest()
        {
            MainWindow m = new MainWindow();
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public void AddtoListTest()
        {
            MainWindow m = new MainWindow();
            
            dlg.DefaultExt = "./GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/images/smile.png";
            m.AddtoList(dlg);
            Assert.IsNotNull(m.browseImages);
        }

        [TestMethod()]
        public void AddtoSheetTest()
        {
            MainWindow m = new MainWindow();
            SpriteImage si = new SpriteImage();

            foreach (String file in dlg.FileNames)
            {
                si.bmImg = new BitmapImage(new Uri(file));

                si.width = 10;
                si.height = 10;
                m.browseImages.Add(si);


                m.AddtoSheet();
            }
            Assert.IsNotNull(m.browseImages);
        }


    }
}
