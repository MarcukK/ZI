using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;

namespace _17
{
    class Program
    {
        static Random rand = new Random();

        static void ShowListInt(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
                Console.WriteLine();
            }
        }

        public static BigInteger RandomIntegerBelow(BigInteger N)
        {
            byte[] bytes = N.ToByteArray();
            BigInteger R;

            do
            {
                rand.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger(bytes);
            } while (R >= N);

            return R;
        }

        static BigInteger GetRandomPrimeBigInteger() {
            var rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[512 / 8];
            rng.GetBytes(bytes);

            BigInteger value = new BigInteger(bytes);
            Boolean isPrime = false;

            if (value.Sign == -1)
                value *= -1;

            BigInteger biggerValue = value;
            Boolean isBiggerPrime = false;
            BigInteger lowerValue = value;
            Boolean isLowerPrime = false;
            do
            {
                biggerValue++;
                lowerValue--;
                isBiggerPrime = biggerValue.IsProbablyPrime();
                isLowerPrime = lowerValue.IsProbablyPrime();
                Console.WriteLine(biggerValue);
                Console.WriteLine(lowerValue);
            } while (!isBiggerPrime && !isLowerPrime);
            if (isLowerPrime)
                value = lowerValue;
            if (isBiggerPrime)
                value = biggerValue;
            return value;
        }

        static void Main(string[] args)
        {
            BigInteger p = GetRandomPrimeBigInteger();
            BigInteger q = GetRandomPrimeBigInteger();
            while(p == q){
                q = GetRandomPrimeBigInteger();
            }

            BigInteger n = p * q;
            BigInteger pseudoN = (p - 1) * (q - 1);

            BigInteger e = GetRandomPrimeBigInteger();
            while (e == q || e == p)
            {
                e = GetRandomPrimeBigInteger();
            }

            BigInteger xt = GetRandomPrimeBigInteger();
            for (int i = 0; i < 100; i++)
            {
                xt = BigInteger.ModPow(xt, e, n);
                //Console.WriteLine(xt);
                //if(BigInteger.ModPow(xt, 1, 2) == 1)
                //    Console.Write("1");
                //else
                //    Console.Write("0");
                Console.Write((xt % 2 == 1) ? "1" : "0");
            }
            Console.WriteLine();
            ////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////RC4
            ////////////////////////////////////////////////////////

            int rc4N = 6;
            int count2byN = (int)Math.Pow(2, rc4N);
            List<int> S = new List<int>();
            for (int i = 0; i < count2byN; i++)
            {
                S.Add(i);
            }
            List<int> keys = new List<int> { 10, 11, 12, 13, 14, 15 };
            List<int> K = new List<int>();
            for (int i = 0; i < count2byN; i++)
            {
                K.Add(keys[i % keys.Count]);
            }

            //int rc4N = 4;
            //int count2byN = (int)Math.Pow(2, rc4N);
            //List<int> S = new List<int>();
            //for (int i = 0; i < count2byN; i++)
            //{
            //    S.Add(i);
            //}
            //List<int> keys = new List<int> { 1,2,3,4,5,6 };
            //List<int> K = new List<int>();
            //for (int i = 0; i < count2byN; i++)
            //{
            //    K.Add(keys[i % keys.Count]);
            //}

            int changerS;
            for (int i = 0, j = 0; i < count2byN; i++)
            {
                    j = (j + S[i] + K[i]) % count2byN;
                    changerS = S[i];
                    S[i] = S[j];
                    S[j] = changerS;
            }

            int a;
            int key;
            for (int i = 0, j = 0; i < 10;)
            {
                i += 1;
                j = (j + S[i]) % count2byN;

                changerS = S[i];
                S[i] = S[j];
                S[j] = changerS;

                a = (S[i] + S[j]) % count2byN;

                key = S[a];
                Console.Write(key + " ");
                Console.Write(Convert.ToString(key,2).PadLeft(6,'0') + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadLine();

        }
    }
}
