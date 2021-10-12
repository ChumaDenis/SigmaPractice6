using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    enum Day {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday};

    public class User
    {
        private string ip;
        private DateTime time;
        private string day;
        private int count = 1;
        public int Count 
        {
            set { count=value; }
            get { return count; }
        }
        public string Ip 
        { 
            set { this.ip = value; }
            get { return ip; } 
        }
        public DateTime Time
        {
            set { this.time = value; }
            get { return time; }
        }
        public string Day
        {
            set { this.day = value; }
            get { return day; }
        }
        public User(string str)
        {
            string[] tmp = str.Split(' ');
            ip= tmp[0];
            time = Convert.ToDateTime(tmp[1]);
            day = tmp[2];
        }
    }

    class Check
    {
        private User[] A;
        public Check(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    string[] def = sr.ReadToEnd().Split('\n');
                    A = new User[def.Length];
                    for (int i = 0; i < def.Length; i++)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            if (def[i].Split(' ')[0] == A[k].Ip)
                            {
                                A[k].Count++;
                                A[i] = new User(def[i]);
                                A[i].Count = A[k].Count++;
                            }
                            else
                                A[i] = new User(def[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong {ex}");
            }
        }

        public Day MostPopularDay()
        {
            int MaxPop = 0;
            Day MaxDay= Day.Monday;
            Day tempDay = Day.Monday;
            for (int i = 0; i<7;i++ , tempDay++)
            {
                int count = 0;
                for (int n = 0; n < A.Length; n++)
                {
                    if(A[n].Day==tempDay.ToString())
                    {
                        count++;
                    }
                }
                if (MaxPop <count)
                {
                    MaxPop = count;
                    MaxDay = tempDay;
                }
            }
            return MaxDay;
        }

        public string MaxHour(Day tempDay)
        {
            int MaxCount = 0;
            DateTime MaxTime = new DateTime(0, 0, 0);
            DateTime TempTime = new DateTime(0, 0, 0);
            for (int i = 0; i < 24; i++, TempTime += TimeSpan.FromHours(1))
            {
                int count = 0;
                for (int n = 0; n < A.Length; n++)
                {
                    if((tempDay.ToString()==A[n].Day)&&(TempTime>=A[n].Time && A[n].Time<=(TempTime+ TimeSpan.FromHours(1))))
                    {
                        count++;
                    }
                }
                if(MaxCount<count)
                {
                    MaxCount = count;
                    MaxTime = TempTime;
                }
            }
            return MaxTime.ToString();
        }
    }
    





    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path(to log file):");
            string path = @"";
            path+=Console.ReadLine();
        }
    }
}
