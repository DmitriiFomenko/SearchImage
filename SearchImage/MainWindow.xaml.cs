using SearchImage.Classes;
using System.Windows;
using System.Collections.Generic;
using SearchImage.AForge;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;

namespace SearchImage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            if (!System.IO.Directory.Exists("commands"))
                System.IO.Directory.CreateDirectory("commands");
            if (!System.IO.Directory.Exists("assets"))
                System.IO.Directory.CreateDirectory("assets");
        }

        private async void Button_Start(object sender, RoutedEventArgs e)
        {

            (sender as Button).IsEnabled = false;

            string str = textCommand.Text;
            await Task.Run(() => CommandEvent.Read(str));

            (sender as Button).IsEnabled = true;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText(textPath.Text, textCommand.Text);
        }

        private void Button_Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.CheckFileExists = true;

            dlg.Filter = "txt files (*.txt)|*.txt";
            dlg.DefaultExt = ".txt";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textCommand.Text = System.IO.File.ReadAllText(dlg.FileName);
            }
        }
    }
}




