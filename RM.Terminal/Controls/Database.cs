using Npgsql;
using System;

namespace Racing_Monitor.Controls
{
    internal class Database
    {
        private static NpgsqlConnection _conn;

        public static void Init(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public static bool Connect()
        {
            _conn = new NpgsqlConnection(GetConnectionString());
            try
            {
                _conn.Open();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"ERROR: could not connect to Database. {ex}");
            }
            return IsConnected;
        }

        public static bool Disconnect()
        {
            try
            {
                IsConnected = false;
                _conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"ERROR: Could not disconnect from Database. {ex}");
            }
            return IsConnected;
        }

        private static string GetConnectionString()
        {
            try
            {
                var connectionstring = new NpgsqlConnectionStringBuilder
                {
                    Host = Ip,
                    Timeout = 1,
                    Port = Port,
                    Database = "db",
                    Username = "usr",
                    Password = "pswrd"
                };

                return connectionstring.ToString();
            }
            catch (Exception er) { Console.WriteLine(er.ToString()); return ""; }
        }

        public static string Ip { get; set; }
        public static int Port { get; set; }
        public static bool IsConnected { get; set; }
    }
}
 