using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        public static int[] StartPermutation = { 58 ,50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
                                                 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
                                                 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
                                                 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };

        public static int[] EndPermutation = {40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
                                              38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
                                              36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
                                              34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };

        public static int[] ExtendPermutation = {32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13,
                                                 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22,
                                                 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1};

        public static int[] KeyPermutation = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
                                               10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
                                               63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
                                               14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4};

        public static int[] GetKeyPermutation = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4,
                                                  26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40,
                                                  51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32};

        public static int[] ShiftKey = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public static int[,] ReplaceTable1 = { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                                {0, 15, 7, 4, 14, 2,  13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                                {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                                { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} };

        public static int[,] ReplaceTable2 = { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11,  5 },
                                                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                                { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9} };

        public static int[,] ReplaceTable3 = { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15,  1 },
                                                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                                { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} };

        public static int[,] ReplaceTable4 = { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                               {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                                               {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                               { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} };

        public static int[,] ReplaceTable5 = { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                               {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                                               {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                               { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3} };

        public static int[,] ReplaceTable6 = { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                               {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                                               {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                               { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13} };

        public static int[,] ReplaceTable7 = { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                               {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                                               {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                               { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12} };

        public static int[,] ReplaceTable8 = { { 13, 2,  8,  4, 6, 15, 11, 1, 10, 9, 3, 14, 5 , 0 , 12, 7 },
                                               {1, 15,  13,  8, 10, 3, 7, 4, 12, 5, 6 , 11, 0 , 14, 9, 2},
                                               {7, 11,  4,  1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                               { 2, 1,  14,  7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11  } };

        public static int[] ReplaceP = {16, 7, 20, 21, 29, 12, 28, 17, 1,  15, 23, 26, 5,  18, 31, 10, 2,
                                         8, 24, 14, 32, 27, 3,  9,19, 13, 30, 6,  22, 11, 4,  25};

        static void Main(string[] args)
        {
            string message = "Marchuk Kanstantsin Sergeevich";
            Console.WriteLine("\n Initial Message");
            Console.WriteLine(message);

            string[] key = { "YUIOPW}" ,"Saaejga","'sd%rq("};
            string[] decodekey = new string[key.Length];

            for (int i = 0; i < key.Length; i++)
            {
                decodekey[i] = ExtendKey(GetBitByByte(key[i]));
            }

            string code=message;

            for (int i = 0; i < key.Length; i++)
            {
                code = Encode(code, key[i], i % 2);
            }

            Console.WriteLine("\n Encoded message");
            Console.WriteLine(code);

            string newmessage = code;

            for (int i = 0; i < decodekey.Length; i++)
            {
                newmessage = Decode(newmessage, decodekey[decodekey.Length - 1 - i], (i + 1) % 2);
            }

            Console.WriteLine("\n Decoded message");
            Console.WriteLine(newmessage);

            Console.ReadLine();

        }

        public static string Encode(string message, string key,int orderkey)
        {
            string bitmessage = GetBitByByte(message);
            string bitkey = GetBitByByte(key);
            bitkey = ExtendKey(bitkey);
            bitkey = new string(KeyPermutationFunc(bitkey.ToCharArray()));

            if (bitmessage.Length % 64 != 0)
            {
                for (int i = bitmessage.Length % 64; i < 64; i++)
                {
                    bitmessage += "0";
                }
            }

            char[] result = new char[bitmessage.Length/8];
            int notfull = bitmessage.Length % 64 == 0 ? 0 : 1;

            for (int count = 0; count < bitmessage.Length/64 + notfull; count++)
            {
                string currentMessage = bitmessage.Substring(count * 64, 64);

                if (currentMessage.Length < 64)
                {
                    for (int i = currentMessage.Length-1; i < 64; i++)
                    {
                        currentMessage += "0";
                    }
                }

                char[] permutation = StartPermutationFunc(currentMessage.ToCharArray());

                char[] left = new string(permutation).Substring(0, 32).ToCharArray();
                char[] right = new string(permutation).Substring(32, 32).ToCharArray();


                string CKey = bitkey.Substring(0, 28);
                string DKey = bitkey.Substring(28, 28);

                for (int round = 0; round < 16; round++)
                {
                    char[] saveright = right;

                    if (orderkey == 1)
                    {
                        CKey = LeftShift(CKey, ShiftKey[round]);
                        DKey = LeftShift(DKey, ShiftKey[round]);
                    }

                    right = FBlock(right, new string(CKey + DKey));

                    char[] save = left;
                    left = saveright;
                    right = XOR(right, save);

                    if (orderkey == 0)
                    {
                        CKey = RightShift(CKey, ShiftKey[15 - round]);
                        DKey = RightShift(DKey, ShiftKey[15 - round]);
                    }
                }

                permutation = EndPermutationFunc((new string(left) + new string(right)).ToCharArray());

                currentMessage = new string(permutation);

                for (int i = 0; i < currentMessage.Length / 8; i++)
                {
                    result[count*8 + i] = Convert.ToChar(BitToInt(currentMessage.Substring(i * 8, 8).ToCharArray()));
                }
            }
            return new string(result);
        }

        public static string Decode(string code,string key,int orderkey)
        {
            string bitmessage = GetBitByByte(code);
            key = new string(KeyPermutationFunc(key.ToCharArray()));

            char[] result = new char[bitmessage.Length/8];

            for (int count = 0; count < bitmessage.Length/64; count++)
            {
                string currentMessage = bitmessage.Substring(count * 64, 64);

                char[] permutation = StartPermutationFunc(currentMessage.ToCharArray());

                char[] left = new string(permutation).Substring(0, 32).ToCharArray();
                char[] right = new string(permutation).Substring(32, 32).ToCharArray();

                string CKey = key.Substring(0, 28);
                string DKey = key.Substring(28, 28);

                for (int round = 0; round < 16; round++)
                {
                    char[] saveleft = left;

                    if (orderkey == 1)
                    {
                        CKey = LeftShift(CKey, ShiftKey[round]);
                        DKey = LeftShift(DKey, ShiftKey[round]);
                    }

                    left = FBlock(left, new string(CKey + DKey));

                    left = XOR(left, right);
                    right = saveleft;

                    if (orderkey == 0)
                    {
                        CKey = RightShift(CKey, ShiftKey[15 - round]);
                        DKey = RightShift(DKey, ShiftKey[15 - round]);
                    }
                }

                permutation = EndPermutationFunc((new string(left) + new string(right)).ToCharArray());

                currentMessage = new string(permutation);

                for (int i = 0; i < currentMessage.Length / 8; i++)
                {
                    result[count*8+i] = Convert.ToChar(BitToInt(currentMessage.Substring(i * 8, 8).ToCharArray()));
                }
            }
            return new string(result);
        }

        public static char[] FBlock(char[] block,string key)
        {
            string curentKey = new string(GetKeyPermutationFunc(key.ToCharArray()));

            char[] extendPermuntation = ExtendPermutationFunc(block);

            string[] Sblocks = new string[8];

            for (int i = 0; i < Sblocks.GetLength(0); i++)
            {
                Sblocks[i] = new string(XOR(new String(extendPermuntation).Substring(i * 6, 6).ToCharArray(),
                    curentKey.Substring(i * 6, 6).ToCharArray()));
            }

            for (int i = 0; i < Sblocks.Length; i++)
            {
                Sblocks[i] = new string(PermutationS(Sblocks[i].ToCharArray(), i + 1));
            }

            block = new char[32];
            for (int i = 0; i < block.Length; i++)
            {
                block[i] = Sblocks[i / 4].ToCharArray()[i % 4];
            }

            return PermutationP(block);
        }

        public static string GetBitByByte(string array)
        {
            string bit = "";
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 7; j >= 0; j--)
                    bit += array[i] >> j & 1;
            }
            return bit;
        }

        public static char[] KeyPermutationFunc(char[] array)
        {
            char[] permutation = new char[array.Length];
            for (int i = 0; i < KeyPermutation.Length; i++)
            {
                permutation[i] = array[KeyPermutation[i] - 1];
            }
            return permutation;
        }

        public static string ExtendKey(string array)
        {
            int correction = 0;
            for (int i = 7; i < 64; i += 8)
            {
                int count = 0;
                for (int j = 0; j < 7; j++)
                {
                    if (array[correction * 8 + j] == '1')
                    {
                        count++;
                    }
                }
                if (count % 2 == 0)
                    array = array.Insert(i, "0");
                else
                    array = array.Insert(i, "1");
                correction++;
            }
            return array;
        }

        public static char[] GetKeyPermutationFunc(char[] array)
        {
            char[] permutation = new char[array.Length];
            for (int i = 0; i < GetKeyPermutation.Length; i++)
            {
                permutation[i] = array[GetKeyPermutation[i] - 1];
            }
            return permutation;
        }

        public static string LeftShift(string array,int i)
        {
            string save = array.Substring(0, i);
            array = array.Remove(0, i);
            array += save;
            return array;
        }

        public static string RightShift(string array, int i)
        {
            string save = array.Substring(array.Length-i, i);
            string op = array;
            array = array.Remove(array.Length - i, i);
            array=array.Insert(0,save);
            return array;
        }

        public static char[] StartPermutationFunc(char[] array)
        {
            char[] permutation = new char[array.Length];
            for (int i = 0; i < StartPermutation.Length; i++)
            {
                permutation[i] = array[StartPermutation[i] - 1];
            }
            return permutation;
        }

        public static char[] EndPermutationFunc(char[] array)
        {
            char[] permutation = new char[array.Length];
            for (int i = 0; i < EndPermutation.Length; i++)
            {
                permutation[i] = array[EndPermutation[i] - 1];
            }
            return permutation;
        }

        public static char[] ExtendPermutationFunc(char[] array)
        {
            char[] permutation = new char[ExtendPermutation.Length];
            for (int i = 0; i < ExtendPermutation.Length; i++)
            {
                permutation[i] = array[ExtendPermutation[i] - 1];
            }
            return permutation;
        }

        public static int BitToInt(char[] array)
        {
            int result = 0;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                result += Int32.Parse(array[i].ToString()) * (int)Math.Pow(2, array.Length - 1 - i);
            }
            return result;
        }

        public static char[] PermutationS(char[] position, int ordertable)
        {
            string result = "";

            string row = position[0].ToString() + position[5].ToString();
            int rowOrder = BitToInt(row.ToCharArray());

            string column = (new string(position)).Substring(1, 4);
            int columnOrder = BitToInt(column.ToCharArray());

            int[,] table;
            switch (ordertable)
            {
                case 1:
                    table = ReplaceTable1;
                    break;

                case 2:
                    table = ReplaceTable2;
                    break;

                case 3:
                    table = ReplaceTable3;
                    break;

                case 4:
                    table = ReplaceTable4;
                    break;

                case 5:
                    table = ReplaceTable5;
                    break;

                case 6:
                    table = ReplaceTable6;
                    break;

                case 7:
                    table = ReplaceTable7;
                    break;

                case 8:
                    table = ReplaceTable8;
                    break;

                default:
                    table = null;
                    break;
            }

            int value = table[rowOrder, columnOrder];

            for (int j = 3; j >=0; j--)
                result += value >> j & 1;

            return result.ToCharArray();
        }

        public static char[] PermutationP(char[] array)
        {
            char[] result = array;
            for (int i = 0; i < ReplaceP.Length; i++)
            {
                result[i] = array[ReplaceP[i]-1];
            }
            return result;
        }

        public static char[] XOR(char[] a, char[] b)
        {
            string result = "";

            for (int i = 0; i <a.Length; i++)
            {
                bool r1 = Convert.ToBoolean(Convert.ToInt32(a[i].ToString()));
                bool r2 = Convert.ToBoolean(Convert.ToInt32(b[i].ToString()));

                if (r1 ^ r2)
                    result += "1";
                else
                    result += "0";
            }
            return result.ToCharArray();
        }
    }
}
