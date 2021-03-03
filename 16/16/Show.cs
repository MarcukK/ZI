using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16
{
    partial class Program
    {
        private static void ShowBitArray(BitArray array)
        {
            foreach (bool item in array)
                Console.Write(item ? 1 : 0);
            Console.WriteLine();
        }
        private static void ShowListBitArray(List<BitArray> list)
        {
            foreach (BitArray array in list)
            {
                foreach (bool item in array)
                    Console.Write(item ? 1 : 0);
                Console.WriteLine();
            }
        }

        private static void ShowIntArray(int[,] array)
        {
            for (int i = 0; i < array.GetLongLength(0); i++)
            {
                for (int j = 0; j < array.GetLongLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        private static void ShowIntList(List<int> array)
        {
            foreach (int item in array)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        private static void ShowIntListAsTable(List<int> array, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array[i * cols + j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
