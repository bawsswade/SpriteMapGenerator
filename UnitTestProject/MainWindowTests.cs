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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = "./GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/images/smile.png";
            m.AddtoList(dlg);
            Assert.IsNotNull(m.browseImages);
        }

        [TestMethod()]
        public void AddtoSheetTest()
        {
            MainWindow m = new MainWindow();
            SpriteImage si = new SpriteImage();
            si.width = 10;
            si.height = 10;
            si.bmImg = new BitmapImage();
            si.bmImg.BeginInit();
            si.bmImg.UriSource = new Uri(@"/Users/wade.gushikuma/MyDocuments/GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/images/smile.png", UriKind.Relative);
            si.bmImg.EndInit();
            m.browseImages.Add(si);

            m.AddtoSheet();
            Assert.IsNotNull(m.browseImages);
        }


    }
}
