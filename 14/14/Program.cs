using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14
{
    class Program
    {
        public static void ShowMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static string Spiral(string message)
        {
            int n = (int)Math.Pow(message.Length, 0.5);
            char[,] arr = new char[n, n];
            int count = n;
            int value = -n;
            int sum = -1;
            int k = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i, j] = message[i * n + j];
                }
            }
            message = "";

            do
            {
                value = -1 * value / n;
                for (int i = 0; i < count; i++, k++)
                {
                    sum += value;
                    message += arr[sum / n, sum % n];
                }
                value *= n;
                count--;
                for (int i = 0; i < count; i++, k++)
                {
                    sum += value;
                    message += arr[sum / n, sum % n];
                }
            } while (count > 0);

            return message;
        }

        public static string BackSpiral(string message)
        {
            int n = (int)Math.Pow(message.Length, 0.5);
            char[,] arr = new char[n, n];
            int count = n;
            int value = -n;
            int sum = -1;
            int k = 0;

            do
            {
                value = -1 * value / n;
                for (int i = 0; i < count; i++, k++)
                {
                    sum += value;
                    arr[sum / n, sum % n] = message[k];
                }
                value *= n;
                count--;
                for (int i = 0; i < count; i++, k++)
                {
                    sum += value;
                    arr[sum / n, sum % n] = message[k];
                }
            } while (count > 0);

            message = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    message += arr[i, j];
                }
            }

            return message;
        }

        public static string MultiplePermutation(string message)
        {
            List<char> name = "констаи".ToCharArray().ToList();
            List<char> surname = "марчук".ToCharArray().ToList();

            List<char> sortedName = name.OrderBy(x => x).ToList();
            List<char> sortedSurname = surname.OrderBy(x => x).ToList();

            char[,] multiplePermutation = new char[name.Count, surname.Count];

            for (int i = 0; i < multiplePermutation.GetLength(0); i++)
            {
                for (int j = 0; j < multiplePermutation.GetLength(1); j++)
                {
                    multiplePermutation[sortedName.IndexOf(name[i]),
                                        sortedSurname.IndexOf(surname[j])]
                                      = message[i * multiplePermutation.GetLength(1) + j];
                }
            }

            message = "";
            
            for (int i = 0; i < multiplePermutation.GetLength(0); i++)
            {
                for (int j = 0; j < multiplePermutation.GetLength(1); j++)
                {
                    message += multiplePermutation[i, j];
                }
            }
            return message;
        }

        public static string BackMultiplePermutation(string message)
        {
            List<char> name = "констаи".ToCharArray().ToList();
            List<char> surname = "марчук".ToCharArray().ToList();

            List<char> sortedName = name.OrderBy(x => x).ToList();
            List<char> sortedSurname = surname.OrderBy(x => x).ToList();

            char[,] multiplePermutation = new char[name.Count, surname.Count];

            for (int i = 0; i < multiplePermutation.GetLength(0); i++)
            {
                for (int j = 0; j < multiplePermutation.GetLength(1); j++)
                {
                    multiplePermutation[name.IndexOf(sortedName[i]),
                                        surname.IndexOf(sortedSurname[j])]
                                      = message[i * multiplePermutation.GetLength(1) + j];
                }
            }

            message = "";

            for (int i = 0; i < multiplePermutation.GetLength(0); i++)
            {
                for (int j = 0; j < multiplePermutation.GetLength(1); j++)
                {
                    message += multiplePermutation[i, j];
                }
            }

            return message;
        }

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

            /////////////////////////////////////////////////////////////////////
            ///1
            /////////////////////////////////////////////////////////////////////
            Console.WriteLine("Спиральная перестановка");

            //string msg = "123456789";
            //msg = Spiral(msg);
            //Console.WriteLine(msg);
            //msg = BackSpiral(msg);
            //Console.WriteLine(msg);

            int matrixLength = 5;
            int matrixSize = matrixLength * matrixLength;
            string message = "";
            char[,] forSpiral = new char[matrixLength, matrixLength];


            using (StreamReader sr = new StreamReader(@"OnlyRusText.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextEncodedBySpiral.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        message += line;
                        if(message.Length > matrixSize)
                        {
                            sw.WriteLine(Spiral(message.Substring(0, matrixSize)));
                            message = message.Substring(matrixSize, message.Length - matrixSize);
                        }
                    }
                    while (message.Length > 0)
                    {
                        if (message.Length < matrixSize)
                            message = message.PadRight(matrixSize);
                        sw.WriteLine(Spiral(message.Substring(0, matrixSize)));
                        message = message.Substring(matrixSize, message.Length - matrixSize);
                    }
                }
            }

            using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedBySpiral.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextDecodedBySpiral.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        message += line;
                        if (message.Length > matrixSize)
                        {
                            sw.WriteLine(BackSpiral(message.Substring(0, matrixSize)));
                            message = message.Substring(matrixSize, message.Length - matrixSize);
                        }
                    }
                    while (message.Length > 0)
                    {
                        if (message.Length < matrixSize)
                            message = message.PadRight(matrixSize);
                        sw.WriteLine(BackSpiral(message.Substring(0, matrixSize)));
                        message = message.Substring(matrixSize, message.Length - matrixSize);
                    }
                }
            }

            /////////////////////////////////////////////////////////////////////
            ///2
            /////////////////////////////////////////////////////////////////////
            //string msg = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя123456789";
            //msg = MultiplePermutation(msg);
            //Console.WriteLine();
            //msg = BackMultiplePermutation(msg);
            //Console.WriteLine();


            int namesLength = 42;

            using (StreamReader sr = new StreamReader(@"OnlyRusText.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextEncodedByMultiple.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        message += line;
                        sw.WriteLine(MultiplePermutation(message.Substring(0, namesLength)));
                        message = message.Substring(namesLength, message.Length - namesLength);
                    }
                    while (message.Length > 0)
                    {
                        if (message.Length < namesLength)
                            message = message.PadRight(namesLength);
                        sw.WriteLine(MultiplePermutation(message.Substring(0, namesLength)));
                        message = message.Substring(namesLength, message.Length - namesLength);
                    }
                }
            }

            using (StreamReader sr = new StreamReader(@"OnlyRusTextEncodedByMultiple.txt", Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(@"OnlyRusTextDecodedByMultiple.txt", false, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        message += line;
                        sw.WriteLine(BackMultiplePermutation(message.Substring(0, namesLength)));
                        message = message.Substring(namesLength, message.Length - namesLength);
                    }
                    while (message.Length > 0)
                    {
                        if (message.Length < namesLength)
                            message = message.PadRight(namesLength);
                        sw.WriteLine(BackMultiplePermutation(message.Substring(0, namesLength)));
                        message = message.Substring(namesLength, message.Length - namesLength);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
