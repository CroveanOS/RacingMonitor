using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RM.Core
{
    public class ParallelogramConverter : BaseValueConverter<ParallelogramConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double offset = 2;

            FrameworkElement element = value as FrameworkElement;
            PointCollection points = new PointCollection();
            Action fillPoints = () =>
            {
                points.Clear();
                points.Add(new Point(element.Height + offset, 0));
                points.Add(new Point(element.Width - offset, 0));
                points.Add(new Point(element.Width - element.Height - offset, element.Height));
                points.Add(new Point(offset, element.Height));
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
