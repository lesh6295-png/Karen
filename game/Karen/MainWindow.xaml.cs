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

namespace Karen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Singelton;
        public MainWindow()
        {
            Singelton = this;
            InitializeComponent();
        }

        public async Task WriteText(string text, bool wait = true, bool clear = true, int delay = 20)
        {
            if (clear)
            {
                Dispatcher.Invoke(() => { textBox.Text = ""; });
            }
            foreach (char sym in text)
            {
                Dispatcher.Invoke(() => { textBox.Text += sym; });
                if (wait)
                    await Task.Delay(delay);
            }
        }

        private void KarenWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
