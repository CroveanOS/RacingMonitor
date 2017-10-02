using RM.Core;
using System.Security;
using System.Windows.Input;
using System;
using System.Threading.Tasks;

namespace RM.Terminal
{
    public class LoginViewModel : BaseViewModel
    {

        #region Private Methods

        private Account mLoggedUser;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Email of the user
        /// </summary>
        public string Email { get; set; } = "crovean09@gmail.com";

        /// <summary>
        /// The Password of  the user
        /// </summary>
        //public string Password { get; set; } 

        /// <summary>
        /// Is the data written wrong
        /// </summary>
        public bool IsDataWrong { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
        }

        #endregion

        /// <summary>
        /// Attempt to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the user password</param>
        /// <returns></returns>
        public async Task Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer == null) return;

            string password = ToUnsecureString.ConvertToUnsecureString(passwordContainer.Password);

            foreach (Account usr in UserCollection.Users)
            {
                var loginB = !string.IsNullOrEmpty(Email) && Email == usr.Email;
                var passwordB = !string.IsNullOrEmpty(password) && Cryptation.GetSha1(password, usr.Email) == usr.Password;

                if (!loginB || !passwordB) continue;

                mLoggedUser = usr;
                Email = string.Empty;
                password = string.Empty;
                IoC.Application.SideMenuVisible = true;

                break;
            }
            
        }
    }
}
