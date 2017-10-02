using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace RM.Terminal
{
    public partial class MainWindow
    {
        public ApplicationViewModel ApplicationViewModel { get; set; } = new ApplicationViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}
