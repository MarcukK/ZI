using System;
using System.Linq;

namespace _5
{
    class Program
    {
        static Random rand = new Random();
        public static void ShowWord(byte[] word)
        {
            foreach (var literal in word)
            {
                Console.Write(literal + " ");
            }
            Console.WriteLine();
        }

        public static byte[,] CreateMatrix(int k1, int k2)
        {
            return new byte[k1, k2];
        }

        public static byte[,,] CreateMatrix(int k1, int k2, int z)
        {
            return new byte[k1, k2, z];
        }

        public static byte[,] FillMatrix(byte[,] matrix, byte[] word)
        {
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    matrix[i, j] = (byte)word[i * matrix.GetLongLength(1) + j];
                }
            }
            return matrix;
        }

        public static byte[,,] FillMatrix(byte[,,] matrix, byte[] word)
        {
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    for (int g = 0; g < matrix.GetLongLength(2); g++)
                    {
                        matrix[i, j, g] = (byte)word[i * matrix.GetLongLength(1) + j * matrix.GetLongLength(2) + g];
                    }
                }
            }
            return matrix;
        }

        public static void ShowMatrix(byte[,] matrix)
        {
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ") ;
                }
                Console.WriteLine();
            }
        }

        public static byte[] GetFirstParity(byte[,] matrix)
        {
            byte[] firstParity = new byte[matrix.GetLongLength(0)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                sum = (byte)0;
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    sum += (byte)(matrix[i,j]);
                }
                firstParity[i] = (byte)(sum % 2);
            }
            return firstParity;
        }

        public static byte[] GetSecondParity(byte[,] matrix)
        {
            byte[] secondParity = new byte[matrix.GetLongLength(1)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(1); i++)
            {
                sum = (byte)0;
                for (int j = 0; j < matrix.GetLongLength(0); j++)
                {
                    sum += (byte)(matrix[j, i]);
                }
                secondParity[i] = (byte)(sum % 2);
            }
            return secondParity;
        }

        public static byte[] GetFirstDiagParity(byte[,] matrix)
        {
            byte[] firstDiagParity = new byte[matrix.GetLongLength(0)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                sum = (byte)0;
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    Console.Write(matrix[(i + j) % matrix.GetLongLength(0), j % matrix.GetLongLength(1)] + " ");
                    sum += (byte)(matrix[(i + j) % matrix.GetLongLength(0), j % matrix.GetLongLength(1)]);
                }
                Console.WriteLine(sum);
                firstDiagParity[i] = (byte)(sum % 2);
            }
            return firstDiagParity;
        }

        public static byte[] GetSecondDiagParity(byte[,] matrix)
        {
            byte[] firstDiagParity = new byte[matrix.GetLongLength(0)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                sum = (byte)0;
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    Console.Write(matrix[(matrix.GetLongLength(0) - 1) - ((i + j) % matrix.GetLongLength(0)), 
                                            j % matrix.GetLongLength(1)] + " ");
                    sum += (byte)(matrix[(matrix.GetLongLength(0) - 1) - ((i + j) % matrix.GetLongLength(0)), 
                                            j % matrix.GetLongLength(1)]);
                }
                Console.WriteLine(sum);
                firstDiagParity[i] = (byte)(sum % 2);
            }
            return firstDiagParity;
        }

        public static byte[] CreateFullWord(byte[] word, byte[] firstParity, byte[] secondParity, byte[] firstDiagParity)
        {
            return ((word.Concat(firstParity).ToArray()).Concat(secondParity).ToArray()).Concat(firstDiagParity).ToArray();
        }

        public static byte[] CreateMistake(byte[] fullWord, int k, int countMistakes)
        {
            for (int i = 0; i < countMistakes; i++)
            {
                int placeOfMistake = rand.Next(0, k);
                Console.WriteLine("Ошибка задана в бите №" + placeOfMistake);
                fullWord[placeOfMistake] = (byte)((fullWord[placeOfMistake] + 1) % 2);
            }
            return fullWord;
        }

        public static byte[] GetSyndrom(byte[] paritiesWithMistakes, byte[] newParities)
        {
            byte[] syndrom = new byte[paritiesWithMistakes.Length];
            for (int i = 0; i < paritiesWithMistakes.Length; i++)
            {
                syndrom[i] = (byte)((paritiesWithMistakes[i] + newParities[i]) % 2);
            }
            return syndrom;
        }


        public static void FindMistakesWithTwoParities(int k1, int k2, byte[] paritiesWithMistakes,  byte[] newParities)
        {
            //сравним паритеты
            Console.WriteLine("Сравним паритеты");
            ShowWord(paritiesWithMistakes);
            ShowWord(newParities);


            //получим синдромы
            Console.WriteLine("Получим синдром");
            byte[] syndrom = GetSyndrom(paritiesWithMistakes, newParities);
            ShowWord(syndrom);

            //распарсим синдромы паритетов
            Console.WriteLine("Распарсим синдром паритетов");
            byte[] firstParitySyndrom = new byte[k1];
            Array.Copy(syndrom, 0, firstParitySyndrom, 0, k1);
            byte[] secondParitySyndrom = new byte[k2];
            Array.Copy(syndrom, k1, secondParitySyndrom, 0, k2);
            ShowWord(firstParitySyndrom);
            ShowWord(secondParitySyndrom);

            //ИСПРАВИМ ОШИБКИ
            for (int i = 0; i < firstParitySyndrom.Length; i++)
            {
                if (firstParitySyndrom[i] == (byte)1)
                {
                    for (int j = 0; j < secondParitySyndrom.Length; j++)
                    {
                        if (secondParitySyndrom[j] == (byte)1)
                        {
                            Console.WriteLine("Ошибка в бите №" + ( (i * secondParitySyndrom.Length) + (j+1) - 1) );
                        }
                    }
                }
            }
        }

        public static void FindMistakesWithThreeParities(int k1, int k2, int k3, byte[] paritiesWithMistakes, byte[] newParities)
        {
            //сравним паритеты
            Console.WriteLine("Сравним паритеты");
            ShowWord(paritiesWithMistakes);
            ShowWord(newParities);

            //получим синдромы
            Console.WriteLine("Получим синдром");
            byte[] syndrom = GetSyndrom(paritiesWithMistakes, newParities);
            ShowWord(syndrom);

            //распарсим синдромы паритетов
            Console.WriteLine("Распарсим синдром паритетов");
            byte[] firstParitySyndrom = new byte[k1];
            Array.Copy(syndrom, 0, firstParitySyndrom, 0, k1);
            byte[] secondParitySyndrom = new byte[k2];
            Array.Copy(syndrom, k1, secondParitySyndrom, 0, k2);
            byte[] firstDiagParitySyndrom = new byte[k3];
            Array.Copy(syndrom, k1 + k2, firstDiagParitySyndrom, 0, k3);
            ShowWord(firstParitySyndrom);
            ShowWord(secondParitySyndrom);
            ShowWord(firstDiagParitySyndrom);

            //ИСПРАВИМ ОШИБКИ
            for (int i = 0; i < firstParitySyndrom.Length; i++)
            {
                if (firstParitySyndrom[i] == (byte)1)
                {
                    for (int j = 0; j < secondParitySyndrom.Length; j++)
                    {
                        if (secondParitySyndrom[j] == (byte)1)
                        {
                            for (int g = 0; g < firstDiagParitySyndrom.Length; g++)
                            {
                                if (firstDiagParitySyndrom[g] == (byte)1)
                                {
                                    for (int newI = g, newJ = 0; newJ < secondParitySyndrom.Length; newI++, newJ++)
                                    {
                                        if ((i == newI % firstParitySyndrom.Length) && (j == newJ))
                                            Console.WriteLine("Ошибка в бите №" + ((i * secondParitySyndrom.Length) + (j + 1) - 1));
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }



        public static byte[,] GetFirstParity(byte[,,] matrix)
        {
            byte[,] firstParity = new byte[matrix.GetLongLength(0), matrix.GetLongLength(1)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    sum = (byte)0;
                    for (int g = 0; g < matrix.GetLongLength(2); g++)
                    {
                        sum += (byte)(matrix[i, j, g]);
                    }
                    firstParity[i, j] = (byte)(sum % 2);
                }
            }
            return firstParity;
        }

        public static byte[,] GetSecondParity(byte[,,] matrix)
        {
            byte[,] secondParity = new byte[matrix.GetLongLength(1), matrix.GetLongLength(2)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(1); i++)
            {
                sum = (byte)0;
                for (int j = 0; j < matrix.GetLongLength(2); j++)
                {
                    for (int g = 0; g < matrix.GetLongLength(0); g++)
                    {
                        sum += (byte)(matrix[j, g, i]);
                    }
                    secondParity[i, j] = (byte)(sum % 2);
                }
            }
            return secondParity;
        }

        public static byte[,] GetThirdParity(byte[,,] matrix)
        {
            byte[,] secondParity = new byte[matrix.GetLongLength(2), matrix.GetLongLength(0)];
            byte sum;
            for (int i = 0; i < matrix.GetLongLength(2); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(0); j++)
                {
                    sum = (byte)0;
                    for (int g = 0; g < matrix.GetLongLength(1); g++)
                    {
                        sum += (byte)(matrix[g, i, j]);
                    }
                    secondParity[i, j] = (byte)(sum % 2);
                }
            }
            return secondParity;
        }

        public static byte[] ActionsForTwoDimensialMatrix(byte[] baseWord, byte[,] baseMatrix)
        {
            //заполняем матрицу словом
            Console.WriteLine("Заполняем матрицу словом");
            byte[,] filledMatrix = FillMatrix(baseMatrix, baseWord);
            ShowMatrix(filledMatrix);


            //получаем горизонтальный (1) паритет
            Console.WriteLine("Получаем горизонтальный (1) паритет");
            byte[] firstParity = GetFirstParity(filledMatrix);
            ShowWord(firstParity);


            //получаем вертикальный (2) паритет
            Console.WriteLine("Получаем вертикальный (2) паритет");
            byte[] secondParity = GetSecondParity(filledMatrix);
            ShowWord(secondParity);


            //получаем диагональный (слева направо) паритет
            Console.WriteLine("Получаем диагональный (слева направо) паритет");
            byte[] firstDiagParity = GetFirstDiagParity(filledMatrix);
            ShowWord(firstDiagParity);


            //получаем диагональный (справа налево) паритет
            Console.WriteLine("Получаем диагональный (справа налево) паритет");
            byte[] secondDiagParity = GetSecondDiagParity(filledMatrix);
            ShowWord(secondDiagParity);


            //выводим всё в 1 строку
            Console.WriteLine("Выводим всё в 1 строку");
            byte[] fullWord = CreateFullWord(baseWord, firstParity, secondParity, firstDiagParity);
            ShowWord(fullWord);


            return fullWord;
        }

        static void Main()
        {

            //создаём слово
            Console.WriteLine("Создаём слово");
            int k = 20;
            byte[] baseWord = new byte[k];
            for (int i = 0; i < k; i++)
            {
                baseWord[i] = (byte)(rand.Next(0, 2));
            }
            ShowWord(baseWord);


            //создаём матрицу
            Console.WriteLine("Создаём матрицу");
            int k1 = 4;
            int k2 = 5;
            byte[,] baseMatrix = CreateMatrix(k1,k2);


            byte[] fullWord = ActionsForTwoDimensialMatrix(baseWord, baseMatrix);


            //задаём ошибки
            Console.WriteLine();
            Console.WriteLine("Задаём ошибки");
            int countMistakes = 2;
            byte[] fullWordWithMistakes = CreateMistake(fullWord, k, countMistakes);
            ShowWord(fullWordWithMistakes);
            Console.WriteLine();

            //повторяем операции для слова с ошибкой
            Console.WriteLine("Повторяем операции для слова с ошибкой");
            byte[] wordWithMistakes = new byte[k]; 
            Array.Copy(fullWordWithMistakes, wordWithMistakes, k);
            byte[] newfullWord = ActionsForTwoDimensialMatrix(wordWithMistakes, baseMatrix);


            //ищем ошибки 2 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] twoParitiesWithMistakes = new byte[k1 + k2];
            Array.Copy(fullWordWithMistakes, k, twoParitiesWithMistakes, 0, k1 + k2);
            byte[] newTwoParities = new byte[k1 + k2];
            Array.Copy(newfullWord, k, newTwoParities, 0, k1 + k2);

            FindMistakesWithTwoParities(k1, k2, twoParitiesWithMistakes, newTwoParities);


            //ищем ошибки 3 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] threeParitiesWithMistakes = new byte[k1 + k2 + k1];
            Array.Copy(fullWordWithMistakes, k, threeParitiesWithMistakes, 0, k1 + k2 + k1);
            byte[] newThreeParities = new byte[k1 + k2 + k1];
            Array.Copy(newfullWord, k, newThreeParities, 0, k1 + k2 + k1);

            FindMistakesWithThreeParities(k1, k2, k1, threeParitiesWithMistakes, newThreeParities);



            Console.ReadLine();
        }
    }
}
