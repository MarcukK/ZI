using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    partial class Program
    {
        public static byte[] CreateWord(int n)
        {
            byte[] word = new byte[n];
            for (int i = 0; i < n; i++)
                word[i] = (byte)(rand.Next(0, 2));
            return word;
        }

        static List<byte[]> CreateListOfBytes(byte[] word, int eachWordLength)
        {
            List<byte[]> listOfWords = new List<byte[]>();
            for (int i = 0; i < word.Length;)
            {
                listOfWords.Add(new byte[eachWordLength]);
                for (int j = 0; j < eachWordLength; j++, ++i)
                {
                    if (i < word.Length)
                    {
                        listOfWords[(i / eachWordLength)][j] = word[i];
                    }
                }
            }
            return listOfWords;
        }



        static byte[,] CreateHemmingsMatrix(int k, int r, int n)
        {
            byte[,] HemmingsMatrix = new byte[n, r];
            byte[,] HT = new byte[r, n];


            string BitStr;
            int digit = 1;
            for (int i = 0; i < k; i++, digit++)
            {
                BitStr = Convert.ToString(digit, 2);

                for (int kek = 0; kek < BitStr.Length; kek++)
                {
                    if (BitStr[kek] == 48)
                        HemmingsMatrix[i, BitStr.Length - kek] = 0;
                    if (BitStr[kek] == 49)
                        HemmingsMatrix[i, BitStr.Length - kek] = 1;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    if (i == j + k)
                        HemmingsMatrix[i, j] = 1;
                }
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    HemmingsMatrix[i, 0] = 1;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    HT[j, i] = HemmingsMatrix[i, j];
                }
            }

            return HT;
        }

        static byte[] CreateHemmingsWord(byte[] baseWord)
        {
            int k = baseWord.Length;
            int r = Convert.ToInt32(Math.Ceiling(Math.Log(k, 2))) + 1;
            int n = k + r;

            byte[] hemmingsWord = new byte[n];
            Array.Copy(baseWord, 0, hemmingsWord, 0, k);

            byte[,] hemmingsMatrix = CreateHemmingsMatrix(k, r, n);

            for (int i = 0, XrCounter = 0; i < r; i++, XrCounter++)
            {
                int result = 0;
                for (int j = 0; j < k; j++)
                {
                    result += (hemmingsMatrix[i, j] * baseWord[j]);
                }
                if ((result % 2) == 0)
                    hemmingsWord[k + XrCounter] = (byte)0;
                if ((result % 2) == 1)
                    hemmingsWord[k + XrCounter] = (byte)1;
            }

            return hemmingsWord;
        }

        static List<byte[]> CreateHemmingsListOfWords(List<byte[]> baseListOfWords)
        {
            List<byte[]> listOfHemmingsWords = new List<byte[]>();

            for (int i = 0; i < baseListOfWords.Count; i++)
            {
                listOfHemmingsWords.Add(
                    CreateHemmingsWord(baseListOfWords[i])
                    );
            }

            return listOfHemmingsWords;
        }
    }
}
