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
        private static List<int> keysTable = new List<int>{
                                14, 17, 11, 24, 1,  5,  3,  28, 15, 6,  21, 10, 23, 19, 12, 4,
                                26, 8,  16, 7,  27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40,
                                51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
        private static List<int> numberOfMoves = new List<int> { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        private static List<int> InitialPermutationList = new List<int>() {
                58, 50, 42, 34, 26, 18, 10, 2,  60, 52, 44, 36, 28, 20, 12, 4,
                62, 54, 46, 38, 30, 22, 14, 6,  64, 56, 48, 40, 32, 24, 16, 8,
                57, 49, 41, 33, 25, 17, 9,  1,  59, 51, 43, 35, 27, 19, 11, 3,
                61, 53, 45, 37, 29, 21, 13, 5,  63, 55, 47, 39, 31, 23, 15, 7
        };
        private static List<int> InitialFullKeyPermutationList = new List<int>() {
                57, 49, 41, 33, 25, 17, 9,  1,  58, 50, 42, 34, 26, 18,
                10, 2,  59, 51, 43, 35, 27, 19, 11, 3,  60, 52, 44, 36,
                63, 55, 47, 39, 31, 23, 15, 7,  62, 54, 46, 38, 30, 22,
                14, 6,  61, 53, 45, 37, 29, 21, 13, 5,  28, 20, 12, 4
        };
        private static List<int> FinalPermutationList = new List<int>(){
                40, 8,  48, 16, 56, 24, 64, 32, 39, 7,  47, 15, 55, 23, 63, 31,
                38, 6,  46, 14, 54, 22, 62, 30, 37, 5,  45, 13, 53, 21, 61, 29,
                36, 4,  44, 12, 52, 20, 60, 28, 35, 3,  43, 11, 51, 19, 59, 27,
                34, 2,  42, 10, 50, 18, 58, 26, 33, 1,  41, 9,  49, 17, 57, 25
        };
        private static List<int> PPermutationList = new List<int>()
                             {  16, 7,  20, 21, 29, 12, 28, 17,
                                1,  15, 23, 26, 5,  18, 31, 10,
                                2,  8,  24, 14, 32, 27, 3,  9,
                                19, 13, 30, 6,  22, 11, 4,  25};
        private static List<List<int>> SBlocks = new List<List<int>> {
                                    new List<int>(){
                                        14, 4,  13, 1,  2,  15, 11, 8,  3,  10, 6,  12, 5,  9,  0,  7,
                                        0,  15, 7,  4,  14, 2,  13, 1,  10, 6,  12, 11, 9,  5,  3,  8,
                                        4,  1,  14, 8,  13, 6,  2,  11, 15, 12, 9,  7,  3,  10, 5,  0,
                                        15, 12, 8,  2,  4,  9,  1,  7,  5,  11, 3,  14, 10, 0,  6,  13
                                    },
                                    new List<int>(){
                                        15, 1,  8,  14, 6,  11, 3,  4,  9,  7,  2,  13, 12, 0,  5,  10,
                                        3,  13, 4,  7,  15, 2,  8,  14, 12, 0,  1,  10, 6,  9,  11, 5,
                                        0,  14, 7,  11, 10, 4,  13, 1,  5,  8,  12, 6,  9,  3,  2,  15,
                                        13, 8,  10, 1,  3,  15, 4,  2,  11, 6,  7,  12, 0,  5,  14, 9
                                    },
                                    new List<int>(){
                                        10, 0,  9,  14, 6,  3,  15, 5,  1,  13, 12, 7,  11, 4,  2,  8,
                                        13, 7,  0,  9,  3,  4,  6,  10, 2,  8,  5,  14, 12, 11, 15, 1,
                                        13, 6,  4,  9,  8,  15, 3,  0,  11, 1,  2,  12, 5,  10, 14, 7,
                                        1,  10, 13, 0,  6,  9,  8,  7,  4,  15, 14, 3,  11, 5,  2,  12
                                    },
                                    new List<int>(){
                                        7,  13, 14, 3,  0,  6,  9,  10, 1,  2,  8,  5,  11, 12, 4,  15,
                                        13, 8,  11, 5,  6,  15, 0,  3,  4,  7,  2,  12, 1,  10, 14, 9,
                                        10, 6,  9,  0,  12, 11, 7,  13, 15, 1,  3,  14, 5,  2,  8,  4,
                                        3,  15, 0,  6,  10, 1,  13, 8,  9,  4,  5,  11, 12, 7,  2,  14
                                    },
                                    new List<int>(){
                                        2,  12, 4,  1,  7,  10, 11, 6,  8,  5,  3,  15, 13, 0,  14, 9,
                                        14, 11, 2,  12, 4,  7,  13, 1,  5,  0,  15, 10, 3,  9,  8,  6,
                                        4,  2,  1,  11, 10, 13, 7,  8,  15, 9,  12, 5,  6,  3,  0,  14,
                                        11, 8,  12, 7,  1,  14, 2,  13, 6,  15, 0,  9,  10, 4,  5,  3
                                    },
                                    new List<int>(){
                                        12, 1,  10, 15, 9,  2,  6,  8,  0,  13, 3,  4,  14, 7,  5,  11,
                                        10, 15, 4,  2,  7,  12, 9,  5,  6,  1,  13, 14, 0,  11, 3,  8,
                                        9,  14, 15, 5,  2,  8,  12, 3,  7,  0,  4,  10, 1,  13, 11, 6,
                                        4,  3,  2,  12, 9,  5,  15, 10, 11, 14, 1,  7,  6,  0,  8,  13
                                    },
                                    new List<int>(){
                                        4,  11, 2,  14, 15, 0,  8,  13, 3,  12, 9,  7,  5,  10, 6,  1,
                                        13, 0,  11, 7,  4,  9,  1,  10, 14, 3,  5,  12, 2,  15, 8,  6,
                                        1,  4,  11, 13, 12, 3,  7,  14, 10, 15, 6,  8,  0,  5,  9,  2,
                                        6,  11, 13, 8,  1,  4,  10, 7,  9,  5,  0,  15, 14, 2,  3,  12
                                    },
                                    new List<int>(){
                                        13, 2,  8,  4,  6,  15, 11, 1,  10, 9,  3,  14, 5,  0,  12, 7,
                                        1,  15, 13, 8,  10, 3,  7,  4,  12, 5,  6,  11, 0,  14, 9,  2,
                                        7,  11, 4,  1,  9,  12, 14, 2,  0,  6,  10, 13, 15, 3,  5,  8,
                                        2,  1,  14, 7,  4,  10, 8,  13, 15, 12, 9,  0,  3,  5,  6,  11
                                    } };

        private static string messageString = "1010101011001100111100001111111110101010110011001111000011111110";
        //private static string initialKey = "00000001000000110000011100001111000111110011111101111111";
        private static BitArray fullKey;
        private static List<BitArray> shortKeys;

        private static void ShowInitialData()
        {
            Console.WriteLine("\nMessage");
            Console.WriteLine(messageString);
            Console.WriteLine("\nKeys Table");
            ShowIntListAsTable(keysTable, 3, 16);
            Console.WriteLine("\nNumber of Moves");
            ShowIntList(numberOfMoves);
            Console.WriteLine("\nInitial Permutation List");
            ShowIntListAsTable(InitialPermutationList, 4, InitialPermutationList.Count / 4);
            Console.WriteLine("\nInitial Full Key Permutation List");
            ShowIntListAsTable(InitialFullKeyPermutationList, 4 , InitialFullKeyPermutationList.Count / 4);
            Console.WriteLine("\nFinal Permutation List");
            ShowIntListAsTable(FinalPermutationList, 4, FinalPermutationList.Count / 4);
            Console.WriteLine("\nP Permutation List");
            ShowIntListAsTable(PPermutationList, 4, PPermutationList.Count / 4);
        }

        private static BitArray GetMessage()
        {
            BitArray message = new BitArray(64);
            for (int i = 0; i < messageString.Length; i++)
            {
                if (messageString[i] == '0')
                    message.Set(i, false);
                else
                    message.Set(i, true);
            }
            //Console.WriteLine("\nMessage");
            //ShowBitArray(message);
            return message;
        }

        private static void FillFullKey(string initialKey)
        {
            Console.WriteLine("\nInitial Key");
            Console.WriteLine(initialKey);
            //from string to BitArray
            BitArray fullKeyInMethod = new BitArray(64);
            for (int i = 0; i < 8; i += 1)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (initialKey[i * 7 + j] == '1')
                        fullKeyInMethod[i * 8 + j] = true;
                    else
                        fullKeyInMethod[i * 8 + j] = false;

                }
            }
            Console.WriteLine("\nFull key");
            ShowBitArray(fullKeyInMethod);
            //add 8th bits
            bool bit = false;
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 != 7)
                    bit = bit ^ fullKeyInMethod[i];
                else
                    bit = false;

            }
            Console.WriteLine("\nFull key with bits");
            ShowBitArray(fullKeyInMethod);
            fullKey = fullKeyInMethod;
        }

        //private static void FillFullKey()
        //{
        //    //Console.WriteLine("\nInitial Key");
        //    //Console.WriteLine(initialKey);
        //    //from string to BitArray
        //    BitArray fullKeyInMethod = new BitArray(64);
        //    for (int i = 0; i < 8; i += 1)
        //    {
        //        for (int j = 0; j < 7; j++)
        //        {
        //            if (initialKey[i * 7 + j] == '1')
        //                fullKeyInMethod[i * 8 + j] = true;
        //            else
        //                fullKeyInMethod[i * 8 + j] = false;

        //        }
        //    }
        //    //add 8th bits
        //    bool bit = false;
        //    for (int i = 0; i < 64; i++)
        //    {
        //        if (i % 8 != 7)
        //            bit = bit ^ fullKeyInMethod[i];
        //        else
        //            bit = false;

        //    }
        //    //Console.WriteLine("\nFull key");
        //    //ShowBitArray(fullKeyInMethod);
        //    fullKey = fullKeyInMethod;
        //}

        private static void FillShortKeys()
        {
            List<BitArray> shortKeysInMethod = new List<BitArray>();
            BitArray shortKey = new BitArray(48);

            List<bool> blockC = new List<bool>();
            for (int i = 0; i < 32; i++)
            {
                blockC.Add(fullKey[i]);
                if (i % 8 == 7) i++;
            }

            List<bool> blockD = new List<bool>();
            for (int i = 32; i < 64; i++)
            {
                blockD.Add(fullKey[i]);
                if (i % 8 == 7) i++;
            }

            List<bool> blockCD;
            for (int i = 0; i < 16; i++)
            {
                blockCD = new List<bool>();
                blockC = MoveListLeft(blockC, numberOfMoves[i]);
                blockD = MoveListLeft(blockD, numberOfMoves[i]);
                blockCD.AddRange(blockC);
                blockCD.AddRange(blockD);

                for (int j = 0; j < 48; j++)
                {
                    shortKey.Set(j, blockCD[keysTable[j]]);
                }
                shortKeysInMethod.Add(shortKey);
                shortKey = new BitArray(48);
            }
            //Console.WriteLine("\nShort keys");
            //ShowListBitArray(shortKeysInMethod);
            shortKeys = shortKeysInMethod;
        }

        //private static List<int> CreateInitialPermutationList()
        //{
        //    List<int> InitialPermutation = new List<int>(64);
        //    for (int i = 0; i < 64; i++)
        //    {
        //        InitialPermutation.Add(0);
        //    }
        //    int counter = 1;
        //    for (int i = 7; i >= 0; i--)
        //    {
        //        InitialPermutation[2 * 16 + i] = counter; counter++;
        //        InitialPermutation[0 * 16 + i] = counter; counter++;
        //        InitialPermutation[2 * 16 + (i + 8)] = counter; counter++;
        //        InitialPermutation[0 * 16 + (i + 8)] = counter; counter++;

        //        InitialPermutation[3 * 16 + i] = counter; counter++;
        //        InitialPermutation[1 * 16 + i] = counter; counter++;
        //        InitialPermutation[3 * 16 + (i + 8)] = counter; counter++;
        //        InitialPermutation[1 * 16 + (i + 8)] = counter; counter++;
        //    }
        //    //Console.WriteLine("\nInitial Permutation as Table");
        //    //ShowIntListAsTable(InitialPermutation, 4, 16);
        //    return InitialPermutation;
        //}

        private static List<int> CreateFinalPermutationList()
        {
            List<int> FinalPermutation = new List<int>(64);
            int i = 0;
            for (i = 0; i < 64; i++)
            {
                FinalPermutation.Add(0);
            }
            int counter = 2;
            i = 0;
            for (; i < 16; i += 2)
            {
                if (i == 8)
                    counter = 1;
                for (int j = 3; j >= 0; j--)
                {
                    FinalPermutation[j * 16 + i + 1] = counter;
                    FinalPermutation[j * 16 + i] = counter + 32;
                    counter += 2;
                }
            }
            //Console.WriteLine("\nFinal Permutation as Table");
            //ShowIntListAsTable(FinalPermutation, 4, 16);
            return FinalPermutation;
        }

        private static List<int> CreateInitialFullKeyPermutationList()
        {
            List<int> initialFullKeyPermutation = new List<int>();
            int i = 0;
            for (i = 0; i < 56; i++)
            {
                initialFullKeyPermutation.Add(0);
            }
            int counter = 56;
            for (i = 0; i < initialFullKeyPermutation.Count / 2; i++, counter = (counter - 8))
            {
                if (counter < 0)
                    counter += 65;
                initialFullKeyPermutation[i] = counter;
            }
            counter = 62;
            for (i = 0; i < initialFullKeyPermutation.Count / 2; i++, counter = (counter - 8))
            {
                if (counter < 0)
                    counter += 63;
                initialFullKeyPermutation[i + initialFullKeyPermutation.Count / 2] = counter;
            }
            counter = 27;
            for (i = 4; i > 0; i--, counter = (counter - 8))
            {
                initialFullKeyPermutation[initialFullKeyPermutation.Count - i] = counter;
            }
            //Console.WriteLine("\nInitial Full Key Permutation as Table");
            //ShowIntListAsTable(initialFullKeyPermutation, 4, initialFullKeyPermutation.Count / 4);
            return initialFullKeyPermutation;
        }
    }
}
