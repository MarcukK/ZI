using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            int xi = 497523685;
            string xs = "NDk3NTIzNjg1";

            int n = 0;
            
            //List<char> Chars = new List<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            //List<int> Counters = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            //List<double> Probabilities = new List<double>();

            List<char> Chars = new List<char>() { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            List<int> Counters = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            List<int> CountersBase64 = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };

            List<double> Probabilities = new List<double>();
            List<double> ProbabilitiesBase64 = new List<double>();

            List<char> CharsBits = new List<char>() { '0', '1' };
            List<int> CountersBits = new List<int>() { n, n };

            List<double> ProbabilitiesBits = new List<double>();

            Console.WriteLine(Chars.Count);
            Console.WriteLine(Counters.Count);
            Console.WriteLine(CountersBase64.Count);
            Console.WriteLine();

            for (int i = 0; i < Chars.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"sat.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Counters[i] += line.Count(x => x == Chars[i]);
                    }
                }
            }
            double sumChars = Counters.Sum(x => x);
            Console.WriteLine(sumChars + " символов");
            for (int i = 0; i < Chars.Count; i++)
            {
                Console.Write(Chars[i]); Console.Write(" - "); Console.Write(Counters[i]);
                Probabilities.Add((((double)Counters[i]) / sumChars));
                Console.WriteLine(" Вероятность появления = " + Probabilities[i]);
            }
            Console.WriteLine("Сумма вероятностей " + (Probabilities.Sum(x => x)));
            double Entropy = -Probabilities.Sum(x => x != 0 ? x * (Math.Log(x, 2)) : 0); // вычисляет правильно
            double EntropyHart = Math.Log(Chars.Count, 2);
            Console.WriteLine("Энтропия по Шеннону латиницы = " + Entropy);
            Console.WriteLine("Энтропия по Хартли латиницы = " + EntropyHart);
            Console.WriteLine("Избыточность латинского алфавита = " + ((EntropyHart - Entropy) / EntropyHart) * 100 + "%");
            Console.WriteLine();

            for (int i = 0; i < Chars.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"satBase64.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        CountersBase64[i] += line.Count(x => x == Chars[i]);
                    }
                }
            }
            double sumCharsBase64 = CountersBase64.Sum(x => x);
            Console.WriteLine(sumCharsBase64 + " символов");
            for (int i = 0; i < Chars.Count; i++)
            {
                Console.Write(Chars[i]); Console.Write(" - "); Console.Write(CountersBase64[i]);
                ProbabilitiesBase64.Add((((double)CountersBase64[i]) / sumCharsBase64));
                Console.WriteLine(" Вероятность появления = " + ProbabilitiesBase64[i]);
            }
            Console.WriteLine("Сумма вероятностей Base64 " + (ProbabilitiesBase64.Sum(x => x)));
            double EntropyBase64 = -ProbabilitiesBase64.Sum(x => x !=0 ? x * (Math.Log(x, 2)) : 0); // вычисляет правильно
            double EntropyHartBase64 = Math.Log(Chars.Count, 2);
            //double EntropyBase64 = -ProbabilitiesBase64.Sum(x => x * (Math.Log(x, 2))); // вычисляет правильно
            Console.WriteLine("Энтропия по Шеннону Base64 = " + EntropyBase64);
            Console.WriteLine("Энтропия по Хартли Base64 = " + EntropyHartBase64);
            Console.WriteLine("Избыточность алфавита Base64 = " + ((EntropyHartBase64 - EntropyBase64) / EntropyHartBase64) * 100 + "%");
            Console.WriteLine();

            List<string> FirstName = new List<string> { "Konstantin", "S29uc3RhbnRpbg==" };
            List<string> LastName = new List<string> { "Marchuk000", "TWFyY2h1azAwMA==" };
            Encoding ascii = Encoding.ASCII;
            for(int j = 0; j < FirstName.Count; j++)
            {
                Byte[] FirstBytes = ascii.GetBytes(FirstName[j]);
                Byte[] LastBytes = ascii.GetBytes(LastName[j]);
                Byte[] NewBytes = new Byte[FirstBytes.Count()];
                Byte[] NewBytes2 = new Byte[FirstBytes.Count()];
                string FirstNameBytes = "";
                string LastNameBytes = "";
                string EncodedName = "";
                string EncodedName2 = "";
                for (int i = 0; i < FirstBytes.Count(); i++)
                {
                    NewBytes[i] += Convert.ToByte(FirstBytes[i] ^ LastBytes[i]);
                    NewBytes2[i] += Convert.ToByte(FirstBytes[i] ^ LastBytes[i] ^ LastBytes[i]);
                    FirstNameBytes += (FirstBytes[i] + " ");
                    LastNameBytes += (LastBytes[i] + " ");
                    EncodedName += (NewBytes[i] + " ");
                    EncodedName2 += (NewBytes2[i] + " ");
                }
                string EncodedFirstName = System.Text.Encoding.ASCII.GetString(NewBytes);
                string EncodedFirstName2 = System.Text.Encoding.ASCII.GetString(NewBytes2);
                Console.WriteLine("First name bytes     - " + FirstNameBytes);
                Console.WriteLine("Last name bytes      - " + LastNameBytes);

                Console.WriteLine("Encoded name bytes   - " + EncodedName);
                Console.WriteLine("Decoded name bytes   - " + EncodedName2);

                Console.WriteLine("Encoded first name is " + EncodedFirstName);
                Console.WriteLine("Decoded first name is " + EncodedFirstName2);
                Console.WriteLine();
            }

            //string FirstNameBase64;
            //string LastNameBase64;

            //byte[] FirstNameData = System.Text.ASCIIEncoding.ASCII.GetBytes(FirstName);
            //byte[] LastNameData = System.Text.ASCIIEncoding.ASCII.GetBytes(LastName);

            //for (int i = 0; i < FirstBytes.Count(); i++)
            //{
            //    NewBytes[i] += Convert.ToByte(FirstNameData[i] ^ LastNameData[i]);
            //    NewBytes2[i] += Convert.ToByte(FirstNameData[i] ^ LastNameData[i] ^ LastNameData[i]);
            //    FirstNameBytes += (FirstNameData[i] + " ");
            //    LastNameBytes += (LastNameData[i] + " ");
            //    EncodedName += (NewBytes[i] + " ");
            //    EncodedName2 += (NewBytes2[i] + " ");
            //}

            //FirstNameBase64 = System.Convert.ToBase64String(FirstNameData);
            //LastNameBase64 = System.Convert.ToBase64String(LastNameData);



            Console.ReadLine();

        }
    }
}
