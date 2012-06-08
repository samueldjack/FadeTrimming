using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FadeTrimmingDemo.Silverlight
{
    public partial class MainPage : UserControl
    {
        private List<Row> _itemsSource;

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

        public MainPage()
        {
            InitializeComponent();

            _itemsSource = Enumerable.Range(1, 100).Select(i => new Row()).ToList();
            DataGrid.ItemsSource = _itemsSource;

            var random = new Random();
            var timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(10)
            };

            timer.Tick += delegate
            {
                var index = random.Next(0, _itemsSource.Count);
                _itemsSource[index].Text = _itemsSource[index].Text.Length > 5
                                       ? "Short"
                                       : "This is a different long text string";
            };
            timer.Start();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _itemsSource[0].Text = "ipsua Lorem dolor sit amet";
        }
    }
}
