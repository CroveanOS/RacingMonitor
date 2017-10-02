using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Ink;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RM.Server.Controls
{
    public class CourseRow
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CourseRow));
        
        public CourseRow(string name, string courseId, string userId, string kartId, string date, bool finished = false)
        {
            Name = name;
            CourseID = courseId;
            UserID = userId;
            KartID = kartId;
            Date = date;
            Finished = finished;
        }

        public string Name { get; set; }
        public string CourseID { get; set; }
        public string UserID { get; set; }
        public string KartID { get; set; }
        public string Date { get; set; }
        public bool Finished { get; set; }
    }

    public class CourseRowList : INotifyPropertyChanged
    {
        private static CourseRowList instance;
        public CourseRowList() { }

        public static CourseRowList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseRowList();
                }
                return instance;
            }
        }

        public static void RefreshList()
        {
            CRList.Clear();
            DataTable dt = Database.GetAllData("Course");
            foreach (DataRow dataRow in dt.Rows)
            {
                CRList.Add(new CourseRow(
                    dataRow["Course_Name"].ToString(),
                    dataRow["Course_Id"].ToString(),
                    dataRow["User_Id"].ToString(),
                    dataRow["Kart_Id"].ToString(),
                    dataRow["Date"].ToString(),
                    Convert.ToBoolean(dataRow["Finished"])));
            }
        }

        private static List<CourseRow> mCRList = new List<CourseRow>();
        public static List<CourseRow> CRList
        {
            get => mCRList;
            set
            {
                mCRList = value;
                NotifyStaticPropertyChanged("CRList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }
    }

    public class Course
    {
        public Course(string name, string courseID, string count)
        {
            Name = name;
            CourseID = courseID;
            Count = count;
        }

        public string Name { get; set; }
        public string CourseID { get; set; }
        public string Date { get; set; }
        public string Count { get; set; }
    }

    
    public class CourseList : INotifyPropertyChanged
    {
        public static void RefreshList()
        {
            if (CourseRowList.CRList == null) return;

            var result =
                from courseRow in CourseRowList.CRList
                group courseRow by new { courseRow.CourseID, courseRow.Name }
                into c
                select new { c.Key.CourseID, c.Key.Name, Count = c.Count() };

            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                CList.Clear();
                foreach (var res in result)
                    CList.Add(new Course(res.Name, res.CourseID, res.Count.ToString()));
            }
            );

            CList.CollectionChanged += CList_CollectionChanged;
        }

        private static void CList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyStaticPropertyChanged("CList");
        }

        private static readonly ObservableCollection<Course> mCList = new ObservableCollection<Course>();
        public static ObservableCollection<Course> CList
        {
            get => mCList;
        }

        #region PropertyChanged
        private static CourseList instance;
        public CourseList() { }

        public static CourseList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseList();
                }
                return instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public static event PropertyChangedEventHandler StaticPropertyChanged = (sender, e) => { };

        public static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }
        #endregion  
    }
}
