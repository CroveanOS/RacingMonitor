using RM.Core;
using System.Security;
using System.Windows.Input;
using System;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The Email of the user
        /// </summary>
        public string Email { get; set; }

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
        public RegisterViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
        }

        #endregion Constructor


        /// <summary>
        /// Attempt to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the user password</param>
        /// <returns></returns>
        public async Task Login(object parameter)
        {
            IoC.Application.SideMenuVisible = true;
        }
    }
}
