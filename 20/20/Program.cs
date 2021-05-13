using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _20
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Marchuk Kanstantsin";
            Console.WriteLine(name);
            foreach (var item in Encoding.UTF8.GetBytes(name))
            {
                Console.Write(item.ToString("X"));
            }
            Console.WriteLine();

            SHA512 shaM = new SHA512Managed();
            name = Encoding.UTF8.GetString(shaM.ComputeHash(Encoding.UTF8.GetBytes(name)));
            Console.WriteLine(name);
            foreach (var item in Encoding.UTF8.GetBytes(name))
            {
                Console.Write(item.ToString("X"));
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
