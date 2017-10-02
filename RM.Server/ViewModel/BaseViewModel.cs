using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RM.Server
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }
    }
}
