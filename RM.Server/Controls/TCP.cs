using log4net;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace RM.Server.Controls
{
    internal class TCP
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TCP));
        private static readonly byte[] Data = new byte[1024];

        private static readonly ManualResetEvent SendDone =
            new ManualResetEvent(false);

        public static void Open()
        {
            try
            {
                Stop();
                Server = new TcpListener(IPAddress.Parse(IP), Port);
                Server.Start();
                log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:TCP Server started.");
            }
            catch (SocketException ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Can't start TCP Server: {ex}");
            }
        }

        public static void Pending()
        {
            if (Server == null || BreakTcpLoop) return;
            try
            {
                /*Thread tmpThread = new Thread(() =>*/
                {
                    Client = Server.AcceptTcpClient();
                    TCPState.IsConnected = true;
                    log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:TCP Client connected. Setting TCPState.IsConnected = true.");

                    //TCP Message
                    TcpMessageThread = new Thread(WaitForMessage);
                    TcpMessageThread.Start();
                }
                //);
                //tmpThread.Start();
            }
            catch (Exception ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Error pending TCP client: " + ex);
            }
        }

        /// <summary>
        /// Recieves TCP Message
        /// </summary>
        public static void WaitForMessage()
        {
            if (Client == null || !TCPState.IsConnected || BreakTcpLoop) return;
            while (true)
            {
                string message = Receive();
            }
        }

        public static void Send(string data)
        {
            if (!TCPState.IsConnected) return;
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                Client.Client.Send(byteData);
                if(data != "Ping") log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:Sent: " + data);
            }
            catch (SocketException ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Socket error on sending: {ex}");
                TCPState.IsConnected = false;
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Setting TCPState.IsConnected = false.");
            }
            catch (Exception ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Error sending {data}: {ex}");
                TCPState.IsConnected = false;
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Setting TCPState.IsConnected = false.");
            }
        }

        internal static void GetSettings()
        {
            Port = Convert.ToInt32(Properties.Settings.Default.Port);
            IP = Properties.Settings.Default.IP;
        }

        public static string Receive()
        {
            try
            {
                if (!TCPState.IsConnected || Client == null) return "";
                int recv = Client.Client.Receive(Data);
                string returnData = Encoding.ASCII.GetString(Data, 0, recv);
                if(!returnData.Contains("Ping")) log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:Received: " + returnData);
                GetDirection(returnData);
                return returnData;
            }
            catch(Exception ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Error on receiving message: " + ex);
                TCPState.IsConnected = false;
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Setting TCP.IsConnected = false.");
                return "";
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                TcpClient client = (TcpClient)ar.AsyncState;
                int bytesSent = client.Client.EndSend(ar);
                log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:Sent {bytesSent} bytes to server.");
                SendDone.Set();
            }
            catch (Exception ex)
            {
                log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Error on send callback: " + ex);
            }
        }

        public static void Stop()
        {
            if (!TCPState.IsConnected) return;
            Server?.Stop();
            Server = null;
            Client.Close();
            Client = null;
        }


        /// <summary>
        /// Get direction of the received data.
        /// </summary>
        /// <param name="message"></param>
        private static void GetDirection(string message)
        {
            if (message == "Ping")
            {
                TcpPingTimer.Stop();
                TcpPingTimer.Interval = new TimeSpan(0, 0, 10);
                TcpPingTimer.Start();
                Send("Ping");
            }
            else
            {
                string[] text = message.Split('+');

                switch (text[0])
                {
                    case "K":
                        Kart.SendKarts();
                        break;

                    case "C":
                        if (Database.Insert("Course", "Course_Name, Course_Id, User_Id, Kart_Id, Date",
                            $"'{text[1]}','{text[2]}','{text[3]}','{text[4]}','{text[5]}'"))
                            if(Database.Insert("Laps", "Course_Id, Kart_Id, Time, BestTime ,AbsoluteTime", $"'{text[2]}','{text[4]}','0:00.000','0:00.000','0:00.000'"))
                            {
                                CourseRowList.RefreshList();
                                CourseList.RefreshList();
                                if(LapCollection.LapList.Count(x => x.CourseID == text[2]) > 0)
                                    LapCollection.RefreshList(text[2]);
                            }
                        break;

                    case "P":
                        if (Database.Insert("CoursePlaces", "user,cart,course_id", $"'{text[1]}','{text[2]}','{text[3]}'"))
                        {
                            log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:New user joined Course: {text[1]} - {text[2]} - {text[3]}");
                        }
                        break;

                    case "U":
                        if (Database.Insert("UserStatistics", "User_id,Course_id,Time",
                            $"'{text[1]}','{text[2]}','{text[3]}'"))
                        {
                            log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:New User Statistic: {text[1]} - {text[2]} - {text[3]}");
                        }
                        break;

                    case "R":
                        UserCollection.RefreshList();
                        foreach (User user in UserCollection.UserList)
                        {
                            Send($"R+{user.Email}+{user.Password}+{user.Name}+{user.Surname}+{user.Photo}");
                            log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:Sent user {user.Email} - {user.Name} - {user.Surname}");
                        }
                        break;

                    case "Q":
                        if (Database.Insert("Users", "Email,Name,Surname,Password,Image",
                            $"'{text[1]}','{text[2]}','{text[3]}','{text[4]}','{text[5]}'"))
                        {
                            log.Debug($"[{Settings.GetLine(new StackTrace(true))}]:New User Created: {text[1]} - {text[2]} - {text[3]}");
                        }
                        break;

                    default:
                        log.Error($"[{Settings.GetLine(new StackTrace(true))}]:Unknown message: {message}");
                        break;
                }
            }
        }

        private static int mPort = Convert.ToInt32(Properties.Settings.Default.Port);
        public static int Port
        {
            get => mPort;
            set
            {
                if (mPort != value)
                {
                    mPort = value;
                    Open();
                }
            }
        }

        private static string mIP = Properties.Settings.Default.IP;
        public static string IP
        {
            get => mIP;
            set
            {
                if (mIP != value)
                {
                    mIP = value;
                    Open();
                }
            }
        }

        public static TcpClient Client { get; set; }
        public static TcpListener Server { get; set; }
        public static Thread TcpMessageThread { get; set; }
        public static bool BreakTcpLoop { get; set; }
        public static DispatcherTimer TcpPingTimer { get; set; } = new DispatcherTimer();
    }
}
