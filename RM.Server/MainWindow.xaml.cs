using System.Threading;
using System.Windows;
using RM.Server.Controls;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Decoder = RM.Server.Controls.Decoder;
using log4net;
using System.Diagnostics;

namespace RM.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //DataContext = new MainViewModel(this);
            //Initialize background processes.
            Init();
        }

        /// <summary>
        /// Main initializer.
        /// </summary>
        private void Init()
        {
            //Database
            log.Debug(@"Initializing connections.");
            if (Database.Connect())
            {
                GetSpecificCollections();

                //BindingOperations.EnableCollectionSynchronization(CourseRowList.CRList, SyncLock);
            }
            else
            {
                MessageBox.Show("Database issue.");
            }

            CourseList.RefreshList();

            //Global loop
            DispatcherTimer mainLoopTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3) };
            mainLoopTimer.Tick += MainLoopTimerOnTick;
            mainLoopTimer.Start();

            //TCP callback
            if (!TCPState.IsConnected) TCP.Open();
            MainLoopThread = new Thread(TcpLoop);
            MainLoopThread.Start();

            //TCP Ping
            TCP.TcpPingTimer.Interval = new TimeSpan(0, 0, 10);
            TCP.TcpPingTimer.Tick += _tcpPingTimer_Tick;
            TCP.TcpPingTimer.Start();

            //if (CourseList.CList != null) CourseGrid.ItemsSource = CourseList.CList;

            /*//Course Timer
            _courseTimer.Interval = TimeSpan.FromMilliseconds(2000);
            _courseTimer.Tick += _Timer_Tick;
            _courseTimer.Start();*/

            TimerLabel.Content = "00:00.00";
        }

        private void SetSettings()
        {
            //Terminal
            TCP.GetSettings();
            TerminalIpTextBox.Text = TCP.IP;
            TerminalPortTextBox.Text = TCP.Port.ToString();

            //Decoder
            SerialPortManager spm = new SerialPortManager();
            PortNameComboBox.ItemsSource = spm.CurrentSerialSettings.PortNameCollection;
            BaudRateComboBox.ItemsSource = spm.CurrentSerialSettings.BaudRateCollection;
            DataBitsComboBox.ItemsSource = spm.CurrentSerialSettings.DataBitsCollection;
            ParityComboBox.ItemsSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            StopBitsComboBox.ItemsSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));

            try
            {
                spm.CurrentSerialSettings.GetSettings();

                PortNameComboBox.SelectedValue = spm.CurrentSerialSettings.PortNameCollection.Any(x => x == spm.CurrentSerialSettings.PortName) ? spm.CurrentSerialSettings.PortName : null;
                BaudRateComboBox.SelectedValue = spm.CurrentSerialSettings.BaudRate;
                DataBitsComboBox.SelectedValue = spm.CurrentSerialSettings.DataBits;
                ParityComboBox.SelectedValue = spm.CurrentSerialSettings.Parity;
                StopBitsComboBox.SelectedValue = spm.CurrentSerialSettings.StopBits;
            }
            catch(Exception ex)
            {
                log.Error($"[{ Settings.GetLine(new StackTrace(true))}]:Error getting the settings: {ex}");
            }

        }

        /// <summary>
        /// Check TCP connection.
        /// </summary>
        private void TcpLoop()
        {
            while (true)
            {
                TCP.Pending();
                if (TCP.BreakTcpLoop)
                    break;
            }
        }

        #region OnClick Events

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Terminal
                Properties.Settings.Default.IP = TerminalIpTextBox.Text;
                Properties.Settings.Default.Port = TerminalPortTextBox.Text;

                //Decoder
                Properties.Settings.Default.PortName = PortNameComboBox.SelectedValue?.ToString();
                Properties.Settings.Default.BaudRate = BaudRateComboBox.SelectedValue?.ToString();
                Properties.Settings.Default.DataBits = DataBitsComboBox.SelectedValue?.ToString();
                Properties.Settings.Default.Parity = ParityComboBox.SelectedValue?.ToString();
                Properties.Settings.Default.StopBits = StopBitsComboBox.SelectedValue?.ToString();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                //SettingsFlyout.IsOpen = false;
            }
            catch (Exception ex)
            {
                log.Error($"[{ Settings.GetLine(new StackTrace(true))}]:Error saving settings: {ex}");
            }
        }

        private void TimerStartBtn_Click(object sender, RoutedEventArgs e)
        {
            Decoder.Init();
            Decoder.SpManager.StartListening();
            _decoderTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Background, T_Tick, Dispatcher.CurrentDispatcher)
            {
                IsEnabled = true
            };
            _start = DateTime.Now;
            CourseGrid.IsEnabled = false;
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SetSettings();
            //SettingsFlyout.IsOpen = true;
        }

        private void TimerStopBtn_Click(object sender, RoutedEventArgs e)
        {
            _decoderTimer.Stop();
            Decoder.SpManager.StopListening();
            CourseGrid.IsEnabled = true;
        }

        private void TimerResetBtn_Click(object sender, RoutedEventArgs e)
        {
            TimerLabel.Content = "00:00.00";
            CourseGrid.IsEnabled = true;
        }

        #endregion

        #region Timer Ticks

        /// <summary>
        /// Calls checks each 1 second.
        /// </summary>
        private void MainLoopTimerOnTick(object sender, EventArgs eventArgs)
        {
            //Decoder
            /*if (!DecoderState.IsConnected)
                Decoder.Init();*/
        }

        private void _tcpPingTimer_Tick(object sender, EventArgs e)
        {
            if (TCP.TcpPingTimer.Interval == new TimeSpan(0, 0, 15))
            {
                log.Error("No response from client. TCP.IsConnected = false");
                TCPState.IsConnected = false;
            }
        }

        /*private void _Timer_Tick(object sender, EventArgs e)
        {
            var selected = CourseGrid.SelectedIndex;
            CourseGrid.ItemsSource = null;
            GetSpecificCollections();
            if (CourseList.CList != null) CourseGrid.ItemsSource = CourseList.CList;
            CourseGrid.SelectedIndex = selected;

            SetSpecificLap();
        }*/

        private void T_Tick(object sender, EventArgs e)
        {
            TimeSpan currentTime = DateTime.Now - _start;
            TimerLabel.Content = currentTime.ToString(@"mm\:ss\.fff");
        }

        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_courseTimer != null)
            {
                _courseTimer.Stop();
                log.Debug("Course Timer stopped.");
            }
            if (_decoderTimer != null)
            {
                _decoderTimer.Stop();
                log.Debug("Decoder Timer stopped.");
            }

            TCP.BreakTcpLoop = true;
            log.Debug("Break TCP loop.");

            if (Decoder.SpManager != null)
            {
                Decoder.SpManager.StopListening();
                log.Debug("Serial Port Manager stopped.");

                Decoder.SpManager.Dispose();
                log.Debug("Serial Port Manager disposed.");
            }
        }

        private void CourseGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Course item = (Course)CourseGrid.SelectedItem;

            if (_lastSelected == null)
                _lastSelected = item;

            LapCollection.RefreshList(item?.CourseID);
        }

        private void GetSpecificCollections()
        {
            CourseRowList.RefreshList();
        }

        #region Private Variables
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));
        private readonly DispatcherTimer _courseTimer = new DispatcherTimer();
        private static readonly object SyncLock = new object();
        private DispatcherTimer _decoderTimer;
        private Thread MainLoopThread;
        private DateTime _start;
        private Course _lastSelected;
        #endregion

        public static SerialPortManager SpManager { get; set; } = new SerialPortManager();       
    }

    public static class Track
    {
        public static string ID { get; set; }
        public static string Time { get; set; }
    }
}
