using System;
using System.Collections.Generic;
using System.Data;

namespace RM.Server.Controls
{
    public class Kart
    {
        public Kart(string name, bool available, string transID)
        {
            Name = name;
            Available = available;
            TransID = transID;
        }
        
        private static bool AddKart(Kart kart)
        {
            string querryString = $"INSERT INTO Kart (Kart, Available, Trans_Id) " +
                                  $"VALUES ('{kart.Name}','{kart.Available}','{kart.TransID}')";

            KartList.KList.Add(kart);
            
            return Database.SetQuery(querryString);
        }
        
        public string Name { get; set; }
        public bool Available { get; set; }
        public string TransID { get; set; }

        internal static void SendKarts()
        {
            KartList.RefreshList();
            string message = "K";
            foreach (Kart kart in KartList.KList)
            {
                if (kart.Available && kart.TransID != string.Empty)
                    message += $"+{kart.Name}+{kart.TransID}";
            }
            TCP.Send(message);
        }
    }

    public class KartList
    {
        public static void RefreshList()
        {
            KList.Clear();
            DataTable dt = Database.GetAllData("Kart");
            foreach (DataRow dataRow in dt.Rows)
            {
                KList.Add(new Kart(
                    dataRow["Kart"].ToString(),
                    Convert.ToBoolean(dataRow["Available"]),
                    dataRow["Trans_Id"].ToString()));
            }
        }
        
        public static List<Kart> KList { get; set; } = new List<Kart>();
    }
}