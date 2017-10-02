using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RM.Terminal
{
    public class Kart : BaseControl
    {
        public Kart(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public string ID { get; set; }
        public string Name { get; set; }

        public static void RecieveKart(string[] text)
        {
            for(int i = 1; i < text.Length; i = i + 2)
            {
                KartList.KList.Add(new Kart(text[i + 1], text[i]));
                Console.WriteLine($@"New Kart: {text[1]} - {text[2]}");
            }

            //IsRecievingKarts = false;
        }

        public static void AskKart()
        {
            //IsRecievingKarts = true;
            KartList.KList.Clear();
            TCP.Send("K");
        }

        //private static bool mIsRecievingKarts;
        //public static bool IsRecievingKarts
        //{
        //    get => mIsRecievingKarts;
        //    set
        //    {
        //        mIsRecievingKarts = value;
        //        NotifyStaticPropertyChanged("IsRecievingKarts");
        //    }
        //}
    }

    public static class KartList
    {
        public static ObservableCollection<Kart> KList { get; set; } = new ObservableCollection<Kart>();
    }
}