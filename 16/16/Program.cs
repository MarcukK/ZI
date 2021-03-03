using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace _16
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //extensionList = GetExtensionList();

            BitArray message = GetMessage();

            List<string> initialKeys = new List<string>(){
                "01010001001000110010011100101111000111110011111101111111",
                "10110011100011110000111110000011111100000011111110000000",
                "01010101001100110001110001110000111100001111000001111101"
            };
            ShowInitialData();
            FillFullKey(initialKeys[0]);
            DoInitialFullKeyPermutation();
            FillShortKeys();

            Console.WriteLine("\nInitial Message");
            ShowBitArray(message);
            //BitArray messageAfterInitialPermutation = DoInitialPermutation(message);
            //List<BitArray> blocks = GetBlocksFromMessage(messageAfterInitialPermutation);
            List<BitArray> blocks = GetBlocksFromMessage(message);
            for (int j = 0; j < 16; j++)
            {
                blocks = DoRaund(blocks, shortKeys[j]);
            }
            message = GetFullBlockFromBlocks(blocks);
            //message = DoFinalPermutation(message);
            
            //Console.WriteLine("\nResult must be");
            //Console.WriteLine("0101100111111011101111010001000110110011100110100110011101101111");
            Console.WriteLine("\nEncoded Message");
            ShowBitArray(message);

            //messageAfterInitialPermutation = DoInitialPermutation(message);
            //blocks = GetBlocksFromMessage(messageAfterInitialPermutation);
            blocks = GetBlocksFromMessage(message);
            for (int j = 0; j < 16; j++)
            {
                blocks = DoRaundBack(blocks, shortKeys[15 - j]);
            }
            message = GetFullBlockFromBlocks(blocks);
            //message = DoFinalPermutation(message);

            //Console.WriteLine("\nResult must be");
            //Console.WriteLine("1010101011001100111100001111111110101010110011001111000011111110");            
            Console.WriteLine("\nDecoded Message");
            ShowBitArray(message);



            //for (int i = 0; i < 3; i++)
            //{
            //    FillFullKey(initialKeys[i]);
            //    DoInitialFullKeyPermutation();
            //    FillShortKeys();
            //    BitArray messageAfterInitialPermutation = DoInitialPermutation(message);
            //    List<BitArray> blocks = GetBlocksFromMessage(messageAfterInitialPermutation);
            //    for (int j = 0; j < 16; j++)
            //    {
            //        blocks = DoRaund(blocks, shortKeys[j]);
            //    }
            //    message = GetFullBlockFromBlocks(blocks);
            //    message = DoFinalPermutation(message);
            //}
            Console.WriteLine("\nFinal Message");
            Console.WriteLine(message);

            Console.ReadLine();
        }
    }
}
