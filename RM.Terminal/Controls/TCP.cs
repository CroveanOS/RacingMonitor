using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Threading;

namespace RM.Terminal
{
    /// <summary>
    /// TCP connection class
    /// </summary>
    public class TCP : BaseControl
    {
        #region Private Methods

        /// <summary>
        /// Length of TCP message in bytes
        /// </summary>
        private static readonly byte[] Data = new byte[1024];

        /// <summary>
        /// Interval of sneding the Ping message
        /// </summary>
        private static TimeSpan PingTimeSpan = new TimeSpan(0, 0, 10);

        /// <summary>
        /// TCP connection state
        /// </summary>
        private static bool mConnected;

        #endregion

        #region Public Properties

        /// <summary>
        /// TCP Connection IP
        /// </summary>
        public static string IP { get; set; } = "127.0.0.1";

        /// <summary>
        /// TCP Connection Port
        /// </summary>
        public static int Port { get; set; } = 9800;

        /// <summary>
        /// TCP client
        /// </summary>
        public static TcpClient Client { get; set; }

        /// <summary>
        /// Timer for sending the ping each n seconds, set by <see cref="PingTimeSpan"/>
        /// </summary>
        public static DispatcherTimer TcpPingTimer { get; set; } = new DispatcherTimer();

        /// <summary>
        /// TCP connection state
        /// </summary>
        public static bool Connected
        {
            get => mConnected;
            set
            {
                if (mConnected == value) return;
                mConnected = value;
                NotifyStaticPropertyChanged("Connected");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TCP()
        {
            Open();
        }

        #endregion

        #region Open / Close

        /// <summary>
        /// Opens connection with TCP Server
        /// </summary>
        public static void Open()
        {
            try
            {
                // Create TCP Client
                Client = new TcpClient();

                // Connect the client to the server on the given IP and Port
                Client.ConnectAsync(IP, Port);

                Connected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error Connecting to TCP: {ex.Message}");
            }
        }

        /// <summary>
        /// Close connection with TCP Server
        /// </summary>
        public static void Close()
        {
            // Return if the client is not connected and client is not null
            if (!Connected && Client != null) return;

            // Close the connection
            Client.Close();
        }

        #endregion

        #region Send / Receive

        /// <summary>
        /// Send data to TCP Server
        /// </summary>
        /// <param name="data"></param>
        public static void Send(string data)
        {
            try
            {
                // Conevrt message to bytes
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Send message
                Client.Client.Send(byteData);
            }
            catch
            {
                // Set Client state to false
                Connected = false;
            }
        }

        /// <summary>
        /// Receive data from TCP Server
        /// </summary>
        /// <returns></returns>
        public static void Receive()
        {
            try
            {
                // Return if the client is not connected and client is not null
                if (!Connected && Client != null) return;

                // Recieve data from server
                int recv = Client.Client.Receive(Data);

                // Convert message to string
                string returnData = Encoding.ASCII.GetString(Data, 0, recv);

                // Get the direction of the message
                GetDirection(returnData);
            }
            catch
            {
                // Set Client state to false
                Connected = false;

                return;
            }
        }

        #endregion

        #region Functionality

        /// <summary>
        /// Get direction of the received data
        /// </summary>
        /// <param name="message"></param>
        private static void GetDirection(string message)
        {
            // If received message is 'Ping'
            if (message == "Ping")
            {
                // Reset the ping timer
                ResetTimer();
            }
            else
            {
                // Split the message on each appearence of '+'
                string[] text = message.Split('+');

                // If the first symbol is...
                switch (text[0])
                {
                    case "K":
                        // Recieve the list of karts
                        Kart.RecieveKart(text);
                        break;

                    case "R":
                        // Recieve the list of users
                        UserCollection.Users.Add(new Account(text[1], text[2], text[3], text[4], text[5]));
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Reset the ping timer
        /// </summary>
        private static void ResetTimer()
        {
            // Stop the timer
            TcpPingTimer.Stop();

            // Set timer's interval
            TcpPingTimer.Interval = PingTimeSpan;

            // Start the timer
            TcpPingTimer.Start();

            // Set the client's state to true
            Connected = true;
        }

        #endregion
    }
}
