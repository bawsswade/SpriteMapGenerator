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


namespace SpriteMapGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpriteSheet Atlas = new SpriteSheet();

        SpriteImage temp = new SpriteImage();

        public static RoutedCommand MyCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();

            MyCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }

        public void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = "./GitHub/SpriteMapGenerator/SpriteMapGenerator/resources/images";
            dlg.Filter = "All Files |*.*| JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|JPEG Files (*.jpeg)|*.jpeg";
            dlg.FileName = "Image";

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
            // create temp img
            temp.bmImg = new BitmapImage(new Uri(browseText.Text));
            // add to list
            Atlas.spriteList.Add(temp);

            // set image
            AtlasImage.Source = temp.bmImg;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Atlas.CreateAtlas();
            MessageBox.Show("Your sprite sheet has been saved!");
        }

        private void MyCommandExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Application.Current.Shutdown();
        }
    }
}
