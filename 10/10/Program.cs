using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//string myFIO = "10011100 10110000 10000000 10000111 10000011 10111010 10011010 10111110 10111101 10000001 10000010 10110000 10111101 10000010 10111000 10111101 10100001 10110101 10000000 10110011 10110101 10110101 10110010 10111000 10000111";
//string[] myFIOArray = myFIO.Split();
//foreach (var item in myFIOArray)
//{
//    //Console.WriteLine(Convert.ToInt32(item, 2));
//    Console.Write(Convert.ToString(Convert.ToInt32(item, 2), 2) + " ");
//    Console.Write(Convert.ToString(Convert.ToInt32(item, 2), 8) + " ");
//    Console.Write(Convert.ToString(Convert.ToInt32(item, 2), 10) + " ");
//    Console.WriteLine(Convert.ToString(Convert.ToInt32(item, 2), 16));
//}

//string a, b, c, d;
////1
//a = "000000000000000"; b = "2000302013020"; c = "130313031303130313333333";
//d = "00002";
////2
//a = "000000000000002"; b = "0003020130201"; c = "30313031303130313333333";
//d = "00002 00033";
////3
//a = "000000200030201"; b = "3020130313031"; c = "303130313333333";
//d = "00002 00033 22031";
////4
//a = "003020130201303"; b = "1303130313031"; c = "3333333";
//d = "00002 00033 22031 23133";
////5
//a = "031303130313031"; b = "3333333"; c = "";
//d = "00002 00033 22031 23133 С30301";
////6
//a = "130313031303133"; b = "33333"; c = "";
//d = "00002 00033 22031 23133 С30301 С02013";
////7
//a = "130313031303133"; b = ""; c = "";
//d = "00002 00033 22031 23133 С30301 С02013 c32103";

