using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2LabCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            List<char> Chars = new List<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            List<int> Counters = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            List<double> Probabilities = new List<double>();
            List<char> CharsBits = new List<char>() { '0', '1' };
            List<int> CountersBits = new List<int>() { n, n };
            List<double> ProbabilitiesBits = new List<double>();
            Console.WriteLine(Chars.Count);
            Console.WriteLine(Counters.Count);
            for (int i = 0; i < Chars.Count; i++) {
                using (StreamReader sr = new StreamReader(@"Mein_Kampf_German.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Counters[i] += line.ToLower().Count(x => x == Chars[i]);
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
            double Entropy = -Probabilities.Sum(x => x * (Math.Log(x, 2))); // вычисляет правильно
            Console.WriteLine("Энтропия немецкого алфавита = " + Entropy);

            //ASCII
            Encoding ascii = Encoding.ASCII;

            for (int i = 0; i < CharsBits.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"Mein_Kampf_German.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Byte[] encodedBytes = ascii.GetBytes(line.ToLower());
                        foreach (byte bit in encodedBytes)
                        {
                            line = Convert.ToString(bit, 2);
                            CountersBits[i] += line.Count(x => x == CharsBits[i]);
                        }
                    }
                }
            }
            string FIO = "Марчук";
            Byte[] encodedBytes = ascii.GetBytes(FIO.ToLower());

                double sumCharsBits = CountersBits.Sum(x => x);
            Console.WriteLine(sumCharsBits + " символов");
            for (int i = 0; i < CharsBits.Count; i++)
            {
                Console.Write(CharsBits[i]); Console.Write(" - "); Console.Write(CountersBits[i]);
                ProbabilitiesBits.Add((((double)CountersBits[i]) / sumCharsBits));
                Console.WriteLine(" Вероятность появления = " + ProbabilitiesBits[i]);

            }
            Console.WriteLine("Сумма вероятностей " + (ProbabilitiesBits.Sum(x => x)));
            double EntropyBits = -ProbabilitiesBits.Sum(x => x * (Math.Log(x, 2))); // вычисляет правильно
            Console.WriteLine("Энтропия бинарного кода = " + EntropyBits);

            string FIO = "Marchuk Konstantin Sergeevich";
            Console.WriteLine("Количество информации по немецкому алфавиту = " + (FIO.Length * Entropy) + " бит");
            Console.WriteLine("Количество информации по бинарному алфавиту = " + (FIO.Length * 8 * EntropyBits) + " бит");

            double[] Mistakes = { 0.1, 0.5, 1.0 };
            for(int i = 0; i < 3; i++) {
                double EntropyWithMist = 1 - (-(Mistakes[i] * Math.Log(Mistakes[i], 2)) - ((1 - Mistakes[i]) * Math.Log(1 - Mistakes[i], 2)));
                Console.WriteLine("Энтропия с ошибкой "+ Mistakes[i] + " = " + EntropyWithMist);
                Console.WriteLine("Количество информации по бинарному алфавиту с ошибкой " + Mistakes[i] + " = " + EntropyWithMist*8*FIO.Length + " бит");
            }

            Console.ReadLine();
        }
    }
}