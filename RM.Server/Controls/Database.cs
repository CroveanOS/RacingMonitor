using log4net;
using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace RM.Server.Controls
{
    internal static class Database
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Database));
        private static SQLiteConnection _sqlCon;
        private static SQLiteCommand _sqlCmd = new SQLiteCommand();

        public static bool Connect()
        {
            if (!SetConnection()) return IsConnected;

            try
            {
                IsConnected = true;
                Log.Debug($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Database connected.");
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error in connection to DB: {ex}");
            }
            return IsConnected;
        }

        public static bool Disconnect()
        {
            try
            {
                IsConnected = false;
                _sqlCon?.Close();
                Log.Debug($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Database disconnected.");
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error in disconnectiong  DB: {ex}");
            }
            return IsConnected;
        }

        public static bool Insert(string table, string columns, string values)
        {
            if (columns.Split(',').Length != values.Split(',').Length || !IsConnected) return false;
            string querryString = $"INSERT INTO {table} ({columns}) VALUES ({values})";

            if (!SetQuery(querryString)) return false;
            Log.Debug($"Inserted {values} in {columns} of {table} table.");
            switch (table)
            {
                case "Users":
                    GetUsers();
                    return true;

                default:
                    return true;
            }
        }

        public static bool Update(string table, string column, string value, params string[] where)
        {
            if (!IsConnected) return false;
            string[] columns = column.Split(',');
            string[] values = value.Split(',');
            string qr = "";
            for(int i = 0; i < columns.Length; i++)
            {
                qr += $"{columns[i]} = {values[i]},";
            }
            qr = qr.Substring(0, qr.Length - 1);

            string querryString = $"UPDATE {table} SET {qr} where ({where[0]})";

            if (!SetQuery(querryString)) return false;
            Log.Debug($"Updated {value} with {column} in {table} table.");
            return true;
        }

        public static DataTable GetSpecificData(string columns, string table, params string[] param)
        {
            DataTable dt = new DataTable();
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                    _sqlCon.Open();
                _sqlCmd.CommandText = $"SELECT {columns} FROM {table} where {param[0]} = '{param[1]}'";
                _sqlCmd.Connection = _sqlCon;

                using (SQLiteDataAdapter reader = new SQLiteDataAdapter(_sqlCmd))
                {
                    reader.Fill(dt);
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Fail getting data: {ex}");
            }
            return dt;
        }

        public static DataTable GetAllData(string table, string columns = "*")
        {
            DataTable dt = new DataTable();
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                    _sqlCon.Open();
                _sqlCmd.CommandText = $"SELECT {columns} FROM {table}";
                _sqlCmd.Connection = _sqlCon;

                using (SQLiteDataAdapter reader = new SQLiteDataAdapter(_sqlCmd))
                {
                    reader.Fill(dt);
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Fail getting data: {ex}");
            }
            return dt;
        }

        public static void GetUsers()
        {
            DataTable dt = GetAllData("Users");
            foreach (DataRow dataRow in dt.Rows)
            {
                TCP.Send($"R+{dataRow["Email"]}+{dataRow["Password"]}+{dataRow["Name"]}+{dataRow["Surname"]}+{dataRow["Image"]}");
            }
        }
        
        public static void GetLaps(string courseId)
        {
            try
            {
                if (_sqlCon.State == ConnectionState.Closed) _sqlCon.Open();
                _sqlCmd.CommandText = "SELECT Laps.*, Users.Name, Users.Surname " +
                                      "FROM Laps " +
                                      "LEFT JOIN Course ON Laps.Course_Id = Course.Course_Id " +
                                      "LEFT JOIN Users ON Course.User_Id = Users.Email " +
                                      $"WHERE Laps.Course_Id = '{courseId}'";
                _sqlCmd.Connection = _sqlCon;

                using (SQLiteDataReader reader = _sqlCmd.ExecuteReader())
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(delegate
                    {
                        LapCollection.LapList.Clear();
                        while (reader.Read())
                        {
                            LapCollection.LapList.Add(
                                new Laps(
                                    reader["Course_Id"].ToString(),
                                    reader["Name"].ToString(),
                                    reader["Surname"].ToString(),
                                    reader["Kart_Id"].ToString(),
                                    reader["Time"].ToString(),
                                    reader["BestTime"].ToString(),
                                    reader["AbsoluteTime"].ToString(),
                                    reader["Laps"].ToString()));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error reading places: {ex}");
            }
        }
        
        public static void GetUserLaps()
        {
            try
            {
                if (_sqlCon.State == ConnectionState.Closed) _sqlCon.Open();
                _sqlCmd.CommandText = "SELECT * FROM UserLaps";
                _sqlCmd.Connection = _sqlCon;

                using (SQLiteDataReader reader = _sqlCmd.ExecuteReader())
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(delegate
                    {
                        UserLapsList.ULList.Clear();
                        while (reader.Read())
                        {
                            UserLapsList.ULList.Add(
                                new UserLaps(
                                    reader["Course_Id"].ToString(),
                                    reader["Kart_Id"].ToString(),
                                    reader["Time"].ToString(),
                                    Convert.ToInt32(reader["Lap"])));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error reading user laps: {ex}");
            }
        }

        public static bool SetQuery(string txtQuery)
        {
            try
            {
                if (_sqlCon.State == ConnectionState.Closed) _sqlCon.Open();
                _sqlCmd = _sqlCon.CreateCommand();
                _sqlCmd.CommandText = txtQuery;
                _sqlCmd.Connection = _sqlCon;
                _sqlCmd.ExecuteNonQuery();
                _sqlCon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error executioning query {txtQuery}: {ex}");
                return false;
            }
        }
        

        private static bool SetConnection()
        {
            try
            {
                _sqlCon = new SQLiteConnection(@"Data Source="".\rmdb"";Version=3;New=False;Compress=True;");
                Log.Debug($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Got database connection string.");
                //_sqlCon = new SQLiteConnection(@"Data Source=D:/Visual Studio/Racing Monitor/Racing Monitor/RM Server/rmdb;Version=3;New=False;Compress=True;");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"[{Settings.GetLine(new StackTrace(true)).ToString()}]:Error getting database connection string: " + ex);
                return false;
            }
        }

        public static bool IsConnected { get; set; }

    }
}
