using RM.Core;
using System.Security;
using System.Windows.Input;
using System;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class OneRaceViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The Email of the user
        /// </summary>
        public string Name { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion Commands

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public OneRaceViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(async () => await Login());
        }

        #endregion Constructor


        /// <summary>
        /// Attempt to log the user in
        /// </summary>
        public async Task Login()
        {
            LoggedAccount.Value = new Account("-", "-", Name, "", "");
            IoC.Application.SideMenuVisible = true;
        }
    }
}
