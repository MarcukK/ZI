using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    partial class Program
    {
        static void ShowWord(byte[] word)
        {
            foreach (var item in word)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static void ShowMatrix(byte[,] matrix)
        {
            for (int i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void ShowListOfWords(List<byte[]> listOfWords)
        {
            foreach (var word in listOfWords)
            {
                foreach (var letter in word)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
