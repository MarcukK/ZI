using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _9
{
    public class SymbolWithCode
    {
        public SymbolWithCode(char s, int cnt, double p, string c)
        {
            this.symbol = s;
            this.count = cnt;
            this.probalility = p;
            this.code = c;
        }
        public char symbol;
        public int count;
        public double probalility;
        public string code;

        public static void ShowSymbolsWithCodes(List<SymbolWithCode> symbolsWithCodes)
        {
            foreach (var symbolWithCode in symbolsWithCodes)
            {
                Console.WriteLine("{0} {1} {2} {3}", symbolWithCode.symbol, symbolWithCode.count, symbolWithCode.probalility, symbolWithCode.code);
            }
            Console.WriteLine();
        }

        public static List<SymbolWithCode> AddSymbolsOfLine(List<SymbolWithCode> symbolsWithCodes, string line)
        {
            foreach (var character in line)
            {
                if (symbolsWithCodes.Find(x => x.symbol == character) == null)
                {
                    symbolsWithCodes.Add(new SymbolWithCode(character, 1, 0.0, ""));
                }
                else
                {
                    symbolsWithCodes.Where(x => x.symbol == character).ToList().ForEach(x => x.count++);
                }
            }
            return symbolsWithCodes;
        }
    }
    class Program
    {
        public static List<SymbolWithCode> AddCodes(List<SymbolWithCode> symbolsWithCodes)
        {
            int counter = 0;
            double probability = 0.0;
            List<SymbolWithCode> firstPartOfSymbolsWithCodes = new List<SymbolWithCode>();
            List<SymbolWithCode> secondPartOfSymbolsWithCodes = new List<SymbolWithCode>();

            while (probability < (symbolsWithCodes.Sum(x=>x.probalility) / 2))
            {
                probability += symbolsWithCodes[counter].probalility;
                counter++;
            }
            for (int i = 0; i < counter; i++)
            {
                symbolsWithCodes[i].code += "0";
                firstPartOfSymbolsWithCodes.Add(symbolsWithCodes[i]);
            }
            for (int i = counter; i < symbolsWithCodes.Count; i++)
            {
                symbolsWithCodes[i].code += "1";
                secondPartOfSymbolsWithCodes.Add(symbolsWithCodes[i]);
            }
            if (symbolsWithCodes.Count > 1)
            {
                firstPartOfSymbolsWithCodes = AddCodes(firstPartOfSymbolsWithCodes);
                secondPartOfSymbolsWithCodes = AddCodes(secondPartOfSymbolsWithCodes);

                firstPartOfSymbolsWithCodes.AddRange(secondPartOfSymbolsWithCodes);

                symbolsWithCodes = firstPartOfSymbolsWithCodes;
            }

            return symbolsWithCodes;
        }


        static void Main(string[] args)
        {
            List<SymbolWithCode> symbolsWithCodes = new List<SymbolWithCode>();

            using (StreamReader sr = new StreamReader(@"Mein_Kampf_German.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    symbolsWithCodes = SymbolWithCode.AddSymbolsOfLine(symbolsWithCodes, line);
                }
            }
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);


            double sumChars = symbolsWithCodes.Sum(x => x.count);
            Console.WriteLine(sumChars + " символов");

            for (int i = 0; i < symbolsWithCodes.Count; i++)
            {
                symbolsWithCodes[i].probalility = symbolsWithCodes[i].count / sumChars;
            }
            Console.WriteLine("Сумма вероятностей " + (symbolsWithCodes.Sum(x => x.probalility)));


            symbolsWithCodes = symbolsWithCodes.OrderByDescending(x => x.probalility).ToList();
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);


            symbolsWithCodes = AddCodes(symbolsWithCodes);
            foreach (var symbol in symbolsWithCodes)
            {
                symbol.code = symbol.code.Remove(symbol.code.Length - 1, 1);
            }
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);

            string FIO = "Marchuk Konstantin Sergeevich";
            string FIOencoded = "";
            foreach (var charFIO in FIO)
            {
                FIOencoded += (symbolsWithCodes.Where(x => x.symbol == charFIO).First()).code;
            }
            Console.WriteLine("Сообщение ");
            Console.WriteLine(FIO);
            Console.WriteLine("Закодированное сообщение ");
            Console.WriteLine(FIOencoded);
            Console.WriteLine("ASCII " + FIO.Count() * 8);
            Console.WriteLine("Шеннон-Фано " + FIOencoded.Count());

            Console.WriteLine("========================================");
            Console.WriteLine("ДЕКОДИРОВАНИЕ");
            Console.WriteLine("========================================");

            string characterEncoded = "";
            string FIOdecoded = "";
            for (int i = 0; i < FIOencoded.Count(); i++)
            {
                characterEncoded += FIOencoded[i];
                if (symbolsWithCodes.Find(x => x.code == characterEncoded) != null)
                {
                    FIOdecoded += symbolsWithCodes.Find(x => x.code == characterEncoded).symbol;
                    characterEncoded = "";
                }
            }
            Console.WriteLine(FIOdecoded);


            symbolsWithCodes.Clear();
            
            string line2 = "Marchuk Konstantin Sergeevich";
            symbolsWithCodes = SymbolWithCode.AddSymbolsOfLine(symbolsWithCodes, line2);
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);
            
            sumChars = symbolsWithCodes.Sum(x => x.count);
            Console.WriteLine(sumChars + " символов");

            for (int i = 0; i < symbolsWithCodes.Count; i++)
            {
                symbolsWithCodes[i].probalility = symbolsWithCodes[i].count / sumChars;
            }
            Console.WriteLine("Сумма вероятностей " + (symbolsWithCodes.Sum(x => x.probalility)));


            symbolsWithCodes = symbolsWithCodes.OrderByDescending(x => x.probalility).ToList();
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);


            symbolsWithCodes = AddCodes(symbolsWithCodes);
            foreach (var symbol in symbolsWithCodes)
            {
                symbol.code = symbol.code.Remove(symbol.code.Length - 1, 1);
            }
            SymbolWithCode.ShowSymbolsWithCodes(symbolsWithCodes);

            FIO = "Marchuk Konstantin Sergeevich";
            FIOencoded = "";
            foreach (var charFIO in FIO)
            {
                FIOencoded += (symbolsWithCodes.Where(x => x.symbol == charFIO).First()).code;
            }
            Console.WriteLine("Сообщение ");
            Console.WriteLine(FIO);
            Console.WriteLine("Закодированное сообщение ");
            Console.WriteLine(FIOencoded);
            Console.WriteLine("ASCII " + FIO.Count() * 8);
            Console.WriteLine("Шеннон-Фано " + FIOencoded.Count());

            Console.WriteLine("========================================");
            Console.WriteLine("ДЕКОДИРОВАНИЕ");
            Console.WriteLine("========================================");

            characterEncoded = "";
            FIOdecoded = "";
            for (int i = 0; i < FIOencoded.Count(); i++)
            {
                characterEncoded += FIOencoded[i];
                if (symbolsWithCodes.Find(x => x.code == characterEncoded) != null)
                {
                    FIOdecoded += symbolsWithCodes.Find(x => x.code == characterEncoded).symbol;
                    characterEncoded = "";
                }
            }
            Console.WriteLine(FIOdecoded);

            Console.ReadLine();
        }
    }
}
