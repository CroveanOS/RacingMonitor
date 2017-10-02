using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RM.Terminal
{
    [AddINotifyPropertyChangedInterface]
    public class BaseControl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }

        public void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
