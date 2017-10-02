using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace RM.Core
{
    public class ButtonTypesConverter : BaseValueConverter<ButtonTypesConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const double offsetBottom = 10;
            const double width = 120;
            const double height = 20;


            FrameworkElement element = value as FrameworkElement;
            PointCollection points = new PointCollection();
            Action fillPoints = () =>
            {
                points.Clear();
                points.Add(new Point(element.Width - width + height, element.Height - height - offsetBottom));
                points.Add(new Point(element.Width, element.Height - height - offsetBottom));
                points.Add(new Point(element.Width, element.Height - offsetBottom));
                points.Add(new Point(element.Width - width, element.Height - offsetBottom));
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
