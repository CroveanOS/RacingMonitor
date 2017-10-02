using System.Windows;

namespace Racing_Monitor
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            const string login = "admin";
            const string password = "max123";

            var loginB = !string.IsNullOrEmpty(loginBox.Text) && loginBox.Text == login;
            loginBox.Tag = string.IsNullOrEmpty(loginBox.Text) || loginBox.Text != login ? "2" : "1";
            var passwordB = !string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password == password;
            passwordBox.Tag = string.IsNullOrEmpty(passwordBox.Password) || passwordBox.Password != password ? "2" : "1";

            if (!loginB || !passwordB) return;
            //SettingsWindow settings = new SettingsWindow();
            //settings.ShowDialog();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
