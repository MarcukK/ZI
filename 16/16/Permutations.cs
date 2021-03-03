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

        private static void DoInitialFullKeyPermutation()
        {
            //InitialFullKeyPermutationList = CreateInitialFullKeyPermutationList();

            BitArray fullKeyAfterInitialFullKeyPermutation = new BitArray(64);
            for (int i = 0; i < fullKeyAfterInitialFullKeyPermutation.Length; i++)
            {
                fullKeyAfterInitialFullKeyPermutation.Set(InitialFullKeyPermutationList.IndexOf(i + 1), fullKey[i]);
                if (i % 8 == 6)
                    i++;
            }
            Console.WriteLine("\nKey After Initial Full Key Permutation");
            ShowBitArray(fullKeyAfterInitialFullKeyPermutation);
            fullKey = fullKeyAfterInitialFullKeyPermutation;

        }
        private static void DoInitialFullKeyPermutationBack()
        {
            InitialFullKeyPermutationList = CreateInitialFullKeyPermutationList();

            BitArray fullKeyAfterInitialFullKeyPermutationBack = new BitArray(64);
            for (int i = 0; i < 56; i++)
            {
                fullKeyAfterInitialFullKeyPermutationBack.Set(InitialFullKeyPermutationList[i], fullKey[i]);
                if (i % 8 == 6)
                    i++;
            }
            //Console.WriteLine("\nKey After Initial Full Key Permutation Back");
            //ShowBitArray(fullKeyAfterInitialFullKeyPermutationBack);
            fullKey = fullKeyAfterInitialFullKeyPermutationBack;

        }


        private static BitArray DoPPermutation(BitArray block)
        {
            BitArray newBlock = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                newBlock[i] = block[PPermutationList[i] - 1];
            }
            //Console.WriteLine("\nNew Block After P Permutation");
            //ShowBitArray(newBlock);
            return newBlock;
        }
        private static BitArray DoPPermutationBack(BitArray block)
        {
            BitArray newBlock = new BitArray(32);
            for (int i = 0; i < 32; i++)
            {
                newBlock[i] = block[PPermutationList.IndexOf(i + 1)];
            }
            //Console.WriteLine("\nNew Block After P Permutation Back");
            //ShowBitArray(newBlock);
            return newBlock;
        }

        private static BitArray DoInitialPermutation(BitArray message)
        {
            //InitialPermutationList = CreateInitialPermutationList();
            BitArray messageAfterInitialPermutation = new BitArray(64);
            for (int i = 0; i < messageAfterInitialPermutation.Length; i++)
            {
                messageAfterInitialPermutation.Set(InitialPermutationList.IndexOf(i + 1), message[i]);
            }
            Console.WriteLine("\nMessage After Initial Permutation");
            ShowBitArray(messageAfterInitialPermutation);
            return messageAfterInitialPermutation;
        }
        private static BitArray DoInitialPermutationBack(BitArray message)
        {
            //InitialPermutationList = CreateInitialPermutationList();
            BitArray messageAfterInitialPermutationBack = new BitArray(64);
            for (int i = 0; i < messageAfterInitialPermutationBack.Length; i++)
            {
                messageAfterInitialPermutationBack.Set(InitialPermutationList[i] - 1, message[i]);
            }
            //Console.WriteLine("\nMessage After Initial Permutation Back");
            //ShowBitArray(messageAfterInitialPermutationBack);
            return messageAfterInitialPermutationBack;
        }

        private static BitArray DoFinalPermutation(BitArray message)
        {
            //FinalPermutationList = CreateFinalPermutationList();
            BitArray messageAfterFinalPermutation = new BitArray(64);
            for (int i = 0; i < messageAfterFinalPermutation.Length; i++)
            {
                messageAfterFinalPermutation.Set(FinalPermutationList.IndexOf(i + 1), message[i]);
            }
            //Console.WriteLine("\nMessage After Final Permutation");
            //ShowBitArray(messageAfterFinalPermutation);
            return messageAfterFinalPermutation;
        }
        private static BitArray DoFinalPermutationBack(BitArray message)
        {
            //FinalPermutationList = CreateFinalPermutationList();
            BitArray messageAfterFinalPermutationBack = new BitArray(64);
            for (int i = 0; i < messageAfterFinalPermutationBack.Length; i++)
            {
                messageAfterFinalPermutationBack.Set(FinalPermutationList[i] - 1, message[i]);
            }
            //Console.WriteLine("\nMessage After Final Permutation");
            //ShowBitArray(messageAfterFinalPermutationBack);
            return messageAfterFinalPermutationBack;
        }
    }
}
