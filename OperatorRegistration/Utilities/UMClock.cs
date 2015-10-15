using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012  
    /// Timer Class
    /// </summary>

    public class UMClock : FrameworkElement
    {
        private DispatcherTimer timer;

        private static DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTime", typeof(DateTime), typeof(UMClock),
            new PropertyMetadata(DateTime.Now));

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetValue(DateTimeProperty, DateTime.Now);
        }
    }

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culutre)
        {
            return null;
        }
    }
}