//d = "00002000332203123133303010201332103";

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            int p_LengthFromStart = 0;
            int q_MatchLength = 0;
            string s_LastSymbol = "";

            int N_AlphabetCapacity = 2;
            int dictionarySize = 16;
            int buferSize = 16;
                        
            int buferPaddingLength = (int)Math.Round(Math.Log(buferSize, N_AlphabetCapacity));
            int dictionaryPaddingLength = (int)Math.Round(Math.Log(dictionarySize, N_AlphabetCapacity));

            int l_AlphabetCapacity = ((int)Math.Round(Math.Log(dictionarySize, N_AlphabetCapacity))
                                    + (int)Math.Round(Math.Log(buferSize, N_AlphabetCapacity))
                                    + 1);

            
            string baseMassage = "10011100101100001000000010000111100000111011101010011010101111101011110110000001100000101011000010111101100000101011100010111101101000011011010110000000101100111011010110110101101100101011100010000111";
            string FIOInASCII = baseMassage;
            Console.WriteLine(FIOInASCII);
            Console.WriteLine(FIOInASCII.Length);
            Console.WriteLine();

            if(buferSize > FIOInASCII.Length)
                throw new ArgumentException("Bufer Size " + buferSize + " is bigger than size of message " + FIOInASCII.Length);

            string window = "";
            string encodedFIO = "";


            window = window.PadLeft(dictionarySize, '0');
            window += FIOInASCII.Substring(0, buferSize);
            FIOInASCII = FIOInASCII.Substring(buferSize, FIOInASCII.Length - buferSize);

            while (FIOInASCII != "" || window.Length > dictionarySize)
            {
                q_MatchLength = 0;

                string dictionary = window.Substring(0, dictionarySize);
                string bufer = window.Substring(dictionarySize, window.Length - dictionarySize);

                string searchString = bufer.Substring(0, 1);

                //Console.WriteLine("Dictionary " + dictionary);
                //Console.WriteLine("Bufer      " + bufer);
                //Console.WriteLine("encodedFIO " + encodedFIO);

                while (dictionary.IndexOf(searchString) != -1)
                {
                    p_LengthFromStart = dictionary.IndexOf(searchString);
                    q_MatchLength++;
                    if (bufer.Length > q_MatchLength)
                        searchString = bufer.Substring(0, q_MatchLength + 1);
                    else break;
                }
                if (bufer.Length < q_MatchLength + 1)
                    q_MatchLength = bufer.Length - 1;
                s_LastSymbol = bufer.Substring(q_MatchLength, 1);


                string p_encoded = Convert.ToString(p_LengthFromStart, 2);
                string q_encoded = Convert.ToString(q_MatchLength, 2);
                p_encoded = p_encoded.PadLeft(dictionaryPaddingLength, '0');
                q_encoded = q_encoded.PadLeft(buferPaddingLength, '0');

                encodedFIO += p_encoded + q_encoded + s_LastSymbol;

                if (FIOInASCII.Length >= (q_MatchLength + 1))
                {       
                    window = window.Substring(q_MatchLength + 1, window.Length - q_MatchLength - 1) + FIOInASCII.Substring(0, q_MatchLength + 1);
                    FIOInASCII = FIOInASCII.Substring(q_MatchLength + 1, FIOInASCII.Length - q_MatchLength - 1);
                }
                else
                {
                    window = window.Substring(q_MatchLength + 1, window.Length - q_MatchLength - 1) + FIOInASCII;
                    FIOInASCII = "";
                }

            }

            Console.WriteLine(encodedFIO);
            Console.WriteLine(encodedFIO.Length);
            Console.WriteLine();

            string decodedFIO = "";

            window = "";
            window = window.PadLeft(dictionarySize, '0');

            p_LengthFromStart = 0;
            q_MatchLength = 0;
            s_LastSymbol = "";
            string encodedCharset = "";

            while (encodedFIO != "")
            {
                if (encodedFIO.Length >= dictionaryPaddingLength + buferPaddingLength + 1)
                {
                    encodedCharset = encodedFIO.Substring(0, dictionaryPaddingLength + buferPaddingLength + 1);
                    encodedFIO = encodedFIO.Substring(dictionaryPaddingLength + buferPaddingLength + 1, encodedFIO.Length - dictionaryPaddingLength - buferPaddingLength - 1);
                }
                else
                {
                    encodedCharset = encodedFIO;
                    encodedFIO = "";
                }
                if(encodedCharset.Length >= dictionaryPaddingLength)
                {
                    p_LengthFromStart = Convert.ToInt32(encodedCharset.Substring(0, dictionaryPaddingLength), 2);
                    q_MatchLength = Convert.ToInt32(encodedCharset.Substring(dictionaryPaddingLength, buferPaddingLength), 2);
                    s_LastSymbol = encodedCharset.Last().ToString(); //Substring(dictionaryPaddingLength+ buferPaddingLength, 1);
                }
                else
                {
                    p_LengthFromStart = Convert.ToInt32(encodedCharset.Substring(0, dictionaryPaddingLength), 2);
                    q_MatchLength = Convert.ToInt32(encodedCharset.Substring(dictionaryPaddingLength, buferPaddingLength), 2);
                    s_LastSymbol = encodedCharset.Last().ToString(); //Substring(dictionaryPaddingLength+ buferPaddingLength, 1);
                }

                decodedFIO += window.Substring(0, q_MatchLength + 1);
                window += window.Substring(p_LengthFromStart, q_MatchLength) + s_LastSymbol;
                window = window.Substring(q_MatchLength + 1, window.Length - q_MatchLength - 1);
            }
            decodedFIO += window;
            decodedFIO = decodedFIO.Substring(dictionarySize, decodedFIO.Length - dictionarySize);

            Console.WriteLine(decodedFIO);
            Console.WriteLine(decodedFIO.Length);
            Console.WriteLine();

            Console.WriteLine(baseMassage == decodedFIO);

            Console.ReadLine();
        }
    }
}
