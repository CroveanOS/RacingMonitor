using RM.Core;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace RM.Terminal
{
    public class MainViewModel : BaseViewModel
    {      
        public MainViewModel()
        {
            //TCP Ping
            TCP.TcpPingTimer.Interval = new TimeSpan(0, 0, 10);
            TCP.TcpPingTimer.Tick += _tcpPingTimer_Tick;
            TCP.TcpPingTimer.Start();

            DispatcherTimer mainLoopTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 5) };
            mainLoopTimer.Tick += MainLoopTimerOnTick;
            mainLoopTimer.Start();

            //TCP Message
            Thread t = new Thread(WaitForMessage);
            t.Start();
        }

        private static void _tcpPingTimer_Tick(object sender, EventArgs e)
        {
            if (TCP.TcpPingTimer.Interval == new TimeSpan(0, 0, 10))
            {
                TCP.Connected = false;
            }
        }

        /// <summary>
        /// Recieves TCP Message
        /// </summary>
        private static void WaitForMessage()
        {
            while (true)
            {
                TCP.Receive();
            }
        }

        /// <summary>
        /// Calls checks each n seconds.
        /// </summary>
        private void MainLoopTimerOnTick(object sender, EventArgs eventArgs)
        {
            //TCP Connection
            if (TCP.Connected) TCP.Send("Ping");
            if (TCP.Client != null) TCP.Connected = TCP.Client.Connected;
            if (!TCP.Connected) TCP.Open();
        }
    }
}
