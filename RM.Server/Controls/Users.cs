using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RM.Server.Controls
{
    public class User
    {
        public User(string email, string password, string name, string surname, string photo)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Photo = photo;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
    }

    internal class UserCollection
    {
        public static void RefreshList()
        {
            UserList.Clear();
            DataTable dt = Database.GetAllData("Users");
            foreach (DataRow dataRow in dt.Rows)
            {
                UserList.Add(new User(
                    dataRow["Email"].ToString(),
                    dataRow["Password"].ToString(),
                    dataRow["Name"].ToString(),
                    dataRow["Surname"].ToString(),
                    dataRow["Image"].ToString()));
            }
        }
        
        public static List<User> UserList { get; set; } = new List<User>();
    }
}