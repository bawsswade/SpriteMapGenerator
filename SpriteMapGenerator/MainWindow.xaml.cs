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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;


namespace SpriteMapGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpriteSheet Atlas = new SpriteSheet();

        //XElement newSprite = new XElement("Sprites");
        XDocument doc = new XDocument(new XElement("Sprites"));

        //XmlDocument docu = new XmlDocument

        int idCount = 0;

        double XOffset = 0;
        double YOffset = 0;

        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand QuitCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();

            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            QuitCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
        }

        public void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
           

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = "./GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/";
            dlg.Filter = "All Files |*.*| JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|JPEG Files (*.jpeg)|*.jpeg";
            
            //dlg.FileName = "Image";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                browseText.Text = filename;
                // Set to draw 
                Sprite.Source = new BitmapImage(new Uri(browseText.Text));

            }
        }

        public void AddToSheet_Click(object sender, RoutedEventArgs e)
        {
            // storing in SpriteList
            SpriteImage tempSI = new SpriteImage();
            // create temp img
            tempSI.bmImg = new BitmapImage(new Uri(browseText.Text));
            // get sprite data
            tempSI.width = tempSI.bmImg.Width;
            tempSI.height = tempSI.bmImg.Height;
            // add to Atlas spriteList
            Atlas.spriteList.Add(tempSI);

            //adding to canvas
            Image tempImg = new Image
            {
                Width = tempSI.width,
                Height = tempSI.height,
                //Name = browseText.Text,
                Source = tempSI.bmImg,
            };

            tempImg.SetValue(Canvas.LeftProperty, XOffset);
            tempImg.SetValue(Canvas.TopProperty, YOffset);

            XOffset += tempImg.Width;

            // stamp to right (last image's width) of last image  onto the background
            SpriteCanvas.Children.Add(tempImg);

            // add to xml document
            XElement sprites = doc.Element("Sprites");
            XElement spriteElement = new XElement("Sprite");

            spriteElement.SetAttributeValue("width", tempImg.Width);
            spriteElement.SetAttributeValue("height", tempImg.Height);
            spriteElement.SetAttributeValue("id", idCount);
            sprites.Add(spriteElement);


            

            

            /*
            temp.Element("Sprite").Add(new XAttribute("width", tempImg.Width),
                                        new XAttribute("height", tempImg.Height),
                                        new XAttribute("id", idCount));
             */

           

            idCount++;

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            doc.Element("Sprites").Add(new XAttribute("count", Atlas.spriteList.Count()));

            Atlas.CreateAtlas(XOffset);
            doc.Save("./doc.xml");
            MessageBox.Show("Your sprite sheet has been saved!");
        }

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            doc.Element("Sprites").Add(new XAttribute("count", Atlas.spriteList.Count()));
            Atlas.CreateAtlas(XOffset);
            doc.Save("./doc.xml");
            MessageBox.Show("Your sprite sheet has been saved!");
        }
    }
}
