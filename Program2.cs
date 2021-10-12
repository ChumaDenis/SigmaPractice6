using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Polynomial
    {
        public double x { set; get; }
        public double[] c;
        

        public Polynomial()
        {

        }
        public Polynomial(int n)
        {
            c = new double[n];
        }

        public void Parse(string s)
        {
            string[] buf = s.Split(' ', '*');
            c = new double[buf.Length -2];
            //неявне приведення до double
            c[0] = float.Parse(buf[0]);
            for (int i=1, n=1; i<buf.Length; n++)
            {
                c[n] = Convert.ToDouble(buf[i]);
                i += 2;
                Console.WriteLine(c[n]);
            }
        }


        public void Output()
        {
            for (int i = 0; i < c.Length; i++)
            {
                if (i == 0)
                    Console.Write(String.Format("{0} ", c[i]));
                else
                {
                    if (c[i]>0)
                        Console.Write(String.Format("+{0}*x^{1} ", c[i], i));
                    else
                        Console.Write(String.Format("{0}*x^{1} ", c[i], i));
                }
            }
        }
        public void Checked()
        {
            double Sum = 0;
            for(int i=0; i<c.Length; i++)
            {
                if (c[i]!=0 && c[i]*Math.Pow(x, i)!=0)
                {
                    c[i] = c[c.Length-2]* Math.Pow(x, c.Length-3);
                }
                else if(c[i] != 0 && c[i] * Math.Pow(x, i) == 0)
                {
                    
                    Array.Resize(ref c, c.Length+1);
                    c[c.Length] = c[c.Length - 2] * Math.Pow(x, c.Length - 3);
                }
                else if(c[i] == 0 && c[i] * Math.Pow(x, i) != 0)
                {
                   
                }
                else if(c[i] == 0 && c[i] * Math.Pow(x, i) == 0)
                {

                }
            }
        }

        public static Polynomial operator +(Polynomial a)
        {
            Polynomial Temp = new Polynomial(c.Length>a.c.Length? c.Length: a.c.Length);

            for(int i=0; i< (c.Length < a.c.Length ? c.Length : a.c.Length); i++)
            {
               Temp.c[i]=c[i]+a.c[i];
            }
            double[] r = c.Length > a.c.Length ? c : a.c;
            for(int i= (c.Length < a.c.Length ? c.Length : a.c.Length)-1; i<r.Length; i++)
            {
                Temp.c[i] = r[i];
            }
            return Temp;
        }
        public static Polynomial operator -(Polynomial a)
        {
            Polynomial Temp = new Polynomial(c.Length >= a.c.Length ? c.Length : a.c.Length);

            for (int i = 0; i < (c.Length < a.c.Length ? c.Length : a.c.Length); i++)
            {
                Temp.c[i] = c[i] - a.c[i];
            }
            double[] r = c.Length > a.c.Length ? c : a.c;
            for (int i = (c.Length < a.c.Length ? c.Length : a.c.Length) - 1; i < r.Length; i++)
            {
                if (c.Length > a.c.Length)
                    Temp.c[i] = r[i];
                else if (c.Length < a.c.Length)
                    Temp.c[i] = -r[i];
            }
            return Temp;
        }
        public static Polynomial operator *(Polynomial a)
        {
            Polynomial Temp = new Polynomial(c.Length+a.c.Length);
            for (int i = 0; i <= c.Length; i++)
            {
                for (int j = 0; j <= a.c.Length; j++)
                {
                    Temp.c[i + j] += c[i] * a.c[j];
                }
            }

            return Temp;
        }



    }

    class Program
    {
        static void Main(string[] args)
        {
            string a = "-12 +3*x^1 -5*x^2";
            Polynomial b = new Polynomial();
            b.Parse(a);
            b.Output();
        }
    }
}
