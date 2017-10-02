using RM.Core;

namespace RM.Terminal
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Idle;
        public ApplicationPage CurrentSlidePage { get; set; } = ApplicationPage.Login;
        public ApplicationPage CurrentFlyoutPage { get; set; } = ApplicationPage.KartType;

        public bool SideMenuVisible { get; set; }

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }

        public void GoToSlidePage(ApplicationPage page)
        {
            CurrentSlidePage = page;
        }

        public void GoToFlyoutPage(ApplicationPage page)
        {
            CurrentFlyoutPage = page;
        }
    }
}
