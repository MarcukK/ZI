using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15
{
    class Program
    {
        private static int AddIndex(int number, int index)
        {
            return (number + index) % 26;
        }
        private static List<char> MoveRotor(List<char> rotor, int numberOfPositions)
        {
            rotor.InsertRange(0, rotor.GetRange(rotor.Count - numberOfPositions, numberOfPositions));
            rotor.RemoveRange(rotor.Count - numberOfPositions, numberOfPositions);
            return rotor;
        }


        static void Main(string[] args)
        {
            //0 - 0 - 0
            //M
            //Q
            //W
            ///F
            ///FU
            ///U
            //I
            //N
            //G

            //1 - 3 - 9 (0-2-2)
            //A
            //S
            //C
            ///J
            ///IJ
            ///I
            //O            
            //F
            //W

            List<char> Alphabet = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            // L - Beta
            List<char> R        = new List<char>() { 'L', 'E', 'Y', 'J', 'V', 'C', 'N', 'I', 'X', 'W', 'P', 'B', 'Q', 'M', 'D', 'R', 'T', 'A', 'K', 'Z', 'G', 'F', 'U', 'H', 'O', 'S' };
            // M - Gamma
            List<char> M        = new List<char>() { 'F', 'S', 'O', 'K', 'A', 'N', 'U', 'E', 'R', 'H', 'M', 'B', 'T', 'I', 'Y', 'C', 'W', 'L', 'Q', 'P', 'Z', 'X', 'V', 'G', 'J', 'D' };
            // R - V
            List<char> L        = new List<char>() { 'V', 'Z', 'B', 'R', 'G', 'I', 'T', 'Y', 'U', 'P', 'S', 'D', 'N', 'H', 'L', 'X', 'A', 'W', 'M', 'J', 'Q', 'O', 'F', 'E', 'C', 'K' };
            // Re- B Dunn
            List<string> Re = new List<string>() { "AE", "BN", "CK", "DQ", "FU", "GY", "HW", "IJ", "LO", "MP", "RX", "SZ", "TV" };
            // Li-Mi-Ri - 0-2-2
            int Rei = 0;

            int LiIndex = 0;
            int MiIndex = 2;
            int RiIndex = 2;

            char LChar;
            char MChar;
            char RChar;

            char LCharNew;
            char MCharNew;
            char RCharNew;
            char ReCharNew;

            int n = 26;
            List<char> Chars = new List<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            List<int> Counters = new List<int>() { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n };
            List<double> Probabilities = new List<double>();

            //Encoding
            //symbol -> R -> M -> L -> Re -> L -> M -> R -> symbolNEW

            string FIO = "MARCHUKKANSTANTSINSERGEEVICH";
            Console.WriteLine(FIO);
            
            string[] codeOfTheDay = { "VFL", "QWE", "RTZ", "UIO", "PAS", "DFG", "HJK", "LYX", "CVB", "NMQ", "WER", "TZU", "IOP", "ASD" };
            foreach (var item in codeOfTheDay)
            {
                L = MoveRotor(L, L.Count - L.IndexOf(item[0]));
                M = MoveRotor(M, M.Count - M.IndexOf(item[1]));
                R = MoveRotor(R, R.Count - R.IndexOf(item[2]));

                string FIOnew = "";

                for (int i = 0; i < FIO.Length; i++)
                {
                    LChar = L[Alphabet.IndexOf(FIO[i])];
                    MChar = M[Alphabet.IndexOf(LChar)];
                    RChar = R[Alphabet.IndexOf(MChar)];
                    Rei = Re.IndexOf(Re.Find(x => (x[0]) == RChar || (x[1]) == RChar));
                    if (Re[Rei][0] != RChar)
                    {
                        ReCharNew = Re[Rei][0];
                    }
                    else
                    {
                        ReCharNew = Re[Rei][1];
                    }
                    RCharNew = Alphabet[R.IndexOf(ReCharNew)];
                    MCharNew = Alphabet[M.IndexOf(RCharNew)];
                    LCharNew = Alphabet[L.IndexOf(MCharNew)];

                    //Console.WriteLine(FIO[i]);
                    //Console.WriteLine(LChar);
                    //Console.WriteLine(MChar);
                    //Console.WriteLine(RChar);
                    //Console.WriteLine(ReCharNew);
                    //Console.WriteLine(RCharNew);
                    //Console.WriteLine(MCharNew);
                    //Console.WriteLine(LCharNew);
                    //Console.WriteLine();

                    FIOnew += LCharNew;

                    R = MoveRotor(R, (1 + RiIndex));
                    M = MoveRotor(M, (1 + MiIndex) * (1 + RiIndex));
                    L = MoveRotor(L, (1 + LiIndex) * (1 + MiIndex) * (1 + RiIndex));

                    //L = MoveRotor(L, (1 + LiIndex));
                    //M = MoveRotor(M, (1 + MiIndex) * (1 + LiIndex));
                    //R = MoveRotor(R, (1 + LiIndex) * (1 + MiIndex) * (1 + RiIndex));

                    //Decoding
                }
                Console.WriteLine(FIOnew);
                for (int i = 0; i < Chars.Count; i++)
                {
                    for (int j = 0; j < FIOnew.Length; j++)
                    {
                        Counters[i] += FIOnew.ToLower().Count(x => x == Chars[i]);
                    }
                }

            }
            int sumChars = Counters.Sum(x => x);
            for (int i = 0; i < Chars.Count; i++)
            {
                Console.Write(Chars[i]); Console.Write(" - "); Console.Write(Counters[i]);
                Probabilities.Add((((double)Counters[i]) / sumChars));
                Console.WriteLine(" Вероятность появления = " + Probabilities[i]);
            }
            Console.ReadLine();
        }
    }
}
