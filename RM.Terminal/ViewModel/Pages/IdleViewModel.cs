using RM.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace RM.Terminal
{
    public class IdleViewModel : BaseViewModel
    {
        #region Private Methods

        /// <summary>
        /// Set color of indicator for TCP Server connection
        /// </summary>
        private SolidColorBrush mTcpConnectedIndicator = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff1744"));

        private bool mTcpConnected;
        #endregion

        #region Public Properties

        /// <summary>
        /// Command for start button
        /// </summary>
        public ICommand StartCommand { get; set; }

        /// <summary>
        /// Is the client connected to the TCP Server
        /// </summary>
        public bool TcpConnected
        {
            get => mTcpConnected;
            set
            {
                if (mTcpConnected == TCP.Connected) return;
                mTcpConnected = TCP.Connected;
                OnPropertyChanged("TcpConnectedIndicator");
            }
        }

        /// <summary>
        /// Set color of indicator for TCP Server connection
        /// </summary>
        public SolidColorBrush TcpConnectedIndicator
        {
            get => mTcpConnectedIndicator;
            set
            {
                if (mTcpConnectedIndicator == value) return;
                mTcpConnectedIndicator = TcpConnected ?
                    (SolidColorBrush)(new BrushConverter().ConvertFrom("#4cc140")) :
                    (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff1744"));

                OnPropertyChanged("TcpConnectedIndicator");
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public IdleViewModel()
        {
            // Set command for start button
            StartCommand = new RelayCommand(async () => await StartAsync());

            TCP tcp = new TCP();
        }

        #endregion

        public async Task StartAsync()
        {
            IoC.Application.GoToPage(ApplicationPage.Course);
        }
    }
}
