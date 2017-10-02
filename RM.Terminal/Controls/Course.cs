using System;
using System.Globalization;

namespace RM.Terminal
{
    public class User
    {
        public User(string name, string courseId, string userId, string kartId, bool finished = false)
        {
            Name = name;
            CourseID = courseId;
            UserID = userId;
            KartID = kartId;
            Date = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            Finished = finished;
        }

        public string Name { get; set; }
        public string CourseID { get; set; }
        public string UserID { get; set; }
        public string KartID { get; set; }
        public string Date { get; set; }
        public bool Finished { get; set; }
    }
}
