using RM.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ApplicationPage)value)
            {
                case ApplicationPage.Idle:
                    return new IdlePage();

                case ApplicationPage.Course:
                    return new CoursePage();

                case ApplicationPage.Selection:
                    return new SelectionPage();

                case ApplicationPage.KartType:
                    return new KartTypesPage();

                case ApplicationPage.Kart:
                    return new KartsPage();

                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.Register:
                    return new RegisterPage();

                case ApplicationPage.OneRace:
                    return new OneRacePage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
