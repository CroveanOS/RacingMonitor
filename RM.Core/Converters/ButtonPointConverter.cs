using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RM.Core
{
    public class ButtonPointConverter : BaseValueConverter<ButtonPointConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double offset = 2;

            FrameworkElement element = value as FrameworkElement;
            PointCollection points = new PointCollection();
            Action fillPoints = () =>
            {
                points.Clear();
                points.Add(new Point(0, 0));
                points.Add(new Point(element.Width - element.Height / 2 - offset, 0));
                points.Add(new Point(element.Width - offset, element.Height / 2));
                points.Add(new Point(element.Width - element.Height / 2 - offset, element.Height));
                points.Add(new Point(0, element.Height));
            };
            fillPoints();
            element.SizeChanged += (s, e) => fillPoints();
            return points;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
