﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int k = 30;
            int r = Convert.ToInt32(Math.Ceiling(Math.Log(k, 2))) + 1;
            int n = k + r;

            byte[] Xk_Byte = new byte[k];
            for(int i = 0; i < k; i++)
            {
                Xk_Byte[i] = (byte)(rand.Next(0, 2));
            }
            Console.WriteLine();
            for (int j = 0; j < k; j++)
            {
                Console.Write(Xk_Byte[j] + " ");
            }

            byte[] Xr_Byte = new byte[r];
            byte[] Xr_Byte2 = new byte[r];
            byte[] E_Byte = new byte[r];
            byte[,] HemmingsMatrix = new byte[n, r];
            byte[,] HT = new byte[r, n];

            byte[] Xr_ByteExt = new byte[r+1];
            byte[] Xr_ByteExt2 = new byte[r+1];
            byte[] E_ByteExt = new byte[r+1];
            byte[,] HemmingsMatrixExtended = new byte[n + 1, r + 1];
            byte[,] HTE = new byte[r + 1, n + 1];


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
                for (int j = 0; j < r; j++)
                {
                    //Console.Write(HemmingsMatrix[i, j] + " ");
                }

                //Console.WriteLine();
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

            // Расширенная матрица Хемминга
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    HemmingsMatrixExtended[i, j] = HemmingsMatrix[i, j];
                }
            }
            HemmingsMatrixExtended[n, r] = 1;

            for (int i = 0; i < n + 1; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < r + 1; j++)
                {
                    Console.Write(HemmingsMatrixExtended[i, j] + " ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < k; i++)
            {
                int result = 0;
                for (int j = 0; j < r; j++)
                {
                    result += (HemmingsMatrix[i, j]);
                    Console.WriteLine(result + " += " + HemmingsMatrix[i, j]);
                }
                if ((result % 2) == 0)
                    HemmingsMatrixExtended[i, r] = 1;
                if ((result % 2) == 1)
                    HemmingsMatrixExtended[i, r] = 0;
            }

            //разворачивание и вывод матрицы

            for (int i = 0; i < n+1; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < r+1; j++)
                {
                    Console.Write(HemmingsMatrixExtended[i, j] + " ");
                }
            }
            Console.WriteLine();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    HT[j, i] = HemmingsMatrix[i, j];
                }
            }
            for (int i = 0; i < r; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < n; j++)
                {
                    Console.Write(HT[i, j] + " ");
                }
            }
            Console.WriteLine();

            for (int i = 0; i < n +1; i++)
            {
                for (int j = 0; j < r +1; j++)
                {
                    HTE[j, i] = HemmingsMatrixExtended[i, j];
                }
            }
            for (int i = 0; i < r+1; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < n+1; j++)
                {
                    Console.Write(HTE[i, j] + " ");
                }
            }
            Console.WriteLine();
            //вычисляем избыточные символы
            for (int i = 0, XrCounter = 0; i < r; i++, XrCounter++)
            {
                int result = 0;
                for (int j = 0; j < k; j++)
                {
                    result += (HemmingsMatrix[j, i] * Xk_Byte[j]);
                    //Console.WriteLine(HemmingsMatrix[j, i] + " * " + Xk_Byte[j] + " = " + result);
                }
                if ((result % 2) == 0)
                    Xr_Byte[XrCounter] = 0;
                if ((result % 2) == 1)
                    Xr_Byte[XrCounter] = 1;
            }
            Console.WriteLine();
            for (int j = 0; j < k; j++)
            {
                Console.Write(Xk_Byte[j] + " ");
            }
            for (int j = 0; j < r; j++)
            {
                Console.Write(Xr_Byte[j] + " ");
            }
            Console.WriteLine();

            //формируем ошибки ---------------------------------------------------------------------------------------------------------------------------
            for (int i = 0, countOfMistakes = 1; i < countOfMistakes; i++)
            {
                //int place = rand.Next(0, k);
                if (Xk_Byte[1] == 1)
                    Xk_Byte[1] = 0;
                else
                    Xk_Byte[1] = 1;
            }
            //вычисляем избыточные символы 2
            for (int i = 0, XrCounter = 0; i < r; i++, XrCounter++)
            {
                int result = 0;
                for (int j = 0; j < k; j++)
                {
                    result += (HemmingsMatrix[j, i] * Xk_Byte[j]);
                    //Console.WriteLine(HemmingsMatrix[j, i] + " * " + Xk_Byte[j] + " = " + result);
                }
                if ((result % 2) == 0)
                    Xr_Byte2[XrCounter] = 0;
                if ((result % 2) == 1)
                    Xr_Byte2[XrCounter] = 1;
            }
            Console.WriteLine();
            for (int j = 0; j < k; j++)
            {
                Console.Write(Xk_Byte[j] + " ");
            }
            for (int j = 0; j < r; j++)
            {
                Console.Write(Xr_Byte2[j] + " ");
            }
            Console.WriteLine();


            //вычисляем синдром 
            Console.WriteLine("Синдром");
            for (int j = 0; j < r; j++)
            {
                E_Byte[j] = (byte)(Xr_Byte[j] ^ Xr_Byte2[j]);
                Console.Write(E_Byte[j] + " ");
            }
            Console.WriteLine();

            int RowWithMistake = -1;
            for (int row = 0, counter = 0; row < k; row++)
            {
                counter = 0;
                for (int col = 0; col < r; col++)
                {
                    if(E_Byte[col] == HemmingsMatrix[row, col])
                        counter++;
                    if (counter == r)
                    {
                        RowWithMistake = row;
                        break;
                    }
                }
            }
            Console.WriteLine("ошибка в бите №"+RowWithMistake);
            Console.WriteLine();






            //вычисляем избыточные символы
            for (int i = 0, XrCounter = 0; i < r+1; i++, XrCounter++)
            {
                int result = 0;
                for (int j = 0; j < k; j++)
                {
                    result += (HemmingsMatrixExtended[j, i] * Xk_Byte[j]);
                    //Console.WriteLine(HemmingsMatrix[j, i] + " * " + Xk_Byte[j] + " = " + result);
                }
                if ((result % 2) == 0)
                    Xr_ByteExt[XrCounter] = 0;
                if ((result % 2) == 1)
                    Xr_ByteExt[XrCounter] = 1;
            }
            Console.WriteLine();
            for (int j = 0; j < k; j++)
            {
                Console.Write(Xk_Byte[j] + " ");
            }
            for (int j = 0; j < r+1; j++)
            {
                Console.Write(Xr_ByteExt[j] + " ");
            }
            Console.WriteLine();

            //формируем ошибки ---------------------------------------------------------------------------------------------------------------------------
            for (int i = 0, countOfMistakes = 1; i < countOfMistakes; i++)
            {
                //int place = rand.Next(0, k);
                if (Xk_Byte[1] == 1)
                    Xk_Byte[1] = 0;
                else
                    Xk_Byte[1] = 1;
            }
            //вычисляем избыточные символы 2
            for (int i = 0, XrCounter = 0; i < r+1; i++, XrCounter++)
            {
                int result = 0;
                for (int j = 0; j < k; j++)
                {
                    result += (HemmingsMatrixExtended[j, i] * Xk_Byte[j]);
                    //Console.WriteLine(HemmingsMatrix[j, i] + " * " + Xk_Byte[j] + " = " + result);
                }
                if ((result % 2) == 0)
                    Xr_ByteExt2[XrCounter] = 0;
                if ((result % 2) == 1)
                    Xr_ByteExt2[XrCounter] = 1;
            }
            Console.WriteLine();
            for (int j = 0; j < k; j++)
            {
                Console.Write(Xk_Byte[j] + " ");
            }
            for (int j = 0; j < r+1; j++)
            {
                Console.Write(Xr_ByteExt2[j] + " ");
            }
            Console.WriteLine();


            //вычисляем синдром 
            Console.WriteLine("Синдром");
            for (int j = 0; j < r+1; j++)
            {
                E_ByteExt[j] = (byte)(Xr_ByteExt[j] ^ Xr_ByteExt2[j]);
                Console.Write(E_ByteExt[j] + " ");
            }
            Console.WriteLine();

            RowWithMistake = -1;
            for (int row = 0, counter = 0; row < k; row++)
            {
                counter = 0;
                for (int col = 0; col < r+1; col++)
                {
                    if (E_ByteExt[col] == HemmingsMatrixExtended[row, col])
                        counter++;
                    if (counter == r)
                    {
                        RowWithMistake = row;
                        break;
                    }
                }
            }
            Console.WriteLine("ошибка в бите №" + RowWithMistake);
            Console.WriteLine();










            Console.ReadLine();
        }
    }
}
