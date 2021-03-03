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
        private static List<int> extensionList = new List<int>(48) {
                32, 1,  2,  3,  4,  5,
                4,  5,  6,  7,  8,  9,
                8,  9,  10, 11, 12, 13,
                12, 13, 14, 15, 16, 17,
                16, 17, 18, 19, 20, 21,
                20, 21, 22, 23, 24, 25,
                24, 25, 26, 27, 28, 29,
                28, 29, 30, 31, 32, 1
        };

        //private static List<int> GetExtensionList()
        //{
        //    List<int> extensionList = new List<int>();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        extensionList.Add((31 + i * 4) % 32);
        //        extensionList.Add((0 + i * 4) % 32);
        //        extensionList.Add((1 + i * 4) % 32);
        //        extensionList.Add((2 + i * 4) % 32);
        //        extensionList.Add((3 + i * 4) % 32);
        //        extensionList.Add((4 + i * 4) % 32);
        //    }
        //    //Console.WriteLine("\nExtension List");
        //    //ShowIntListAsTable(extensionList.Select(x => x + 1).ToList(), 8, 6);
        //    return extensionList;
        //}

        private static BitArray DoBlockExtension(BitArray block)
        {
            BitArray extendedBlock = new BitArray(48);
            for (int i = 0; i < 48; i++)
            {
                extendedBlock.Set(i, block[extensionList[i] - 1]);
            }
            //Console.WriteLine("\nExtended Block");
            //ShowBitArray(extendedBlock);
            return extendedBlock;
        }
        private static BitArray DoBlockExtensionBack(BitArray block)
        {
            BitArray extendedBlockBack = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                extendedBlockBack.Set(i, block[extensionList.IndexOf(i)]);
            }
            //Console.WriteLine("\nExtended Block");
            //ShowBitArray(extendedBlockBack);
            return extendedBlockBack;
        }

        private static BitArray DoBlockCompression(BitArray block)
        {
            //Console.WriteLine("\nNot Compressed Block");
            //ShowBitArray(block);
            BitArray compressedBlock = new BitArray(32);
            for (int i = 0, row = 0, col = 0, newBits = 0; i < 8; i++, row = 0, col = 0)
            {
                if (block[i * 6])
                    row += 2;
                if (block[i * 6 + 5])
                    row += 1;

                if (block[i * 6 + 1])
                    col += 8;
                if (block[i * 6 + 2])
                    col += 4;
                if (block[i * 6 + 3])
                    col += 2;
                if (block[i * 6 + 4])
                    col += 1;

                newBits = SBlocks[i][row * 16 + col];

                compressedBlock[i * 4 + 0] = Convert.ToBoolean(newBits / 8); newBits %= 8;
                compressedBlock[i * 4 + 1] = Convert.ToBoolean(newBits / 4); newBits %= 4;
                compressedBlock[i * 4 + 2] = Convert.ToBoolean(newBits / 2); newBits %= 2;
                compressedBlock[i * 4 + 3] = Convert.ToBoolean(newBits);
            }
            //Console.WriteLine("\nCompressed Block");
            //ShowBitArray(compressedBlock);
            return compressedBlock;
        }
        private static BitArray DoBlockCompressionBack(BitArray block)
        {
            BitArray compressedBlockBack = new BitArray(48);
            for (int i = 0, row = 0, col = 0, newBits = 0; i < 8; i++, row = 0, col = 0, newBits = 0)
            {
                if (block[i * 4 + 0])
                    newBits += 8;
                if (block[i * 4 + 1])
                    newBits += 4;
                if (block[i * 4 + 2])
                    newBits += 2;
                if (block[i * 4 + 3])
                    newBits += 1;
                //compressedBlockBack[i * 4 + 0] = Convert.ToBoolean(newBits / 8); newBits %= 8;
                //compressedBlockBack[i * 4 + 1] = Convert.ToBoolean(newBits / 4); newBits %= 4;
                //compressedBlockBack[i * 4 + 2] = Convert.ToBoolean(newBits / 2); newBits %= 2;
                //compressedBlockBack[i * 4 + 3] = Convert.ToBoolean(newBits);

                newBits = SBlocks[i].IndexOf(newBits);
                //row * 16 + col
                row = newBits / 16;
                col = newBits % 16;

                compressedBlockBack[i * 6 + 0] = Convert.ToBoolean(row / 2); row %= 2;
                compressedBlockBack[i * 6 + 5] = Convert.ToBoolean(row);

                compressedBlockBack[i * 6 + 1] = Convert.ToBoolean(col / 8); col %= 8;
                compressedBlockBack[i * 6 + 2] = Convert.ToBoolean(col / 4); col %= 4;
                compressedBlockBack[i * 6 + 3] = Convert.ToBoolean(col / 2); col %= 2;
                compressedBlockBack[i * 6 + 4] = Convert.ToBoolean(col);

            }
            //Console.WriteLine("\nCompressed Block Back");
            //ShowBitArray(compressedBlockBack);
            return compressedBlockBack;
        }

        private static List<BitArray> DoRaund(List<BitArray> blocks, BitArray key)
        {
            BitArray blockLeft = blocks[0];
            BitArray blockRight = blocks[1];

            blockRight = DoBlockExtension(blockRight);
            blockRight.Xor(key);
            blockRight = DoBlockCompression(blockRight);
            //blockRight = DoPPermutation(blockRight);

            blocks[0] = blocks[1];
            blocks[1] = blockLeft.Xor(blockRight);
            return blocks;
        }

        private static List<BitArray> DoRaundBack(List<BitArray> blocks, BitArray key)
        {
            BitArray blockLeft = blocks[0];
            BitArray blockRight = blocks[1];

            blockRight = DoBlockExtension(blockRight);
            blockRight = blockRight.Xor(key);
            blockRight = DoBlockCompression(blockRight);
            blockRight = DoPPermutation(blockRight);

            blocks[0] = blocks[1];
            blocks[1] = blockLeft.Xor(blockRight);
            return blocks;
        }

    }
}
