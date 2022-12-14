using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Karen.Types;
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
        int selectresult = 0;
        bool inselect = false;
        public bool Next { get; private set; }
        public bool IsReady { get; private set; } = false;
        public bool HideWindow
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
                    Dispatcher.Invoke(() => { this.Show(); });
                }
            }
        }
        public MainWindow()
        {
            Hide();
            Singelton = this;
            InitializeComponent();
            IsReady = true;
        }
        public async Task<int> Select(string[] names, int[] results)
        {
            if (names.Length != results.Length)
            {
                throw new InvalidApiParamsException("Count of names and results are not equals");
            }
            Dispatcher.Invoke(() => { select.Visibility = Visibility.Visible; nextBut.Visibility = Visibility.Hidden; textBox.Visibility = Visibility.Hidden; });
            inselect = true;
            for (int i = 0; i < names.Length; i++)
            {
                Dispatcher.Invoke(() => { AddSelectButton(names[i], results[i]); });
            }
            while (inselect)
            {
                await Task.Delay(70);
            }
            Dispatcher.Invoke(() => { select.Visibility = Visibility.Hidden; select.Children.Clear(); select.RowDefinitions.Clear(); nextBut.Visibility = Visibility.Visible; textBox.Visibility = Visibility.Visible; });
            return selectresult;
        }
        void AddSelectButton(string description, int variantid)
        {
            select.RowDefinitions.Add(new RowDefinition());
            var but = new Button();
            but.ClickMode = ClickMode.Release;
            but.Click += UnlockSelectMode;
            but.Name = "n" + variantid.ToString();
            var stackpanel = new StackPanel();
            stackpanel.Name = "pan" + Extensions.RandomString(min: 1, max: 3);
            stackpanel.HorizontalAlignment = HorizontalAlignment.Center;
            select.VerticalAlignment = VerticalAlignment.Center;
            but.Content = description;
            but.Opacity = 0.5;
            select.Children.Add(but);
            Grid.SetRow(but, select.RowDefinitions.Count - 1);
        }

        private void UnlockSelectMode(object sender, RoutedEventArgs e)
        {
            selectresult = Convert.ToInt32(((Button)sender).Name.Substring(1));
            inselect = false;
        }

        public async Task WriteText(string text, bool wait = true, bool clear = true, int delay = 20)
        {
            Next = true;
            Dispatcher.Invoke(async () =>
            {
                if (clear)
                {
                    textBox.Text = "";
                }
                foreach (char sym in text)
                {
                    textBox.Text += sym;
#if TESTING
                if (Config.AUTO_TEST)
                    continue;
#endif
                    if (wait)
                        await Task.Delay(delay);
                }
            }).Wait();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Engine.EngineStarter.VM.AllowHideWindow)
            {
                Hide();
                Next = false;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("Ты не можешь меня игнорировать!");
            }
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
            Dispatcher.Invoke(() => { textBox.Text = ""; });
            Next = false;
        }
        public void SetBodySprite(int kbl,int file)
        {
            Dispatcher.Invoke(() => { body.Source = Karen.Engine.Cache.GetSprite(kbl,file); });
        }
        public void SetEmotionSprite(int kbl, int file)
        {
            Dispatcher.Invoke(() => { emotion.Source = Karen.Engine.Cache.GetSprite(kbl, file); });
        }
        
    }
}
