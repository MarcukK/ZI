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
        private static List<bool> MoveListLeft(List<bool> list, int numberOfPositions)
        {
            list.AddRange(list.GetRange(0, numberOfPositions));
            list.RemoveRange(0, numberOfPositions);
            return list;
        }

        private static List<bool> MoveListRight(List<bool> list, int numberOfPositions)
        {
            list.InsertRange(0, list.GetRange(list.Count - numberOfPositions, numberOfPositions));
            list.RemoveRange(list.Count - numberOfPositions, numberOfPositions);
            return list;
        }

        private static List<BitArray> GetBlocksFromMessage(BitArray message)
        {
            List<BitArray> blocks = new List<BitArray>();
            blocks.Add(new BitArray(message.Length / 2));
            blocks.Add(new BitArray(message.Length / 2));
            for (int i = 0; i < message.Length / 2; i++)
            {
                blocks[0].Set(i, message[i]);
            }
            Console.WriteLine("\nLeft Block");
            ShowBitArray(blocks[0]);

            for (int i = message.Length / 2; i < message.Length; i++)
            {
                blocks[1].Set(i - message.Length / 2, message[i]);
            }
            Console.WriteLine("\nRight Block");
            ShowBitArray(blocks[1]);

            return blocks;
        }

        private static BitArray GetLeftBlockFromMessage(BitArray message)
        {
            BitArray leftBlock = new BitArray(message.Length / 2);
            for (int i = 0; i < message.Length / 2; i++)
            {
                leftBlock.Set(i, message[i]);
            }
            //Console.WriteLine("\nLeft Block");
            //ShowBitArray(leftBlock);
            return leftBlock;
        }

        private static BitArray GetRightBlockFromMessage(BitArray message)
        {
            BitArray rightBlock = new BitArray(message.Length / 2);
            for (int i = 0; i < message.Length / 2; i++)
            {
                rightBlock.Set(i, message[(message.Length / 2 + i)]);
            }
            //Console.WriteLine("\nRight Block");
            //ShowBitArray(rightBlock);
            return rightBlock;
        }

        private static BitArray GetFullBlockFromBlocks(List<BitArray> blocks)
        {

            BitArray fullBlock = new BitArray(64);
            for (int i = 0; i < 32; i++)
            {
                fullBlock[i] = blocks[0][i];
            }
            //Console.WriteLine("\nLeft Block");
            //ShowBitArray(blocks[0]);
            for (int i = 0; i < 32; i++)
            {
                fullBlock[i + 32] = blocks[1][i];
            }
            //Console.WriteLine("\nRight Block");
            //ShowBitArray(blocks[1]);
            //Console.WriteLine("\nFull Block");
            //ShowBitArray(fullBlock);
            return fullBlock;
        }

        private static BitArray GetFullBlockFromBlocks(BitArray blockL, BitArray blockR)
        {
            BitArray fullBlock = new BitArray(64);
            for (int i = 0; i < 32; i++)
            {
                fullBlock[i] = blockL[i];
            }
            for (int i = 0; i < 32; i++)
            {
                fullBlock[i + 32] = blockR[i];
            }
            //Console.WriteLine("\nRight Block");
            //ShowBitArray(fullBlock);
            return fullBlock;
        }
    }
}
