using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Net.Http;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO.Compression;
using System.Windows.Shapes;

namespace GuiInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            config.Text = App.branchName;
            InitializeComponent();
        }

        private void installbutton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://github.com/lesh6295-png/Karen/releases/download/" + release.Text + "/" + config.Text + ".bin";
            string temp = Karen.Registry.RegController.GetKarenFolderPath() + "/temp.bin";
            try
            {
            install_progress.Value++;
            
                new System.Net.WebClient().DownloadFile(url, temp);
            
            
            install_progress.Value++;
            Directory.CreateDirectory(Karen.Registry.RegController.GetKarenFolderPath() + "/game/");
            Directory.CreateDirectory(path.Text);
            install_progress.Value++;
            ZipFile.ExtractToDirectory(temp, path.Text,true);
            install_progress.Value++;
            ZipFile.ExtractToDirectory(temp, path.Text,true);
            install_progress.Value++;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(url);
            }
        }
    }
}
