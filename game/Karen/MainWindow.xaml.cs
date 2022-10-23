using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        bool _hide = false;
        public bool Next { get; private set; }
        public bool Hide
        {
            get
            {
                return _hide;
            }
            set
            {
                _hide = value;
                if (_hide)
                {
                    Dispatcher.Invoke(() => { this.Hide(); });
                }
                else
                {
                    Dispatcher.Invoke(() => { this.Show(); textBox.Text = ""; });
                }
            }
        }
        public MainWindow()
        {
            Hide();
            Singelton = this;
            InitializeComponent();
        }

        public async Task WriteText(string text, bool wait = true, bool clear = true, int delay = 20)
        {
            Next = true;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            Next = false;
            e.Cancel = true;
        }

        void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                 this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Next = false;
        }
    }
}
