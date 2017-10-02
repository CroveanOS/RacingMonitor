using System;
using System.Security;
using RM.Core;

namespace RM.Terminal
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public SecureString Password => UserPassword.SecurePassword;
    }
}
