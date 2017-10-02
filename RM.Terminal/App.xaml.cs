using System.Windows;

namespace RM.Terminal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Setup();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            TCP.Close();
        }
    }
}
