using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RM.Server.Controls
{
    public class Laps
    {
        public Laps(string courseID, string name, string surname, string kartID, string time, string bestTime, string absoluteTime, string lap)
        {
            CourseID = courseID;
            Username = $"{surname} {name}";
            KartID = kartID;
            Time = time;
            BestTime = bestTime;
            AbsoluteTime = absoluteTime;
            Lap = lap;
        }
        
        public static string GetTime(Laps laps)
        {
            string last = LapCollection.LapList.Where(y => y.CourseID == laps.CourseID && y.KartID == laps.KartID).Select(x => x.Time).Last();
            if (string.IsNullOrEmpty(last))
                return laps.Time;

            TimeSpan lastSpan, timeSpan;
            if (!TimeSpan.TryParse(last, out lastSpan)) return laps.Time;
            return TimeSpan.TryParse(laps.Time, out timeSpan) ? (timeSpan - lastSpan).ToString() : laps.Time;
        }

        public static string GetBestTime(Laps laps)
        {
            IEnumerable<string> timesString = LapCollection.LapList.Where(y => y.CourseID == laps.CourseID && y.KartID == laps.KartID).Select(x => x.Time);
            ObservableCollection<TimeSpan> times = new ObservableCollection<TimeSpan>();
            foreach (string time in timesString)
            {
                TimeSpan timeSpan;
                if (TimeSpan.TryParse(time, out timeSpan))
                    times.Add(timeSpan);
            }

            if(times.Any())
                return times.Min().ToString();
            return "";
        }
        
        public string CourseID { get; set; }
        public string Username { get; set; }
        public string KartID { get; set; }
        public string Time { get; set; }
        public string BestTime { get; set; }
        public string AbsoluteTime { get; set; }        
        public string Lap { get; set; }
    }

    public class UserLaps
    {
        public UserLaps(string courseID, string kartID, string time, int lap)
        {
            CourseID = courseID;
            KartID = kartID;
            Time = time;
            Lap = lap;
        }
        
        public string CourseID { get; set; }
        public string KartID { get; set; }
        public string Time { get; set; }
        public int Lap { get; set; }
    }

    public class UserLapsList
    {
        public static void RefreshList()
        {
            Database.GetUserLaps();
        }
        
        public static ObservableCollection<UserLaps> ULList { get; set; } = new ObservableCollection<UserLaps>();
    }

    public class CurrentLaps
    {
        public static void SetCurrentLaps(string courseID)
        {
            CLaps?.Clear();
            CLaps = (ObservableCollection<Laps>) LapCollection.LapList.Where(x => x.CourseID.Contains(courseID));
        }

        public static ObservableCollection<Laps> CLaps { get; set; } = new ObservableCollection<Laps>();
    }

    public class LapCollection : INotifyPropertyChanged
    {
        private static LapCollection instance;
        public LapCollection() { }

        public static LapCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LapCollection();
                }
                return instance;
            }
        }

        public static void RefreshList(string courseId)
        {
            Database.GetLaps(courseId);
        }
        
        private static ObservableCollection<Laps> mLapList = new ObservableCollection<Laps>();
        public static ObservableCollection<Laps> LapList
        {
            get => mLapList;
            set
            {
                if(value != mLapList)
                {
                    mLapList = value;
                    NotifyStaticPropertyChanged("LapList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }
    }
}