using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            List<char> Chars = new List<char>() { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ',', '.', ' ' };
            List<int> Counters = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            List<double> Probabilities = new List<double>();

            Console.WriteLine(Chars.Count);
            Console.WriteLine(Counters.Count);

            //оставляем только русские символы и пунктуацию
            for (int i = 0; i < Chars.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"text.txt", Encoding.UTF8))
                {
                    using (StreamWriter sw = new StreamWriter(@"OnlyRusText.txt", false, Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            foreach (var item in line.ToLower())
                            {
                                if (Chars.Contains(item))
                                    sw.Write(item);
                            }
                            sw.WriteLine();
                        }
                    }
                }
            }

            for (int i = 0; i < Chars.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"OnlyRusText.txt", Encoding.UTF8))
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

            ////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////ПОРТ//////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////
            List<char> CharsEncoded = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            List<int> CountersEncoded = new List<int>() { n, n, n, n, n, n, n, n, n, n };
            List<double> ProbabilitiesEncoded = new List<double>();

            string[,] Port = new string[Chars.Count, Chars.Count];
            for (int i = 0; i < Port.GetLength(0); i++)
            {
                for (int j = 0; j < Port.GetLength(1); j++)
                {
                    Port[i,j] = (i * Chars.Count + j).ToString().PadLeft(4, '0');
                }
            }

            //кодируем в новый файл
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (StreamReader sr = new StreamReader(@"OnlyRusText.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextEncodedByPort.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length % 2 == 1)
                            line += ".";
                        for (int i = 0; i < line.Length; i += 2)
                        {
                            sw.Write(Port[Chars.IndexOf(line[i]), Chars.IndexOf(line[i+1])]);
                        }
                        sw.WriteLine();
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Затраченное на зашифрование Портом время = " + stopwatch.ElapsedMilliseconds);

            //декодируем в новый файл
            stopwatch.Reset();
            stopwatch.Start();
            using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedByPort.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextDecodedByPort.txt", false, Encoding.UTF8))
                {
                    string line;
                    string symbol;
                    int symbolIndex1;
                    int symbolIndex2;
                    string linetoWrite;

                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i += 4)
                        {
                            symbol = line[i].ToString() 
                                   + line[i + 1].ToString()
                                   + line[i + 2].ToString()
                                   + line[i + 3].ToString();
                            symbolIndex1 = Convert.ToInt32(symbol) / Chars.Count;
                            symbolIndex2 = Convert.ToInt32(symbol) % Chars.Count;
                            linetoWrite = Chars[symbolIndex1].ToString() + Chars[symbolIndex2].ToString();
                            sw.Write(linetoWrite);
                        }
                        sw.WriteLine();
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Затраченное на расшифрование Портом время = " + stopwatch.ElapsedMilliseconds);


            //подсчитываем статистику по закодированному документу
            for (int i = 0; i < CharsEncoded.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedByPort.txt", Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        CountersEncoded[i] += line.ToLower().Count(x => x == CharsEncoded[i]);
                    }
                }
            }


            double sumCharsEncoded = CountersEncoded.Sum(x => x);
            Console.WriteLine(sumCharsEncoded + " символов");
            for (int i = 0; i < CharsEncoded.Count; i++)
            {
                Console.Write(CharsEncoded[i]); Console.Write(" - "); Console.Write(CountersEncoded[i]);
                ProbabilitiesEncoded.Add((((double)CountersEncoded[i]) / sumCharsEncoded));
                Console.WriteLine(" Вероятность появления = " + ProbabilitiesEncoded[i]);

            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////ЦЕЗАРЬ////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////
            List<char> CharsCaesar = new List<char>() { 'м', 'а', 'р', 'ч', 'у', 'к', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'л', 'н', 'о', 'п', 'с', 'т', 'ф', 'х', 'ц', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ',', '.', ' ' };
            List<int> CountersCaesar = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            List<double> ProbabilitiesCaesar = new List<double>();

            //кодируем в новый файл
            stopwatch.Start();
            using (StreamReader sr = new StreamReader(@"OnlyRusText.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextEncodedByCaesar.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            sw.Write(CharsCaesar[Chars.IndexOf(line[i])]);
                        }
                        sw.WriteLine();
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Затраченное на зашифрование Цезарем время = " + stopwatch.ElapsedMilliseconds);

            //декодируем в новый файл
            stopwatch.Reset();
            stopwatch.Start();
            using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedByCaesar.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextDecodedByCaesar.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            sw.Write(Chars[CharsCaesar.IndexOf(line[i])]);
                        }
                        sw.WriteLine();
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Затраченное на расшифрование Цезарем время = " + stopwatch.ElapsedMilliseconds);


            //подсчитываем статистику по закодированному документу
            for (int i = 0; i < CharsCaesar.Count; i++)
            {
                using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedByCaesar.txt", Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        CountersCaesar[i] += line.ToLower().Count(x => x == CharsCaesar[i]);
                    }
                }
            }


            int sumCharsCaesar = CountersCaesar.Sum(x => x);
            Console.WriteLine(sumCharsCaesar + " символов");
            for (int i = 0; i < CharsCaesar.Count; i++)
            {
                Console.Write(CharsCaesar[i]); Console.Write(" - "); Console.Write(CountersCaesar[i]);
                ProbabilitiesCaesar.Add((((double)CountersCaesar[i]) / sumCharsCaesar));
                Console.WriteLine(" Вероятность появления = " + ProbabilitiesCaesar[i]);

            }

            Console.ReadLine();

        }
    }
}
