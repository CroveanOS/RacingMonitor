using RM.Core;
using System.Windows.Input;
using System;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class KartTypesViewModel : BaseViewModel
    {
        #region Commands

        public ICommand CancelCommand { get; set; }
        public ICommand KidsCommand { get; set; }
        public ICommand AdultsCommand { get; set; }
        public ICommand BipostoCommand { get; set; }

        #endregion

        public KartTypesViewModel()
        {
            CancelCommand = new RelayCommand(async () => await CancelAsync());
            KidsCommand = new RelayCommand(async () => await KidsAsync());
            AdultsCommand = new RelayCommand(async () => await AdultsAsync());
            BipostoCommand = new RelayCommand(async () => await BipostoAsync());
        }

        private async Task BipostoAsync()
        {
            IoC.Application.CurrentFlyoutPage = ApplicationPage.Kart;
        }

        private async Task AdultsAsync()
        {
            IoC.Application.CurrentFlyoutPage = ApplicationPage.Kart;
        }

        private async Task KidsAsync()
        {
            IoC.Application.CurrentFlyoutPage = ApplicationPage.Kart;
        }

        private async Task CancelAsync()
        {
            IoC.Application.SideMenuVisible = false;
            IoC.Application.CurrentSlidePage = ApplicationPage.Login;
        }
    }
}
