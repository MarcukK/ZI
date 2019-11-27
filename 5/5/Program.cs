using System;
using System.Linq;
using System.Text;

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
                    Console.Write(matrix[i, j] + " ");
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
                    sum += (byte)(matrix[i, j]);
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
            return ((word.Concat(firstParity).ToArray())
                .Concat(secondParity).ToArray())
                .Concat(firstDiagParity).ToArray();
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


        public static void FindMistakesWithTwoParities(int k1, int k2, byte[] paritiesWithMistakes, byte[] newParities)
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
                            Console.WriteLine("Ошибка в бите №" + ((i * secondParitySyndrom.Length) + (j + 1) - 1));
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

            for (int j = 0; j < matrix.GetLongLength(1); j++)
            {
                sum = (byte)0;
                for (int g = 0; g < matrix.GetLongLength(2); g++)
                {
                    for (int i = 0; i < matrix.GetLongLength(0); i++)
                    {
                        sum += (byte)(matrix[i, j, g]);
                    }
                    secondParity[j, g] = (byte)(sum % 2);
                }
            }
            return secondParity;
        }

        public static byte[,] GetThirdParity(byte[,,] matrix)
        {
            byte[,] thirdParity = new byte[matrix.GetLongLength(2), matrix.GetLongLength(0)];
            byte sum;
            for (int g = 0; g < matrix.GetLongLength(2); g++)
            {
                for (int i = 0; i < matrix.GetLongLength(0); i++)
                {
                    sum = (byte)0;
                    for (int j = 0; j < matrix.GetLongLength(1); j++)
                    {
                        sum += (byte)(matrix[i, j, g]);
                    }
                    thirdParity[g, i] = (byte)(sum % 2);
                }
            }
            return thirdParity;
        }


        public static byte[,] GetFirstDiagParity(byte[,,] matrix)
        {
            byte[,] firstDiagParity = new byte[matrix.GetLongLength(0), matrix.GetLongLength(1)];
            byte sum;
            for (int g = 0; g < matrix.GetLongLength(2); g++)
            {
                for (int i = 0; i < matrix.GetLongLength(0); i++)
                {
                    sum = (byte)0;
                    for (int j = 0; j < matrix.GetLongLength(1); j++)
                    {
                        Console.Write(matrix[(i + j) % matrix.GetLongLength(0), j % matrix.GetLongLength(1), g] + " ");
                        sum += (byte)(matrix[(i + j) % matrix.GetLongLength(0), j % matrix.GetLongLength(1), g]);
                    }
                    Console.WriteLine(sum);
                    firstDiagParity[g, i] = (byte)(sum % 2);
                }
            }
            return firstDiagParity;
        }

        public static byte[,] GetSecondDiagParity(byte[,,] matrix)
        {
            byte[,] secondDiagParity = new byte[matrix.GetLongLength(0), matrix.GetLongLength(1)];
            byte sum;
            for (int g = 0; g < matrix.GetLongLength(2); g++)
            {
                for (int i = 0; i < matrix.GetLongLength(0); i++)
                {
                    sum = (byte)0;
                    for (int j = 0; j < matrix.GetLongLength(1); j++)
                    {
                        Console.Write(matrix[(matrix.GetLongLength(0) - 1) - ((i + j) % matrix.GetLongLength(0)),
                                                j % matrix.GetLongLength(1), g] + " ");
                        sum += (byte)(matrix[(matrix.GetLongLength(0) - 1) - ((i + j) % matrix.GetLongLength(0)),
                                                j % matrix.GetLongLength(1), g]);
                    }
                    Console.WriteLine(sum);
                    secondDiagParity[g, i] = (byte)(sum % 2);
                }
            }
            return secondDiagParity;
        }

        public static byte[] ConvertMatrixToString(byte[,] matrix)
        {
            byte[] str = new byte[matrix.GetLongLength(0) * matrix.GetLongLength(1)];
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    str[i * matrix.GetLongLength(1) + j] = matrix[i, j];
                }
            }
            return str;
        }
        public static byte[] CreateFullWord(byte[] word,
                                            byte[,] firstParityMatrix,
                                            byte[,] secondParityMatrix, 
                                            byte[,] thirdParityMatrix, 
                                            byte[,] firstDiagParityMatrix, 
                                            byte[,] secondDiagParityMatrix)
        {
            byte[] firstParity = new byte[firstParityMatrix.GetLongLength(0) * firstParityMatrix.GetLongLength(1)];
            firstParity = ConvertMatrixToString(firstParityMatrix);
            byte[] secondParity = new byte[secondParityMatrix.GetLongLength(0) * secondParityMatrix.GetLongLength(1)];
            secondParity = ConvertMatrixToString(secondParityMatrix);
            byte[] thirdParity = new byte[thirdParityMatrix.GetLongLength(0) * thirdParityMatrix.GetLongLength(1)];
            thirdParity = ConvertMatrixToString(thirdParityMatrix);
            byte[] firstDiagParity = new byte[firstDiagParityMatrix.GetLongLength(0) * firstDiagParityMatrix.GetLongLength(1)];
            firstDiagParity = ConvertMatrixToString(firstDiagParityMatrix);
            byte[] secondDiagParity = new byte[secondDiagParityMatrix.GetLongLength(0) * secondDiagParityMatrix.GetLongLength(1)];
            secondDiagParity = ConvertMatrixToString(secondDiagParityMatrix);

            return (((word.Concat(firstParity).ToArray())
                .Concat(secondParity).ToArray())
                .Concat(thirdParity).ToArray())
                .Concat(firstDiagParity).ToArray()
                .Concat(secondDiagParity).ToArray();
        }

        public static byte[] ActionsForThreeDimensialMatrix(byte[] baseWord, byte[,,] baseMatrix)
        {
            //заполняем матрицу словом
            Console.WriteLine("Заполняем матрицу словом");
            byte[,,] filledMatrix = FillMatrix(baseMatrix, baseWord);


            //получаем горизонтальный (1) паритет
            Console.WriteLine("Получаем горизонтальный (1) паритет");
            byte[,] firstParity = GetFirstParity(filledMatrix);
            ShowMatrix(firstParity);


            //получаем вертикальный (2) паритет
            Console.WriteLine("Получаем вертикальный (2) паритет");
            byte[,] secondParity = GetSecondParity(filledMatrix);
            ShowMatrix(secondParity);


            //получаем глубинный (3) паритет
            Console.WriteLine("Получаем глубинный (3) паритет");
            byte[,] thirdParity = GetThirdParity(filledMatrix);
            ShowMatrix(thirdParity);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //получаем диагональный (слева направо) паритет
            Console.WriteLine("Получаем диагональный (слева направо) паритет");
            byte[,] firstDiagParity = GetFirstDiagParity(filledMatrix);
            ShowMatrix(firstDiagParity);


            //получаем диагональный (справа налево) паритет
            Console.WriteLine("Получаем диагональный (справа налево) паритет");
            byte[,] secondDiagParity = GetSecondDiagParity(filledMatrix);
            ShowMatrix(secondDiagParity);


            //выводим всё в 1 строку
            Console.WriteLine("Выводим всё в 1 строку");
            byte[] fullWord = CreateFullWord(baseWord, firstParity, secondParity, thirdParity, firstDiagParity, secondDiagParity);
            ShowWord(fullWord);


            return fullWord;
        }


        public static void FindMistakesWithTwoParitiesThree(int k1, int k2, byte[] paritiesWithMistakes, byte[] newParities)
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
                            Console.WriteLine("Ошибка в бите №" + ((i * secondParitySyndrom.Length) + (j + 1) - 1));
                        }
                    }
                }
            }
        }

        public static void FindMistakesWithThreeParitiesThree(int k1, int k2, int k3, byte[] paritiesWithMistakes, byte[] newParities)
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
            byte[,] firstParitySyndrom = new byte[k1,k2];
            for (int i = 0; i < k1; i++)
            {
                for (int j = 0; j < k2; j++)
                {
                    firstParitySyndrom[i, j] = syndrom[i * k2 + j];
                }
            }
            byte[,] secondParitySyndrom = new byte[k2,k3];
            for (int i = 0; i < k2; i++)
            {
                for (int j = 0; j < k3; j++)
                {
                    secondParitySyndrom[i, j] = syndrom[(k1 * k2) + i * k3 + j];
                }
            }
            byte[,] thirdParitySyndrom = new byte[k3,k1];
            for (int i = 0; i < k3; i++)
            {
                for (int j = 0; j < k1; j++)
                {
                    thirdParitySyndrom[i, j] = syndrom[(k3 * k1) + (k1 * k2) + i * k1 + j];
                }
            }
            Console.WriteLine("Первый синдром");
            ShowMatrix(firstParitySyndrom);
            Console.WriteLine("Второй синдром");
            ShowMatrix(secondParitySyndrom);
            Console.WriteLine("Третий синдром");
            ShowMatrix(thirdParitySyndrom);

            //ИСПРАВИМ ОШИБКИ
            for (int i = 0; i < firstParitySyndrom.Length; i++)
            {
                if (firstParitySyndrom[i] == (byte)1)
                {
                    for (int j = 0; j < secondParitySyndrom.Length; j++)
                    {
                        if (secondParitySyndrom[j] == (byte)1)
                        {
                            for (int g = 0; g < thirdParitySyndrom.Length; g++)
                            {
                                if (thirdParitySyndrom[g] == (byte)1)
                                {
                                    Console.WriteLine("Ошибка в бите №" + ((i * secondParitySyndrom.Length) + (j + 1) - 1));
                                }
                            }
                        }
                    }
                }
            }

        }


        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;


            //создаём слово
            Console.WriteLine("Создаём слово");
            int k = 20;
            byte[] baseWord = new byte[k];
            for (int i = 0; i < k; i++)
            {
                baseWord[i] = (byte)(rand.Next(0, 2));
            }
            ShowWord(baseWord);


            //создаём матрицу 2-мерную
            Console.WriteLine("Создаём матрицу");
            int k1 = 2;
            int k2 = 10;
            byte[,] baseMatrix = CreateMatrix(k1, k2);


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


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("/////////////////////////////////");
            Console.WriteLine("////////////////ТРЁХМЕРНАЯ ЗАДАЧА");
            Console.WriteLine("/////////////////////////////////");

            //создаём матрицу 3-мерную
            Console.WriteLine("Создаём матрицу");
            k1 = 2;
            k2 = 5;
            int k3 = 2;
            byte[,,] baseMatrixTriple = CreateMatrix(k1, k2, k3);


            fullWord = ActionsForThreeDimensialMatrix(baseWord, baseMatrixTriple);


            //задаём ошибки
            Console.WriteLine();
            Console.WriteLine("Задаём ошибки");
            countMistakes = 3;
            byte[] fullWordWithMistakesThree = CreateMistake(fullWord, k, countMistakes);
            ShowWord(fullWordWithMistakesThree);
            Console.WriteLine();

            //повторяем операции для слова с ошибкой
            Console.WriteLine("Повторяем операции для слова с ошибкой");
            byte[] wordWithMistakesThree = new byte[k];
            Array.Copy(fullWordWithMistakesThree, wordWithMistakesThree, k);
            byte[] newfullWordThree = ActionsForThreeDimensialMatrix(wordWithMistakesThree, baseMatrixTriple);


            //ищем ошибки 2 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] twoParitiesWithMistakesThree = new byte[
                (k1 * k2) + (k2 * k3)];
            Array.Copy(fullWordWithMistakesThree, k, twoParitiesWithMistakesThree, 0,
                twoParitiesWithMistakesThree.Length);
            newTwoParities = new byte[
                (k1 * k2) + (k2 * k3)];
            Array.Copy(newfullWordThree, k, newTwoParities, 0, 
                newTwoParities.Length);

            FindMistakesWithTwoParitiesThree(k1, k2, twoParitiesWithMistakesThree, newTwoParities);


            //ищем ошибки 3 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] threeParitiesWithMistakesThree = new byte[
                (k1 * k2) + (k2 * k3) + (k1 * k3)];
            Array.Copy(fullWordWithMistakesThree, k, threeParitiesWithMistakesThree, 0,
                threeParitiesWithMistakesThree.Length);
            newThreeParities = new byte[
                (k1 * k2) + (k2 * k3) + (k1 * k3)];
            Array.Copy(newfullWordThree, k, newThreeParities, 0, 
                newThreeParities.Length);

            FindMistakesWithThreeParitiesThree(k1, k2, k3, threeParitiesWithMistakesThree, newThreeParities);


            //ищем ошибки 4 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] fourParitiesWithMistakes = new byte[
                (k1 * k2) + (k2 * k3) + (k1 * k3) + (k1 * k2)];
            Array.Copy(fullWordWithMistakesThree, k, fourParitiesWithMistakes, 0, 
                fourParitiesWithMistakes.Length);
            byte[] newFourParities = new byte[
                (k1 * k2) + (k2 * k3) + (k1 * k3) + (k1 * k2)];
            Array.Copy(newfullWordThree, k, newFourParities, 0, 
                newFourParities.Length);

            //FindMistakesWithFourParitiesThree(k1, k2, fourParitiesWithMistakes, newFourParities);


            //ищем ошибки 5 паритета
            Console.WriteLine();
            Console.WriteLine("Ищем ошибки");
            byte[] fiveParitiesWithMistakes = 
                new byte[(k1 * k2) + (k2 * k3) + (k1 * k3) + (k1 * k2) + (k2 * k3)];
            Array.Copy(fullWordWithMistakesThree, k, fiveParitiesWithMistakes, 0, 
                fiveParitiesWithMistakes.Length);
            byte[] newFiveParities = new byte[
                (k1 * k2) + (k2 * k3) + (k1 * k3) + (k1 * k2) + (k2 * k3)];
            Array.Copy(newfullWordThree, k, newFiveParities, 0, 
                newFiveParities.Length);

            //FindMistakesWithFiveParitiesThree(k1, k2, k3, k4, k5, fiveParitiesWithMistakes, newFiveParities);


            Console.ReadLine();
        }
    }
}
