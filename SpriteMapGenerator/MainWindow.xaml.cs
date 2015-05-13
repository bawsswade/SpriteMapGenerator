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
using System.Collections.ObjectModel;


namespace SpriteMapGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpriteSheet Atlas = new SpriteSheet();

        ListBox LB = new ListBox();

        //XElement newSprite = new XElement("Sprites");
        XDocument doc = new XDocument(new XElement("Sprites"));

        // list of images to be added to atlas 
        //public List<SpriteImage> browseImages = new List<SpriteImage>();

        public ObservableCollection<SpriteImage> browseImages = new ObservableCollection<SpriteImage>();

        int idCount = 0;

        double XOffset = 0;
        double YOffset = 0;

        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand QuitCommand = new RoutedCommand();

        //public event RoutedEventHandler mouseClick = new RoutedEventHandler(object);

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
            dlg.Filter = "PNG Files (*.png)|*.png|All Files |*.*| JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|JPEG Files (*.jpeg)|*.jpeg";

            //dlg.FileName = "Image";

            

            AddtoList(dlg);
        }

        public void AddtoList(Microsoft.Win32.OpenFileDialog dlg)
        {
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // store multi files in list
            foreach (String file in dlg.FileNames)
            {
                // to store in list
                SpriteImage temp = new SpriteImage();
                temp.bmImg = new BitmapImage(new Uri(file));

                var onlyFileName = System.IO.Path.GetFileName(file);
                temp.filename = onlyFileName;

                //add to list box
                spriteNames.Items.Add(temp);

                // add to tmep list for atlas
                browseImages.Add(temp);
            }

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
            AddtoSheet();
        }

        public void AddtoSheet()
        {
            foreach (SpriteImage sprite in browseImages)
            {
                // add to atlas's list
                sprite.width = sprite.bmImg.Width;
                sprite.height = sprite.bmImg.Height;
                Atlas.spriteList.Add(sprite);

                //adding to canvas
                Image tempImg = new Image
                {
                    Width = sprite.width,
                    Height = sprite.height,
                    Source = sprite.bmImg,
                };

                tempImg.SetValue(Canvas.LeftProperty, XOffset);
                tempImg.SetValue(Canvas.TopProperty, YOffset);

                XOffset += tempImg.Width;

                // stamp to right (last image's width) of last image  onto the background
                SpriteCanvas.Children.Add(tempImg);

                // add to xml document
                XElement spritesElement = doc.Element("Sprites");
                XElement spriteElement = new XElement("Sprite");

                spriteElement.SetAttributeValue("width", tempImg.Width);
                spriteElement.SetAttributeValue("height", tempImg.Height);
                spriteElement.SetAttributeValue("id", idCount);
                spriteElement.SetAttributeValue("filename", sprite.filename);
                spriteElement.SetAttributeValue("x", XOffset);
                spriteElement.SetAttributeValue("y", YOffset);
                spritesElement.Add(spriteElement);

                idCount++;
            }
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

        private void spriteName_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            SpriteImage lbi = ((sender as ListBox).SelectedItem as SpriteImage);
            browseText.Text = lbi.filename;
            Sprite.Source = lbi.bmImg;
        }
    }
}
