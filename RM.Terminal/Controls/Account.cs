using System.Collections.Generic;

namespace RM.Terminal
{
    public class Account
    {
        public Account(string email, string password, string name, string surname, string photo)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Photo = photo;
            InCourse = false;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
        public bool InCourse { get; set; }
    }

    public static class UserCollection
    {
        public static void Update()
        {
            Users.Clear();
            TCP.Send("R");
        }

        public static List<Account> Users { get; set; } = new List<Account>();
    }
}
