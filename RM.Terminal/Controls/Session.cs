using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RM.Terminal
{
    public static class Session
    {
        /// <summary>
        /// Create session.
        /// </summary>
        /// <param name="name">Name of the session.</param>
        /// <param name="maxParts">Maximum participants.</param>
        public static void Create(string name, int maxParts)
        {
            SessionName = name;
            CourseID = Cryptation.Md5Hash(name);
            Parts = 0;
            MaxParts = maxParts;
            CList = new List<User>();
        }

        public static string SessionName { get; set; }
        public static string CourseID { get; set; }
        public static int Parts { get; set; }
        public static int MaxParts { get; set; }
        public static List<User> CList { get; set; }

        
    }
}
