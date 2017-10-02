using RM.Core;
using System.Windows.Input;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class SelectionViewModel : BaseViewModel
    {
        #region Public Properties


        public string SessionName { get; set; } = Session.SessionName;
        public string PlayersSigned { get; set; } = $"{Session.Parts.ToString()}/{Session.MaxParts.ToString()} players signed in";

        public bool IsLoginActivePage { get; set; }
        public bool IsRegisterActivePage { get; set; }
        public bool IsOneCourseActivePage { get; set; }

        #endregion

        #region Commands
        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand OneRaceCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructor

        public SelectionViewModel()
        {
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
            OneRaceCommand = new RelayCommand(async () => await OneRaceAsync());
            CancelCommand = new RelayCommand(async () => await CancelAsync());
        }

        private async Task CancelAsync()
        {
            IoC.Application.GoToPage(ApplicationPage.Course);
        }

        private async Task LoginAsync()
        {
            IoC.Application.GoToSlidePage(ApplicationPage.Login);

            IsLoginActivePage = false;
            IsRegisterActivePage = true;
            IsOneCourseActivePage = true;
        }

        private async Task RegisterAsync()
        {
            IoC.Application.GoToSlidePage(ApplicationPage.Register);

            IsLoginActivePage = true;
            IsRegisterActivePage = false;
            IsOneCourseActivePage = true;
        }

        private async Task OneRaceAsync()
        {
            IoC.Application.GoToSlidePage(ApplicationPage.OneRace);

            IsLoginActivePage = true;
            IsRegisterActivePage = true;
            IsOneCourseActivePage = false;
        }

        #endregion
    }
}
