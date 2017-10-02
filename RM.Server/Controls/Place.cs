using System.Collections.Generic;

namespace RM.Server.Controls
{
    public class Place
    {
        public Place(string user, string cart, string courseId)
        {
            User = user;
            Cart = cart;
            CourseID = courseId;
        }

        public string User { get; set; }
        public string Cart { get; set; }
        public string CourseID { get; set; }
    }

    public static class PlaceList
    {
        public static List<Place> Places { get; set; } = new List<Place>();
    }
}
