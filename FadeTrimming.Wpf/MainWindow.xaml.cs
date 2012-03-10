using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FadeTrimmingDemo.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Row : INotifyPropertyChanged
        {
            private string _text;

            public Row()
            {
                _text = "Lorem ipsum dolor sit amet";
            }

            public string Text
            {
                get { return _text; }
                set
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }

            #region Implementation of INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, e);
            }

            #endregion
        }

        public MainWindow()
        {
            InitializeComponent();

            List<Row> rows = Enumerable.Range(1, 100).Select(i => new Row()).ToList();
            DataGrid.ItemsSource = rows;

            var random = new Random();
            var timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += delegate
            {
                var index = random.Next(0, rows.Count);
                rows[index].Text = rows[index].Text.Length > 5
                                       ? "Short"
                                       : "This is a different long text string";
            };
            timer.Start();
        }
    }
}
