using System;
using System.Text;
using System.Windows.Threading;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace RM.Server.Controls
{
    public static class Decoder
    {
        public static SerialPortManager SpManager;

        public static void Init()
        {
            SpManager = new SerialPortManager();
            SpManager.SetConnection();

            SpManager.NewSerialDataRecieved += _spManager_NewSerialDataRecieved;
        }

        private static void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
            if (!dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), sender, e);
                return;
            }

            string data = Encoding.ASCII.GetString(e.Data);
            if (data == "\r")
            {
                DecodeMessage(Timing);
            }
            else
            {
                Timing += data;
            }
        }

        private static void DecodeMessage(string decodedMessage)
        {
            string plainMessage = new string(decodedMessage.Where(char.IsDigit).ToArray());
            Console.WriteLine(plainMessage);

            string cartNo = plainMessage.Substring(0, 2);
            if (cartNo != "00")
            {
                Laps record = LapCollection.LapList.Where(x => x.KartID == cartNo).Last();

                record.Lap = (Convert.ToInt32(record.Lap) + 1).ToString();
                record.AbsoluteTime = $"{int.Parse(plainMessage.Substring(2, 6)).ToString()}.{plainMessage.Substring(8, 3)}";
                string bestTime = Laps.GetBestTime(record);
                string time = Laps.GetTime(record);
                Database.Update("Laps", "Time,Laps,BestTime,AbsoluteTime", $"'{time}','{record.Lap}','{bestTime}','{record.Time}'",$"Course_Id LIKE '{record.CourseID}'");
                LapCollection.RefreshList(record.CourseID);

                Database.Insert("UserLaps", "Course_Id, Kart_Id, Time, Lap",
                    $"'{record.CourseID}','{record.KartID}','{time}','{record.Lap}'");
                UserLapsList.RefreshList();
            }
            Timing = "";
        }

        private static string Timing { get; set; }
    }
}