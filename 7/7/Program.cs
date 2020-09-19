using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    partial class Program
    {
        static Random rand = new Random();

        static List<byte[]> GetErrors(List<byte[]> listOfWords)
        {
            byte[] fullMessage = new byte[listOfWords.Count * listOfWords[0].Length];
            for (int i = 0; i < listOfWords.Count; i++)
            {
                for (int j = 0; j < listOfWords[0].Length; j++)
                {
                    fullMessage[i * (listOfWords[0].Length) + j] = listOfWords[i][j];
                }
            }


            List<byte[]> newListOfWords = new List<byte[]>();
            for (int i = 0; i < fullMessage.Length/7; i++)
            {
                newListOfWords.Add(new byte[7]);
                for (int j = 0; j < 7 && (i * 7 + j) < fullMessage.Length; j++)
                {
                    newListOfWords[i][j] = fullMessage[i + j * 7];
                }
            }
            ShowListOfWords(newListOfWords);


            int countOfErrors = 3;
            int errorStart = rand.Next(0, fullMessage.Length - (int)(Math.Ceiling(fullMessage.Length*0.1)));
            Console.WriteLine(countOfErrors + " ошибок, начиная с бита №" + errorStart);
            for (int i = 0; i < countOfErrors; i++, errorStart++)
            {
                if(newListOfWords[errorStart / 7][errorStart % 7] == (byte)0)
                    newListOfWords[errorStart / 7][errorStart % 7] = (byte)1;
                else
                    newListOfWords[errorStart / 7][errorStart % 7] = (byte)0;
            }
            ShowListOfWords(newListOfWords);

            for (int i = 0; i < newListOfWords.Count; i++)
            {
                for (int j = 0; j < newListOfWords[0].Length; j++)
                {
                    fullMessage[i + j * 7] = (byte)newListOfWords[i][j]; 
                }
            }


            for (int i = 0; i < listOfWords.Count; i++)
            {
                for (int j = 0; j < listOfWords[0].Length; j++)
                {
                    listOfWords[i][j] = (byte)fullMessage[i * (listOfWords[0].Length) + j];
                }
            }

            return listOfWords;
        }

        public static List<byte[]> ChangeErrors(List<byte[]> errorsListOfWords, List<byte[]> newErrorsListOfWords, int eachWordLength)
        {
            List<byte[]> syndrom = new List<byte[]>();
            for (int i = 0; i < errorsListOfWords.Count; i++)
            {
                syndrom.Add(new byte[eachWordLength]);
                for (int j = eachWordLength; j < errorsListOfWords[0].Length; j++)
                {
                    syndrom[i][j - eachWordLength] = (byte)((newErrorsListOfWords[i][j] + errorsListOfWords[i][j]) % 2);
                }
                int k = eachWordLength;
                int r = Convert.ToInt32(Math.Ceiling(Math.Log(k, 2))) + 1;
                int n = k + r;
                byte[,] hemmingsMatrix = CreateHemmingsMatrix(k, r, n);

                int RowWithMistake = -1;
                for (int row = 0, counter = 0; row < k; row++)
                {
                    counter = 0;
                    for (int col = 0; col < r; col++)
                    {
                        if (syndrom[i][row] == hemmingsMatrix[col, row])
                            counter++;
                        if (counter == r)
                        {
                            RowWithMistake = row;
                            if (newErrorsListOfWords[i][RowWithMistake] == (byte)1)
                                newErrorsListOfWords[i][RowWithMistake] = (byte)0;
                            else
                                newErrorsListOfWords[i][RowWithMistake] = (byte)1;
                            break;
                        }
                    }

                }


            }
            return newErrorsListOfWords;
        }

        static void Main(string[] args)
        {
            int wordLength = 13 * 8;
            byte[] baseWord = CreateWord(wordLength);
            ShowWord(baseWord);


            int eachWordLength = 6;
            List<byte[]> baseListOfWords = CreateListOfBytes(baseWord, eachWordLength);
            ShowListOfWords(baseListOfWords);

            List<byte[]> newListOfWords = CreateHemmingsListOfWords(baseListOfWords);
            ShowListOfWords(newListOfWords);

            List<byte[]> errorsListOfWords = GetErrors(newListOfWords);
            ShowListOfWords(errorsListOfWords);

            List<byte[]> errorsListOfWordsWithoutHemmingsBytes = new List<byte[]>();

            for (int i = 0; i < errorsListOfWords.Count; i++)
            {
                errorsListOfWordsWithoutHemmingsBytes.Add(new byte[eachWordLength]);
                for (int j = 0; j < eachWordLength; j++)
                {
                    errorsListOfWordsWithoutHemmingsBytes[i][j] = errorsListOfWords[i][j];
                }
            }
            List<byte[]> newErrorsListOfWords = CreateHemmingsListOfWords(errorsListOfWordsWithoutHemmingsBytes);
            List<byte[]> listOfWordsWithoutErrors = ChangeErrors(errorsListOfWords, newErrorsListOfWords, eachWordLength);

            List<byte[]> list1 = new List<byte[]>();
            List<byte[]> list2 = new List<byte[]>();
            for (int i = 0; i < errorsListOfWords.Count; i++)
            {
                list1.Add(new byte[eachWordLength]);
                list2.Add(new byte[eachWordLength]);
                for (int j = 0; j < eachWordLength; j++)
                {
                    list1[i][j] = newErrorsListOfWords[i][j];
                    list2[i][j] = listOfWordsWithoutErrors[i][j];
                }
            }
            Console.WriteLine("listOfWordsWithoutErrors");
            ShowListOfWords(list1);
            Console.WriteLine("ListOfWordsOld");
            ShowListOfWords(list2);

            

            Console.ReadLine();

        }
    }
}
