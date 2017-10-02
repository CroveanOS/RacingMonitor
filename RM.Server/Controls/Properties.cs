using System.Windows.Media;
//using MahApps.Metro.IconPacks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RM.Server.Controls
{
    public class DecoderState : INotifyPropertyChanged
    {
        private static bool mIsConnected;
        public static bool IsConnected
        {
            get => mIsConnected;
            set
            {
                if (mIsConnected != value)
                {
                    mIsConnected = value;
                    NotifyStaticPropertyChanged("IsConnected");
                    NotifyStaticPropertyChanged("Color");
                    NotifyStaticPropertyChanged("Kind");
                }
            }
        }
        public static SolidColorBrush Color
        {
            get => IsConnected ? Brushes.Green : Brushes.Red;
            set
            {
                NotifyStaticPropertyChanged("Color");
            }
        }
        //public static PackIconMaterialKind Kind = IsConnected ? PackIconMaterialKind.Timer : PackIconMaterialKind.TimerOff;

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

    public class TCPState : INotifyPropertyChanged
    {
        private static TCPState instance;
        public TCPState() { }

        public static TCPState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TCPState();
                }
                return instance;
            }
        }

        private static bool mIsConnected;
        public static bool IsConnected
        {
            get => mIsConnected;
            set
            {
                if (value != mIsConnected)
                {
                    mIsConnected = value;
                    NotifyStaticPropertyChanged("IsConnected");
                    NotifyStaticPropertyChanged("Color");
                    NotifyStaticPropertyChanged("Kind");
                }
            }
        }
        public static SolidColorBrush Color
        {
            get => IsConnected ? Brushes.Green : Brushes.Red;
            set
            {
                NotifyStaticPropertyChanged("Color");
            }
        }
        //public static PackIconMaterialKind Kind => IsConnected ? PackIconMaterialKind.Server : PackIconMaterialKind.ServerOff;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }
    }
}
